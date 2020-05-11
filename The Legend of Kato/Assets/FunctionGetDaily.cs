using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class FunctionGetDaily : FunctionUI
{
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] GameObject rewards;
    private FunctionCancelPrompt cancel;
    private Daily daily;
    private SpriteRenderer renderer;
    private bool setted = false;

    private string notReady;

    void Start()
    {
        notReady = FindObjectOfType<Translator>().GetTranslation("daily_not");
        FindObjectOfType<Translator>().SetFont(FontType.BLACK_WHITE, timeText);
        cancel = FindObjectOfType<FunctionCancelPrompt>();
        daily = FindObjectOfType<Daily>();
        renderer = GetComponent<SpriteRenderer>();
        setted = !daily.Ready();
    }

    private void Update() {
        if(!daily.Ready()) {
            timeText.SetText(notReady+"\n"+GetTime());
            if(setted) {
                rewards.SetActive(false);
                renderer.color = Color.gray;
                setted = false;
            }
        } else {
            if(!setted) {
                rewards.SetActive(true);
                renderer.color = Color.white;
                setted = true;
                timeText.SetText("");
            }
        }
    }
    // 51240s
    // 14

    private string GetTime() {
        double s = daily.SecondsUntilReady();
        int h = (int)(s / 3600);
        s -= h * 3600;
        int m = (int)(s / 60);
        s = (int)(s - m * 60);
        string hours = h > 9 ? h.ToString() : "0"+h.ToString();
        string minutes = m > 9 ? m.ToString() : "0"+m.ToString();
        string seconds = s > 9 ? s.ToString() : "0"+s.ToString();
        return hours+":"+minutes+":"+seconds;
    }

    public override void Function()
    {
        if(daily.Ready()) {
            PlayerPrefs.SetString(C.PREFS_LAST_DAILY, DateTime.Now.ToString());
            PlayerPrefs.Save();
            cancel.Function();
        }
    }
}
