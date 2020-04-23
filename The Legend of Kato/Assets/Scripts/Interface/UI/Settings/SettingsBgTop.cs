using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsBgTop : MonoBehaviour
{
    const float topPartHeight = 387.5f;

    void Start()
    {
        float safeAreaTopShiftValue = 0f;
        #if UNITY_EDITOR_OSX
            safeAreaTopShiftValue = C.SafeAreaTopShiftValue;
        #else
            safeAreaTopShiftValue = Screen.height - Screen.safeArea.yMax;
        #endif

        transform.localPosition = new Vector3(0f, Camera.main.orthographicSize - topPartHeight / 2f - safeAreaTopShiftValue, 0f);
    }
}
