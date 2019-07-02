using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightBlink : MonoBehaviour
{
    Light2D light2d;
    const float blinkTime = .3f;
    float timer = 0f;
    bool bright = true;
    float startTimeBright = 0f;

    void Start()
    {
        light2d = GetComponent<Light2D>();
    }

    void Update()
    {
        Debug.Log("UPDATEEE");
        if(timer < 0)
        {
            timer = blinkTime;
            startTimeBright = Time.time;
            bright = !bright;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (bright)
        {

            light2d.intensity = Mathf.SmoothStep(1f, .3f, (Time.time - startTimeBright) / .5f);
        }
        else
        {
            light2d.intensity = Mathf.SmoothStep(.3f, 1f, (Time.time - startTimeBright) / .5f);
        }
    }
}
