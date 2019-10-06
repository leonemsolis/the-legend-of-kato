using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenSign : MonoBehaviour
{
    const float time = .3f;
    float timer = time;
    bool active = true;

    bool running = false;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        if(running)
        {
            GetComponent<SpriteRenderer>().enabled = active;

            if (timer > 0)
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

    public void Activate()
    {
        running = true;
        active = true;
        timer = time;
    }

    public void Deactivate()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        running = false;
    }
}
