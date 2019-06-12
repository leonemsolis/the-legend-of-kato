using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boots : MonoBehaviour
{
    Transform player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    private void FixedUpdate()
    {
        transform.position = player.position;

        Vector2 originCenter = transform.position + new Vector3(0f, -50f, 0f);
        Vector2 originLeft = transform.position + new Vector3(-50f, -50f, 0f);
        Vector2 originRight = transform.position + new Vector3(50f, -50f, 0f);

        Vector2 direction = Vector2.down;
        float distance = 35f;

        LayerMask collisionMask = 1 << LayerMask.NameToLayer("EnemyHitBox");

        RaycastHit2D hitEnemyCenter = Physics2D.Raycast(originCenter, direction, distance, collisionMask);
        RaycastHit2D hitEnemyLeft = Physics2D.Raycast(originCenter, direction, distance, collisionMask);
        RaycastHit2D hitEnemyRight = Physics2D.Raycast(originCenter, direction, distance, collisionMask);

        if (hitEnemyCenter)
        {
            hitEnemyCenter.collider.GetComponent<EnemyHitBox>().Die(true);
        }
        else if (hitEnemyLeft)
        {
            hitEnemyLeft.collider.GetComponent<EnemyHitBox>().Die(true);
        }
        else if (hitEnemyRight)
        {
            hitEnemyRight.collider.GetComponent<EnemyHitBox>().Die(true);
        }

        //Debug.DrawLine(originCenter, originCenter + direction * distance, Color.red);
        //Debug.DrawLine(originLeft, originLeft + direction * distance, Color.red);
        //Debug.DrawLine(originRight, originRight + direction * distance, Color.red);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyHitBox")
        {
            collision.GetComponent<EnemyHitBox>().Die(true);
        }
    }
}
