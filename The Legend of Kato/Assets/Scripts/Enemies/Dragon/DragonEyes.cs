using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEyes : MonoBehaviour
{
    float leftX = -6.2f;
    float rightY = 6.2f;
    float bottomY = -6.4f;
    float topY = 13f;

    PlayerController player;


    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        float deltaPlayerX = player.transform.position.x - transform.position.x;
        float deltaPlayerY = player.transform.position.y - transform.position.y;
        transform.localPosition = new Vector3(deltaPlayerX / 1000f * 12.4f, deltaPlayerY / 400f * 10f + 12f, 0f);
    }
}
