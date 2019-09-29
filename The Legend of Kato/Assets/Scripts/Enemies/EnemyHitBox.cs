using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour 
{
    protected Transform enemyTransform;
    [SerializeField] protected Smoke smoke;
    [SerializeField] protected  GameObject soulPrefab;
    [SerializeField] protected AudioClip hitSound;
    //bool dead = false;
    protected bool coin;

    private void FixedUpdate()
    {
        if(enemyTransform != null)
        {
            transform.position = enemyTransform.position;
        }
    }

    public virtual void Die(bool hit)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        //dead = true;
        if(coin)
        {
            Instantiate(soulPrefab, transform.position, Quaternion.identity);
        }
        if(hit)
        {
            FindObjectOfType<SoundPlayer>().PlaySound(hitSound, transform.position);
            Instantiate(smoke, transform.position, Quaternion.identity);
        }
        if(enemyTransform.gameObject != null)
        {
            Destroy(enemyTransform.gameObject);
        }
        Destroy(gameObject, hitSound.length + 1f);
    }

    public void SetEnemy(Transform enemy, Vector2 offset, Vector2 size, bool dropCoin)
    {
        enemyTransform = enemy;
        GetComponent<BoxCollider2D>().offset = offset;
        GetComponent<BoxCollider2D>().size = size;
        coin = dropCoin;
    }

    public void ChangeBox(Vector2 offset, Vector2 size)
    {
        GetComponent<BoxCollider2D>().offset = offset;
        GetComponent<BoxCollider2D>().size = size;
    }

    public string GetInfo()
    {
        return enemyTransform.position.ToString();
    }
}
