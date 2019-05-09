using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    BoxCollider2D boxCollider;
    PlayerController player;
    Transform enemyTransform;
    bool facingRight = true;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        player = FindObjectOfType<PlayerController>();
    }

    private void FixedUpdate()
    {
        transform.position = enemyTransform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            Destroy(enemyTransform.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            player.TakeDamage();
        }
    }

    public void SetFacingRight(bool val)
    {
        facingRight = val;
    }

    public void SetEnemy(Transform enemy)
    {
        enemyTransform = enemy;
    }
}
