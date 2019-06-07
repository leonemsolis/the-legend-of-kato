using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour 
{
    Transform enemyTransform;
    [SerializeField] Smoke smoke;
    [SerializeField] GameObject coinPrefab;
    //bool dead = false;
    bool coin;

    private void FixedUpdate()
    {
        if(enemyTransform != null)
        {
            transform.position = enemyTransform.position;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(!dead)
    //    {
    //        if (collision.gameObject.tag == "Sword")
    //        {
    //            Die();
    //        }
    //        if (collision.gameObject.tag == "Boots")
    //        {
    //            Die();
    //        }
    //        if (collision.gameObject.tag == "Body")
    //        {
    //            FindObjectOfType<Health>().TakeDamage();
    //        }
    //    }
    //}

    public void Die()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        Instantiate(smoke, transform.position, Quaternion.identity);
        //dead = true;
        if(coin)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        Destroy(enemyTransform.gameObject);
        Destroy(gameObject);
    }

    public void SetEnemy(Transform enemy, Vector2 offset, Vector2 size, bool dropCoin)
    {
        enemyTransform = enemy;
        GetComponent<BoxCollider2D>().offset = offset;
        GetComponent<BoxCollider2D>().size = size;
        coin = dropCoin;
	}

    public string GetInfo()
    {
        return enemyTransform.position.ToString();
    }
}
