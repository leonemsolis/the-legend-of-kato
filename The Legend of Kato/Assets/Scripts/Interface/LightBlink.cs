using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class LightBlink : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] float blinkTime = 1f;
    float timer;
    bool brightPhase = true;
    UnityEngine.Experimental.Rendering.Universal.Light2D light2d;
    float startTimeBright = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        light2d = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
        timer = blinkTime;
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
            light2d.intensity = Mathf.SmoothStep(.7f, .5f, (Time.time - startTimeBright) / .5f);
        }
        else
        {
            light2d.intensity = Mathf.SmoothStep(.5f, .7f, (Time.time - startTimeBright) / .5f);
        }
    }
}
