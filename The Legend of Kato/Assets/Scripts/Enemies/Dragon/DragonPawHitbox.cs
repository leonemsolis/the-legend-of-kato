﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonPawHitbox : EnemyHitBox
{
    private DragonPaw dragonPaw;

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
                dragonPaw.TakeDamage();
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

    public void SetEnemy(DragonPaw dp, Vector2 offset, Vector2 size, bool left)
    {
        if(!left)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        dragonPaw = dp;
        enemyTransform = dragonPaw.gameObject.transform;
        GetComponent<BoxCollider2D>().offset = offset;
        GetComponent<BoxCollider2D>().size = size;
        coin = false;
    }
}
