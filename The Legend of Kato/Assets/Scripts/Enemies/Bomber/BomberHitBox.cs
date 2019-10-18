using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberHitBox : EnemyHitBox
{
    private Bomber_controller bomber;

    public override void Die(bool hit)
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
            }

            if (PlayerPrefs.GetInt(C.PREFS_PRACTICE_MODE, 0) == 1 && enemyTransform.gameObject.GetComponent<Respawnable>() != null)
            {
                bomber.TakeHit(true);
                enemyTransform.gameObject.GetComponent<Respawnable>().Die();
                enemyTransform.gameObject.GetComponent<SpriteRenderer>().color = new Color(enemyTransform.gameObject.GetComponent<SpriteRenderer>().color.r, enemyTransform.gameObject.GetComponent<SpriteRenderer>().color.g, enemyTransform.gameObject.GetComponent<SpriteRenderer>().color.b, .5f);
                StartCoroutine(Respawn());
            }
            else
            {
                if (enemyTransform.gameObject != null)
                {
                    bomber.TakeHit(false);
                }
                FindObjectOfType<RecordTracker>().MonsterKill();
                Destroy(gameObject, hitSound.length + 1f);
            }
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        enemyTransform.gameObject.GetComponent<SpriteRenderer>().color = new Color(enemyTransform.gameObject.GetComponent<SpriteRenderer>().color.r, enemyTransform.gameObject.GetComponent<SpriteRenderer>().color.g, enemyTransform.gameObject.GetComponent<SpriteRenderer>().color.b, 1f);
        enemyTransform.gameObject.GetComponent<Respawnable>().Respawn();
        dead = false;
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void Disable()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void Enable()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void SetEnemy(Bomber_controller bc, Vector2 offset, Vector2 size)
    {
        bomber = bc;
        enemyTransform = bomber.gameObject.transform;
        GetComponent<BoxCollider2D>().offset = offset;
        GetComponent<BoxCollider2D>().size = size;
        coin = true;
    }
}
