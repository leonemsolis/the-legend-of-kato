﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleImageScaler : MonoBehaviour
{
    Transform top, bot;
    const float topPartHeight = 675f;
    const float botPartHeight = 100f;
    const float buttonsPanelHeight = 250f;

    void Start()
    {
        top = transform.GetChild(0);
        bot = transform.GetChild(1);

        top.localPosition = new Vector3(0f, CalculateTopY(), 0f);

        bot.localScale = new Vector3(1f, CalculateBotScaleY(), 0f);
        bot.localPosition = new Vector3(0f, CalculateBotY(), 0f);
    }

    float CalculateBotScaleY()
    {
        float freeSpace = (Camera.main.orthographicSize * 2f - buttonsPanelHeight) / 2f + buttonsPanelHeight - topPartHeight / 2f;

        return freeSpace / botPartHeight;
    }

    float CalculateBotY()
    {
        float freeSpace = (Camera.main.orthographicSize * 2f - buttonsPanelHeight) / 2f + buttonsPanelHeight - topPartHeight / 2f;

        return -Camera.main.orthographicSize + freeSpace / 2f;
    }

    float CalculateTopY()
    {
        float freeSpace = Camera.main.orthographicSize * 2f - buttonsPanelHeight;
        float centerYInFreeSpace = freeSpace / 2f;

        return -Camera.main.orthographicSize + buttonsPanelHeight + centerYInFreeSpace;
    }
}