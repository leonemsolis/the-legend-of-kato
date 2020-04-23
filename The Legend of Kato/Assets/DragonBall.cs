using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBall : MonoBehaviour
{
    [SerializeField] Material enemy;
    [SerializeField] Material friendly;
    [SerializeField] AudioClip ballSound;
    [SerializeField] AudioClip hitSound;

    ParticleSystemRenderer psr;
    Rigidbody2D rb;

    bool dragonTurn = true;

    const float moveForce = 10000f;
    const float maxSpeed = 200f;

    void Start()
    {
        psr = GetComponent<ParticleSystemRenderer>();
        rb = GetComponent<Rigidbody2D>();

        FindObjectOfType<SoundPlayer>().PlaySound(ballSound);

        if(Random.Range(0, 2) == 0)
        {
            rb.velocity = new Vector2(-maxSpeed, -maxSpeed);
        }
        else
        {
            rb.velocity = new Vector2(maxSpeed, -maxSpeed);
        }
    }

    private void FixedUpdate()
    {
        if (rb.velocity.x > 0f)
        {
            rb.AddForce(Vector2.right * moveForce);
        }
        else
        {
            rb.AddForce(Vector2.left * moveForce);
        }


        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }


        if (rb.velocity.y > 0f)
        {
            rb.AddForce(Vector2.up * moveForce);
        }
        else
        {
            rb.AddForce(Vector2.down * moveForce);
        }


        if (Mathf.Abs(rb.velocity.y) > maxSpeed)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sign(rb.velocity.y) * maxSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case C.BodyTag:
                if(dragonTurn)
                {
                    FindObjectOfType<Health>().TakeDamage();
                    FindObjectOfType<DragonHeadController>().BallReturned();
                    Destroy(gameObject);
                }
                break;
            case C.SwordTag:
            case C.BootsTag:
                if(dragonTurn)
                {
                    dragonTurn = !dragonTurn;
                    psr.material = friendly;
                    FindObjectOfType<SoundPlayer>().PlaySound(hitSound);
                }
                break;
            case C.DragonTag:
                if(!dragonTurn)
                {
                    FindObjectOfType<DragonHeadController>().TakeDamage();
                    FindObjectOfType<DragonHeadController>().BallReturned();
                    FindObjectOfType<SoundPlayer>().PlaySound(hitSound);
                    Destroy(gameObject);
                }
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == C.BlockTag)
        {
            FindObjectOfType<SoundPlayer>().PlaySound(ballSound);
        }
    }
}
