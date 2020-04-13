using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    private void Start()
    {
        float safeAreaTopShiftValue = 0f;

        #if UNITY_EDITOR_OSX
            safeAreaTopShiftValue = 100f;
        #else
            safeAreaTopShiftValue = Screen.height - Screen.safeArea.yMax;
        #endif

        float y = Camera.main.transform.position.y + Camera.main.orthographicSize - 100f - safeAreaTopShiftValue;
        transform.position = new Vector3(0f, y, transform.position.z);
    }
}
