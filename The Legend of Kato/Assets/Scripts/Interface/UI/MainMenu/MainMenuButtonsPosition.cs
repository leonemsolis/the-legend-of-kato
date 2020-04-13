using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonsPosition : MonoBehaviour
{
    float safeAreaBotShiftValue;

    const float bottomGap = 115f;


    private void Start()
    {
        #if UNITY_EDITOR_OSX
            safeAreaBotShiftValue = 100f;
        #else
            safeAreaBotShiftValue = Screen.safeArea.yMin;
        #endif
        transform.position = new Vector3(0f, -Camera.main.orthographicSize + bottomGap + safeAreaBotShiftValue, 0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Application.platform == RuntimePlatform.Android)
        {
            Application.Quit();
        }
    }
}
