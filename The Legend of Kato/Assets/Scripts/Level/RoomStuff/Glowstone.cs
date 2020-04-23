using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Glowstone : MonoBehaviour
{
    const string shineAnimationName = "Shine";
    Animator animator;
    float shineTimer = 0;
    Light2D light2d;
    bool shining = false;
    bool bright = false;
    float startTimeBright = 0f;

    void Start()
    {
        light2d = GetComponent<Light2D>();
        animator = GetComponent<Animator>();
        ResetBlinkTimer();
    }


    void Update()
    {
        if(shineTimer > 0)
        {
            shineTimer -= Time.deltaTime;
        }
        else
        {
            if(!shining)
            {
                Blink();
            }
        }
        if (bright)
        {
            light2d.intensity = Mathf.SmoothStep(.5f, .7f, (Time.time - startTimeBright) / .5f);
            if ((Time.time - startTimeBright) / .3f > 1f)
            {
                bright = false;
                startTimeBright = Time.time;
            }
        }
        else
        {
            light2d.intensity = Mathf.SmoothStep(.7f, .5f, (Time.time - startTimeBright) / .5f);
        }
    }

    public void ResetBlinkTimer()
    {
        shineTimer = Random.Range(2, 6);
        shineTimer *= .8f;
        shineTimer += .3f;
    }

    private void Blink()
    {
        shining = true;
        bright = true;
        animator.Play(shineAnimationName);
        startTimeBright = Time.time;
        StartCoroutine(ResetLight());
    }

    private IEnumerator ResetLight()
    {
        yield return new WaitForSeconds(1f);
        shining = false;
        ResetBlinkTimer();
    }
}
