using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    Transform player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        transform.position = player.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyHitBox")
        {
            FindObjectOfType<Health>().TakeDamage();
        }
        if(collision.tag == "Projectile")
        {
            FindObjectOfType<Health>().TakeDamage();
        }
    }
}
