using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanelPlacer : MonoBehaviour
{

    Transform title, music, sound, language;

    const float titleHeight = 75f;
    const float topPartHeight = 375f;


    const float languageToTogglesGap = 400f;
    const float languageHeight = 312.5f;
    const float bottomGap = 300f;
    const float buttonHeight = 181.25f;

    void Start()
    {
        float safeAreaTopShiftValue, safeAreaBotShiftValue;

        #if UNITY_EDITOR_OSX
            safeAreaTopShiftValue = C.SafeAreaTopShiftValue;
            safeAreaBotShiftValue = C.SafeAreaBotShiftValue;
        #else
            safeAreaTopShiftValue = Screen.height - Screen.safeArea.yMax;
            safeAreaBotShiftValue = Screen.safeArea.yMin;
        #endif

        title = transform.GetChild(0);
        music = transform.GetChild(1);
        sound = transform.GetChild(2);
        language = transform.GetChild(3);

        title.localPosition = new Vector3(0f, Camera.main.orthographicSize - topPartHeight - titleHeight / 2f - safeAreaTopShiftValue, 0f);

        float languageY = -Camera.main.orthographicSize + bottomGap + languageHeight / 2f + safeAreaBotShiftValue;
        language.localPosition = new Vector3(language.localPosition.x, languageY, 0f);
        music.localPosition = new Vector3(music.localPosition.x, languageY + languageToTogglesGap, 0f);
        sound.localPosition = new Vector3(sound.localPosition.x, languageY + languageToTogglesGap, 0f);
    }
}
