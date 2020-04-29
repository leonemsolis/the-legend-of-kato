using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanelPlacer : MonoBehaviour
{

    Transform title, music, sound, restore;

    const float titleHeight = 75f;
    const float topPartHeight = 375f;


    const float restoreToTogglesGap = 400f;
    const float restoreHeight = 262.5f;
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

        title.localPosition = new Vector3(0f, Camera.main.orthographicSize - topPartHeight - titleHeight / 2f - safeAreaTopShiftValue, 0f);

        //Restore button was deleted...
        float restoreY = -Camera.main.orthographicSize + bottomGap + restoreHeight / 2f + safeAreaBotShiftValue;

        music.localPosition = new Vector3(music.localPosition.x, restoreY + restoreToTogglesGap, 0f);
        sound.localPosition = new Vector3(sound.localPosition.x, restoreY + restoreToTogglesGap, 0f);
    }
}
