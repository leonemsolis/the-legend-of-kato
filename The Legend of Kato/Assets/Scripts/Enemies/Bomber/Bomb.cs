using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    string ANIMATION_EXPLODE = "Bomb_explode";
    [SerializeField] EnemyHitBox hitBoxPrefab;
    [SerializeField] AudioClip hitSound;
    AudioSource audioSource;
    Animator animator;
    Rigidbody2D rb;
    EnemyHitBox hitBox;

    float explodeTimer = 1f;
    float lifeTimer = 2f;

    bool settedUp = false;
    bool collided = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        settedUp = true;
        hitBox = Instantiate(hitBoxPrefab);
        hitBox.SetEnemy(gameObject.transform, new Vector2(0f, -.12f), new Vector2(.50f, .50f), true);

        SetUpMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == C.PlayerTag || collision.collider.tag == C.BootsTag ||
        collision.collider.tag == C.SwordTag || collision.collider.tag == C.BodyTag) 
        {
            if (!collided)
            {
                Explode();
            }
        }
    }

    private void FixedUpdate()
    {
        if(collided)
        {
            if(explodeTimer > 0)
            {
                explodeTimer -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if(lifeTimer > 0)
            {
                lifeTimer -= Time.deltaTime;
            }
            else
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
        audioSource.clip = hitSound;
        audioSource.Play();
        collided = true;
        animator.Play(ANIMATION_EXPLODE);
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        GetComponent<CircleCollider2D>().enabled = false;
        Destroy(hitBox.gameObject);
    }

    private void SetUpMovement()
    {
        Bomber_controller bc = transform.parent.gameObject.GetComponent<Bomber_controller>();
        bool left = bc.IsTurnedLeft();
        float forceX = left ? -bc.GetForceX() : bc.GetForceX();
        rb.AddForce(new Vector2(forceX, bc.GetForceY()), ForceMode2D.Impulse);
    }

}
