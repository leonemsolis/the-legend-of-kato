using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenSign : MonoBehaviour
{
    SpriteRenderer renderer;
    const float time = .3f;
    float timer = time;
    bool active = true;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        renderer.enabled = active;

        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = time;
            active = !active;
        }
    }
}
