using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MainFrameScaler : MonoBehaviour
{
    Transform top, mid, bot, safeAreaTop, safeAreaBot;

    const float onePartHeight = 250f;

    float safeAreaTopShiftValue, safeAreaBotShiftValue;

    void Start()
    {
        #if UNITY_EDITOR_OSX
            safeAreaTopShiftValue = C.SafeAreaTopShiftValue;
            safeAreaBotShiftValue = C.SafeAreaBotShiftValue;
        #else
            safeAreaTopShiftValue = Screen.height - Screen.safeArea.yMax;
            safeAreaBotShiftValue = Screen.safeArea.yMin;
        #endif
        

        top = transform.GetChild(0);
        mid = transform.GetChild(1);
        bot = transform.GetChild(2);
        safeAreaTop = transform.GetChild(3);
        safeAreaBot = transform.GetChild(4);

        safeAreaTop.localPosition = new Vector3(0f, CaculateSATopY(), 0f);
        safeAreaBot.localPosition = new Vector3(0f, CaculateSABotY(), 0f);

        top.localPosition = new Vector3(0f, CalculateTopY(), 0f);
        bot.localPosition = new Vector3(0f, CalculateBotY(), 0f);

        mid.localScale = new Vector3(1f, CalculateMidScaleY(), 0f);
        mid.localPosition = new Vector3(0f, CalculateMidY(), 0f);
    }

    float CaculateSATopY() {
        return Camera.main.orthographicSize - safeAreaTopShiftValue + onePartHeight;
    }

    float CaculateSABotY() {
        return -Camera.main.orthographicSize + safeAreaBotShiftValue - onePartHeight;
    }

    float CalculateMidScaleY()
    {
        float freeScreenHeight = Camera.main.orthographicSize * 2f - onePartHeight * 2f - (safeAreaBotShiftValue + safeAreaTopShiftValue);

        return freeScreenHeight / onePartHeight;
    }

    float CalculateMidY() {
        float freeScreenHeight = Camera.main.orthographicSize * 2f - onePartHeight * 2f - (safeAreaBotShiftValue + safeAreaTopShiftValue);
        return CalculateTopY() - onePartHeight / 2f - freeScreenHeight / 2f;
    }

    float CalculateBotY()
    {
        return -Camera.main.orthographicSize + onePartHeight / 2f + safeAreaBotShiftValue;
    }

    float CalculateTopY()
    {
        return Camera.main.orthographicSize - onePartHeight / 2f - safeAreaTopShiftValue;
    }
}
