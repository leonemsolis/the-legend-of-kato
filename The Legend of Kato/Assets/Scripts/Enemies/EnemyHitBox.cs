using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Transform enemyTransform;
    bool facingRight = true;
    bool dead = false;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        if(enemyTransform != null)
        {
            transform.position = enemyTransform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!dead)
        {
            if (collision.gameObject.tag == "Sword")
            {
                Die();
            }
            if (collision.gameObject.tag == "Boots")
            {
                Die();
            }
            if (collision.gameObject.tag == "Body")
            {
                FindObjectOfType<Health>().TakeDamage();
            }
        }
    }

    private void Die()
    {
        FindObjectOfType<ScoreBoardText>().AddScore(Random.Range(1, 3));
        dead = true;
        Destroy(enemyTransform.gameObject);
        Destroy(gameObject);
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
