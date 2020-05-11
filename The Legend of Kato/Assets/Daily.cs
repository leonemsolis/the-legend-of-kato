using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Daily : MonoBehaviour
{
    private SpriteRenderer renderer;
    private IEnumerator blink;
    private bool blinking = false;
    private bool black = false;

    public const int AwaitTime = 43200;

    private void Start() {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(Ready()) {
            if(!blinking) {
                blinking = true;
                blink = Blink();
                StartCoroutine(blink);
            }
        } else {
            if(blinking) {
                blinking = false;
                StopCoroutine(blink);
                renderer.color = Color.white;
            }
        }
    }

    private IEnumerator Blink() {
        if(black) {
            renderer.color = Color.white;
        } else {
            renderer.color = new Color(0.58f, 0.23f, 0.23f, 1f);
        }
        black = !black;
        yield return new WaitForSeconds(.6f);
        blink = Blink();
        StartCoroutine(blink);
    }

    public bool Ready() {
        string lastTime = PlayerPrefs.GetString(C.PREFS_LAST_DAILY, "NO");
        if(lastTime == "NO") {
            return true;
        }
        TimeSpan difference = DateTime.Now.Subtract(DateTime.Parse(lastTime));
        if(difference.TotalSeconds > AwaitTime) {
            return true;
        }
        return false;
    }

    public double SecondsUntilReady() {
        string lastTime = PlayerPrefs.GetString(C.PREFS_LAST_DAILY, "NO");
        if(lastTime == "NO") {
            return 0f;
        }
        return AwaitTime - DateTime.Now.Subtract(DateTime.Parse(lastTime)).TotalSeconds;
    }
}
