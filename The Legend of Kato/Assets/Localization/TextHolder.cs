using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum FontType {BLACK_WHITE, RED_WHITE, WHITE_BLACK};

public class TextHolder : MonoBehaviour
{
    [SerializeField] FontType fontType = FontType.WHITE_BLACK;
    [SerializeField] string key;
    
    void Start()
    {
        FindObjectOfType<Translator>().TranslateStaticTMP(key, fontType, GetComponent<TextMeshProUGUI>());
    }
}
