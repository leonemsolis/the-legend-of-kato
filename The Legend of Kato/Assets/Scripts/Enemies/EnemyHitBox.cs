using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour 
{
    Transform enemyTransform;
    [SerializeField] Smoke smoke;
    //bool dead = false;

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
        FindObjectOfType<ScoreBoardText>().AddScore(Random.Range(1, 3));
        Instantiate(smoke, transform.position, Quaternion.identity);
        //dead = true;
        Destroy(enemyTransform.gameObject);
        Destroy(gameObject);
    }

    public void SetEnemy(Transform enemy, Vector2 offset, Vector2 size)
    {
        enemyTransform = enemy;
        GetComponent<BoxCollider2D>().offset = offset;
        GetComponent<BoxCollider2D>().size = size;
	}

    public string GetInfo()
    {
        return enemyTransform.position.ToString();
    }
}
