using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBgBot : MonoBehaviour
{

    const float topPartHeight = 387.5f;
    const float botPartHeight = 100f;

    void Start()
    {
        float safeAreaTopShiftValue = 0f;
        #if UNITY_EDITOR_OSX
            safeAreaTopShiftValue = C.SafeAreaTopShiftValue;
        #else
            safeAreaTopShiftValue = Screen.height - Screen.safeArea.yMax;
        #endif
        
        float screenHeight = Camera.main.orthographicSize * 2f;
        float freeSpace = screenHeight - topPartHeight - safeAreaTopShiftValue;
        transform.localPosition = new Vector3(0f, -Camera.main.orthographicSize + freeSpace / 2f, 0f);
        transform.localScale = new Vector3(1f, freeSpace / botPartHeight, 1f);
    }
}
