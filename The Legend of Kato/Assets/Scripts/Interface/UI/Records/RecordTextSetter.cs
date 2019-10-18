﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecordTextSetter : MonoBehaviour
{
    enum TextBoxType {TUTORIAL, ENTRANCE, DEPTHS, OCEANBED, TOTALTIME, DEATH, SOULS, MONSTERS};
    [SerializeField] TextBoxType type;

    void Start()
    {
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();       
        switch(type)
        {
            case TextBoxType.TUTORIAL:
                tmp.SetText("BEST TIME:\n" + convertToMSMS(PlayerPrefs.GetFloat(C.PREFS_TUTORIAL_BEST_TIME, 0f)));
                break;
            case TextBoxType.ENTRANCE:
                tmp.SetText("BEST TIME:\n" + convertToMSMS(PlayerPrefs.GetFloat(C.PREFS_ENTRANCE_BEST_TIME, 0f)));
                break;
            case TextBoxType.DEPTHS:
                tmp.SetText("BEST TIME:\n" + convertToMSMS(PlayerPrefs.GetFloat(C.PREFS_DEPTHS_BEST_TIME, 0f)));
                break;
            case TextBoxType.OCEANBED:
                tmp.SetText("BEST TIME:\n" + convertToMSMS(PlayerPrefs.GetFloat(C.PREFS_OCEAN_BED_BEST_TIME, 0f)));
                break;
            case TextBoxType.TOTALTIME:
                tmp.SetText(convertToHMS(PlayerPrefs.GetFloat(C.PREFS_TOTAL_TIME, 0f)));
                break;
            case TextBoxType.DEATH:
                tmp.SetText(""+PlayerPrefs.GetInt(C.PREFS_DEATH_COUNT, 0));
                break;
            case TextBoxType.SOULS:
                tmp.SetText("" + PlayerPrefs.GetInt(C.PREFS_SOULS_COLLECTED, 0));
                break;
            case TextBoxType.MONSTERS:
                tmp.SetText("" + PlayerPrefs.GetInt(C.PREFS_MONSTERS_KILLED, 0));
                break;
        }
    }

    private string convertToMSMS(float time)
    {
        int m = (int)(time / 60f);
        time -= m * 60f;
        int s = (int)time;
        time -= s;
        int ms = (int)Mathf.Round(time * 100f);
        return m+"M : "+s+"S: "+ms+"MS";
    }

    private string convertToHMS(float time)
    {
        int h = (int)(time / 3600f);
        time -= h * 3600f;
        int m = (int)(time / 60f);
        time -= m * 60f;
        int s = (int)time;
        return h + "H : " + m + "M: " + s + "S";
    }
}
