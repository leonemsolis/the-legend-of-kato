using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEraser : MonoBehaviour
{
    PlayerController player;
    const float eraseDistance = 2000f;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, player.gameObject.transform.position.y + eraseDistance, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
