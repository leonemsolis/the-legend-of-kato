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


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == C.EnemyHitBoxTag)
        {
            FindObjectOfType<Health>().TakeDamage();
        }
        if (collision.tag == C.InvulnirableEnemyHitBoxTag)
        {
            FindObjectOfType<Health>().TakeDamage();
        }
        if(collision.tag == C.ProjectileTag) {
            if(!player.gameObject.GetComponent<PlayerController>().shieldActivated) {
                FindObjectOfType<Health>().TakeDamage();
            }
        } 
        if (collision.tag == C.EnemyTag)
        {
            FindObjectOfType<Health>().TakeDamage();
        }
        if (collision.tag == C.SpikesTag)
        {
            FindObjectOfType<Health>().TakeDamage();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
