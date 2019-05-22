using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
    BoxCollider2D boxCollider;
    Transform enemyTransform;
    bool facingRight = true;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        transform.position = enemyTransform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
            FindObjectOfType<ScoreBoardText>().AddScore(Random.Range(1, 3));
            Destroy(enemyTransform.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<Health>().TakeDamage();
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
