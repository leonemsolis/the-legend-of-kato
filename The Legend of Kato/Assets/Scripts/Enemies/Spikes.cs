using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyHitBox")
        {
            FindObjectOfType<Health>().TakeDamage();
        }
        if (collision.tag == "InvulnirableEnemyHitBox")
        {
            FindObjectOfType<Health>().TakeDamage();
        }
    }
}
