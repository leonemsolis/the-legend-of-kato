using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfish : MonoBehaviour
{
    [SerializeField] EnemyHitBox myHitBox;
    [SerializeField] Sprite left;
    [SerializeField] Sprite right;

    Vector3 direction = Vector3.zero;
    Vector3 startPoint;
    float yAxis = 0f;
    const float velocityX = 300f;
    const float verticalAmplitude = 200f;
    const float slower = 120f;
    const float moveDistance = 1400f;
    float startX;
    float deltaX = 0;

    void Start()
    {
        startPoint = transform.position;
        yAxis = transform.position.y;
        startX = transform.position.x;
        EnemyHitBox h = Instantiate(myHitBox, transform.position, Quaternion.identity);
        h.SetEnemy(transform, new Vector2(-0.25f, 0f), new Vector2(0.5f, .88f));
    }

    void Update()
    {
        deltaX += velocityX * direction.x * Time.deltaTime;
        transform.position = new Vector3(startX + deltaX, yAxis + Mathf.Sin(deltaX / slower) * verticalAmplitude, transform.position.z);
        if(Vector3.Distance(startPoint, transform.position) > moveDistance)
        {
            Destroy(gameObject);
        }
    }

    public void StartMovement(bool movingRight)
    {
        GetComponent<SpriteRenderer>().sprite = movingRight ? right : left;
        direction = movingRight ? Vector3.right : Vector3.left;
        GetComponent<Animator>().SetBool("moving_right", movingRight);
    }

}
