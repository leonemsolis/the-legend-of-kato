using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour 
{
    protected Transform enemyTransform;
    [SerializeField] protected Smoke smoke;
    [SerializeField] protected  GameObject soulPrefab;
    [SerializeField] protected AudioClip hitSound;
    protected bool coin;
    protected bool dead = false;

    private void Update()
    {
        if(enemyTransform != null)
        {
            transform.position = enemyTransform.position;
        }
    }

    public virtual void Die(bool hit)
    {
        if(!dead)
        {
            dead = true;
            GetComponent<BoxCollider2D>().enabled = false;
            if (coin)
            {
                Instantiate(soulPrefab, transform.position, Quaternion.identity);
            }
            if (hit)
            {
                FindObjectOfType<SoundPlayer>().PlaySound(hitSound, transform.position);
                Instantiate(smoke, transform.position, Quaternion.identity);
            }
            if (PlayerPrefs.GetInt(C.PREFS_PRACTICE_MODE, 0) == 1 && enemyTransform.gameObject.GetComponent<Respawnable>() != null)
            {
                enemyTransform.gameObject.GetComponent<Respawnable>().Die();
                enemyTransform.gameObject.GetComponent<SpriteRenderer>().color = new Color(enemyTransform.gameObject.GetComponent<SpriteRenderer>().color.r, enemyTransform.gameObject.GetComponent<SpriteRenderer>().color.g, enemyTransform.gameObject.GetComponent<SpriteRenderer>().color.b, .5f);
                StartCoroutine(Respawn());
            }
            else
            {
                if (enemyTransform.GetComponent<Jellyfish>() == null)
                {
                    FindObjectOfType<RecordTracker>().MonsterKill();
                }
                if (enemyTransform.gameObject != null)
                {
                    Destroy(enemyTransform.gameObject);
                }
                Destroy(gameObject, hitSound.length + 1f);
            }
        }
    }


    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        dead = false;
        enemyTransform.gameObject.GetComponent<SpriteRenderer>().color = new Color(enemyTransform.gameObject.GetComponent<SpriteRenderer>().color.r, enemyTransform.gameObject.GetComponent<SpriteRenderer>().color.g, enemyTransform.gameObject.GetComponent<SpriteRenderer>().color.b, 1f);
        enemyTransform.gameObject.GetComponent<Respawnable>().Respawn();
        GetComponent<BoxCollider2D>().enabled = true;
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
