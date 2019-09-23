using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFrameScaler : MonoBehaviour
{
    Transform top, mid, bot;

    const float onePartHeight = 250f;

    void Start()
    {
        top = transform.GetChild(0);
        mid = transform.GetChild(1);
        bot = transform.GetChild(2);

        top.localPosition = new Vector3(0f, CalculateTopY(), 0f);
        bot.localPosition = new Vector3(0f, CalculateBotY(), 0f);

        mid.localScale = new Vector3(1f, CalculateMidScaleY(), 0f);
        mid.localPosition = new Vector3(0f, 0f, 0f);
    }

    private void Update()
    {
        top = transform.GetChild(0);
        mid = transform.GetChild(1);
        bot = transform.GetChild(2);

        top.localPosition = new Vector3(0f, CalculateTopY(), 0f);
        bot.localPosition = new Vector3(0f, CalculateBotY(), 0f);

        mid.localScale = new Vector3(1f, CalculateMidScaleY(), 0f);
        mid.localPosition = new Vector3(0f, 0f, 0f);
    }

    float CalculateMidScaleY()
    {
        float freeScreenHeight = Camera.main.orthographicSize * 2f - onePartHeight * 2f;

        return freeScreenHeight / onePartHeight;
    }

    float CalculateBotY()
    {
        return -Camera.main.orthographicSize + onePartHeight / 2f;
    }

    float CalculateTopY()
    {
        return Camera.main.orthographicSize - onePartHeight / 2f;
    }
}
