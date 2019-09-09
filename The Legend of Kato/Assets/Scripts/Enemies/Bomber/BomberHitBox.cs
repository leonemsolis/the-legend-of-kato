using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberHitBox : EnemyHitBox
{
    private Bomber_controller bomber;

    public override void Die(bool hit)
    {
        GetComponent<BoxCollider2D>().enabled = false;
        //dead = true;

        if (coin)
        {
            Instantiate(soulPrefab, transform.position, Quaternion.identity);
        }
        if (hit)
        {
            audioSource.clip = hitSound;
            audioSource.Play();
        }
        if (enemyTransform.gameObject != null)
        {
            bomber.TakeHit();
        }
        Destroy(gameObject, hitSound.length + 1f);
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
        audioSource = GetComponent<AudioSource>();
    }
}
