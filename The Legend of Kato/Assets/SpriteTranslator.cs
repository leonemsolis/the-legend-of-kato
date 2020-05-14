using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteTranslator : MonoBehaviour
{
    [SerializeField] Sprite onUpKr;
    [SerializeField] Sprite onDownKr;
    [SerializeField] Sprite offUpKr;
    [SerializeField] Sprite offDownKr;
    [SerializeField] Sprite backButtonUp;
    [SerializeField] Sprite backButtonDown;
    [SerializeField] Sprite announcer_go;
    [SerializeField] Sprite announcer_tutorial;
    [SerializeField] Sprite announcer_entrance;
    [SerializeField] Sprite announcer_depth;
    [SerializeField] Sprite announcer_ocean_bed;
    [SerializeField] Sprite shopBuyUp;
    [SerializeField] Sprite shopBuyDown;
    [SerializeField] Sprite modeSelection;
    [SerializeField] Sprite goButtonUp;
    [SerializeField] Sprite goButtonDown;
    [SerializeField] Sprite quitButtonUp;
    [SerializeField] Sprite quitButtonDown;
    [SerializeField] Sprite level_selection;
    [SerializeField] Sprite store_button_up;
    [SerializeField] Sprite store_button_down;
    [SerializeField] Sprite small_go_up;
    [SerializeField] Sprite small_go_down;
    [SerializeField] Sprite kr_loading;

    public Sprite GetSprite(Sprite original, int id) {
        if(FindObjectOfType<Translator>().GetLanguage() == Translator.Language.Korean) {
            switch(id) {
                case 0:
                    if(SceneManager.GetActiveScene().buildIndex == C.SettingsSceneIndex) {
                        return offUpKr;
                    }
                    break;
                case 1:
                    if(SceneManager.GetActiveScene().buildIndex == C.SettingsSceneIndex) {
                        return offDownKr;
                    }
                    break;
                case 2:
                    if(SceneManager.GetActiveScene().buildIndex == C.SettingsSceneIndex) {
                        return onUpKr;
                    }
                    break;
                case 3:
                    if(SceneManager.GetActiveScene().buildIndex == C.SettingsSceneIndex) {
                        return onDownKr;
                    }
                    break;
                case 4:
                    return quitButtonUp;
                case 5:
                    return quitButtonDown;
                case 6:
                    return announcer_go;
                case 7:
                    return announcer_tutorial;
                case 8:
                    return announcer_entrance;
                case 9:
                    return announcer_depth;
                case 10:
                    return announcer_ocean_bed;
                case 11:
                    return shopBuyUp;
                case 12:
                    return shopBuyDown;
                case 13:
                    return modeSelection;
                case 14:
                    return goButtonUp;
                case 15:
                    return goButtonDown;
                case 16:
                    return backButtonUp;
                case 17:
                    return backButtonDown;
                case 18:
                    return level_selection;
                case 19:
                    return store_button_up;
                case 20:
                    return store_button_down;
                case 21:
                    return small_go_up;
                case 22:
                    return small_go_down;
                case 23:
                    return kr_loading;
            }
        }
        return original;
    }
}
