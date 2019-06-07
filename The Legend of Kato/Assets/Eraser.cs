using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eraser : MonoBehaviour
{
    PlayerController player;
    PlatformGenerator generator;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        generator = FindObjectOfType<PlatformGenerator>();
    }

    void Update()
    {

        transform.position = new Vector3(player.transform.position.x - 3000f, player.transform.position.y, 0f);    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
