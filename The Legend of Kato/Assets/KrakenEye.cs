using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenEye : MonoBehaviour
{

    float localOriginX = -15.1f;
    float localOriginY = -16.2f;

    float shiftedLocalOriginX = 58.4f;
    float shiftedLocalOriginY = -171f;

    float leftX = -42.5f;
    float rightY = 12.3f;
    float bottomY = -31.9f;
    float topY = -.5f;

    PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }


    void Update()
    {
        float deltaPlayerX = player.transform.position.x - transform.position.x;
        float deltaPlayerY = player.transform.position.y - transform.position.y;
        transform.localPosition = new Vector3(deltaPlayerX / 1000f * 30f - 15.1f, deltaPlayerY / 400f * 31.4f - 10f, 0f);
    }
}
