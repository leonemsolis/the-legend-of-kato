using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Translator : MonoBehaviour
{
    [SerializeField] TMP_FontAsset korean_white_black;
    [SerializeField] TMP_FontAsset korean_black_white;
    [SerializeField] TMP_FontAsset korean_red_white;

    private static Translator instance = null;

    public enum Language {English, Korean};

    private Language language = Language.Korean;

    private Dictionary<string, string> translatedEN;
    private Dictionary<string, string> translatedKR;

    private void Awake() {
        // Check if instance exists
        if (instance == null) // Instance was found
        {
            instance = this; // Set reference to intance of this object
        }
        else // Instance already exists
        {
            Destroy(gameObject);
        }

        // Do not destroy object when switching scene
        DontDestroyOnLoad(gameObject);

        Init();
    }

    private void Init() {
        CSVLoader csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        translatedEN = csvLoader.GetDictionaryValues("en");
        translatedKR = csvLoader.GetDictionaryValues("kr");

        int l = PlayerPrefs.GetInt(C.PREFS_LANGUAGE, 0);
        switch(l) {
            case 0:
                SetLanguage(Language.English);
                break;
            case 1:
                SetLanguage(Language.Korean);
                break;
        }
    }

    public Language GetLanguage() {
        return language;
    }
    
    public void SetLanguage(Language lang) {
        language = lang;
        switch(language) {
            case Language.English:
                PlayerPrefs.SetInt(C.PREFS_LANGUAGE, 0);
                break;
            case Language.Korean:
                PlayerPrefs.SetInt(C.PREFS_LANGUAGE, 1);
                break;
        }
        PlayerPrefs.Save();
    }

    public void TranslateStaticTMP(string key, FontType fontType, TextMeshProUGUI tmp) {
        string translation = GetTranslation(key);
        SetFont(fontType, tmp);
        tmp.SetText(translation);
    }

    public void SetFont(FontType fontType, TextMeshProUGUI tmp) {
        switch(language) {
            case Language.Korean:
                switch(fontType) {
                    case FontType.BLACK_WHITE:
                        tmp.font = korean_black_white;
                        break;
                    case FontType.RED_WHITE:
                        tmp.font = korean_red_white;
                        break;
                    case FontType.WHITE_BLACK:
                        tmp.font = korean_white_black;
                        break;
                }
                break;
        }
    }

    public string GetTranslation(string key) {
        string value = key;
        switch(language) {
            case Language.English:
                translatedEN.TryGetValue(key, out value);
                break;
            case Language.Korean:
                translatedKR.TryGetValue(key, out value);
                break;
        }
        return value;
    }
}
