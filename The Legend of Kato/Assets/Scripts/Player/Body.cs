using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    PlayerUpgrades playerUpgrades;
    Transform player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        playerUpgrades = FindObjectOfType<PlayerUpgrades>();
    }

    private void Update()
    {
        transform.position = player.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == C.EnemyHitBoxTag)
        {
            FindObjectOfType<Health>().TakeDamage();
        }
        if(collision.tag == C.InvulnirableEnemyHitBoxTag)
        {
            FindObjectOfType<Health>().TakeDamage();
        }
        if(collision.tag == C.SpikesTag)
        {
            if(playerUpgrades.GetCurrentUpgrade() != Upgrade.BOOTS)
            {
                FindObjectOfType<Health>().TakeDamage();
            }
        }
    }
}
