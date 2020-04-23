using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapTextPlacer : MonoBehaviour
{
    const float buttonsPanelHeight = 250f;
    const float topPartHeight = 675f;

    private void Start()
    {
        float safeAreaBotShiftValue = 0f; 
        #if UNITY_EDITOR_OSX
            safeAreaBotShiftValue = C.SafeAreaBotShiftValue;
        #else
            safeAreaBotShiftValue = Screen.safeArea.yMin;
        #endif
        float gap = ((Camera.main.orthographicSize * 2f - buttonsPanelHeight) / 2f - topPartHeight / 2f) / 2f;
        transform.localPosition = new Vector3(0f, -Camera.main.orthographicSize + gap + buttonsPanelHeight + safeAreaBotShiftValue, 0f);
    }


    public void Hide()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
