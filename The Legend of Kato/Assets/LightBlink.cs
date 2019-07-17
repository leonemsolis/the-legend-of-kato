using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightBlink : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    const float blinkTime = .5f;
    float timer = blinkTime;
    bool brightPhase = true;
    Light2D light2d;
    float startTimeBright = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        light2d = GetComponent<Light2D>();
    }

    void Update()
    {
        if(timer < 0)
        {
            timer = blinkTime;
            startTimeBright = Time.time;
            brightPhase = !brightPhase;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (brightPhase)
        {
            light2d.intensity = Mathf.SmoothStep(1f, .3f, (Time.time - startTimeBright) / .5f);
        }
        else
        {
            light2d.intensity = Mathf.SmoothStep(.3f, 1f, (Time.time - startTimeBright) / .5f);
        }
    }
}
