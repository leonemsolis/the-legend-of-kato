using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPanel : MonoBehaviour
{
    private void Start()
    {
        float safeAreaBotShiftValue = 0f;

        #if UNITY_EDITOR_OSX
            safeAreaBotShiftValue = C.SafeAreaBotShiftValue;
        #else
            safeAreaBotShiftValue = Screen.safeArea.yMin;
        #endif

        float y = Camera.main.transform.position.y - Camera.main.orthographicSize + 400f / 2f + safeAreaBotShiftValue;
        transform.position = new Vector3(0f, y, transform.position.z);
    }
}
