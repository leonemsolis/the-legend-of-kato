using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleTextPlacer : MonoBehaviour
{
    const float buttonsPanelHeight = 250f;
    const float topPartHeight = 675f;

    bool down = true;
    const float travelTime = 1.5f;
    const float speed = 30f;
    float timer = travelTime;
    const float awaitTime = .2f;
    float awaitTimer = awaitTime;

    bool running = true;

    private void Start()
    {
        float topGap = ((Camera.main.orthographicSize * 2f - buttonsPanelHeight) / 2f - topPartHeight / 2f) / 2f;
        transform.localPosition = new Vector3(0f, Camera.main.orthographicSize - topGap, 0f);
    }

    void Update()
    {
        if(running)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                if (down)
                {
                    transform.localPosition -= new Vector3(0f, speed * Time.deltaTime, 0f);
                }
                else
                {
                    transform.localPosition += new Vector3(0f, speed * Time.deltaTime, 0f);
                }
            }
            else
            {
                if (awaitTimer > 0)
                {
                    awaitTimer -= Time.deltaTime;
                }
                else
                {
                    timer = travelTime;
                    down = !down;
                    awaitTimer = awaitTime;
                }
            }
        }
    }

    public void Stop()
    {
        running = false;
    }
}
