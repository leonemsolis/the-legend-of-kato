using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour 
{
    Transform enemyTransform;
    [SerializeField] Smoke smoke;
    [SerializeField] GameObject soulPrefab;
    [SerializeField] AudioClip hitSound;
    AudioSource audioSource;
    //bool dead = false;
    bool coin;

    private void FixedUpdate()
    {
        if(enemyTransform != null)
        {
            transform.position = enemyTransform.position;
        }
    }

    public void Die(bool hit)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        //dead = true;
        if(coin)
        {
            Instantiate(soulPrefab, transform.position, Quaternion.identity);
        }
        if(hit)
        {
            audioSource.clip = hitSound;
            audioSource.Play();
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
        audioSource = GetComponent<AudioSource>();
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
