using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleHitBox : EnemyHitBox
{
    private TentacleController tentacleController;

    public override void Die(bool hit)
    {
        if(!dead)
        {
            dead = true;
            GetComponent<BoxCollider2D>().enabled = false;
            if (hit)
            {
                FindObjectOfType<SoundPlayer>().PlaySound(hitSound, transform.position);
            }
            if (enemyTransform.gameObject != null)
            {
                tentacleController.TakeDamage();
            }
            Destroy(gameObject, hitSound.length + 1f);
        }
    }

    public void Disable()
    {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void Enable()
    {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    public void SetEnemy(TentacleController tc, Vector2 offset, Vector2 size)
    {
        tentacleController = tc;
        enemyTransform = tentacleController.gameObject.transform;
        GetComponent<BoxCollider2D>().offset = offset;
        GetComponent<BoxCollider2D>().size = size;
        coin = false;
    }
}
