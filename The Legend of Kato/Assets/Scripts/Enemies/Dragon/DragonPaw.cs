using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class DragonPaw : MonoBehaviour
{
    enum State { APPEAR, ATTACK, AWAIT, DISAPPEAR};
    State state = State.APPEAR;

    [SerializeField] DragonPawHitbox pawHitboxPrefab;
    DragonPawHitbox pawHitbox;

    Animator animator;
    const string PawAttackAnimationName = "Paw";
    const string PawDissapearAnimationName = "PawDisappear";

    GameObject hitboxHighlight;

    float attackTimer = .5f;
    float awaitTimer = 2f;

    const float destinationX = -200f;
    const float originX = -900f;

    private CircleCollider2D pawAttackHitbox;

    void Start()
    {
        animator = GetComponent<Animator>();
        pawAttackHitbox = GetComponent<CircleCollider2D>();
        pawAttackHitbox.enabled = false;
        hitboxHighlight = transform.GetChild(0).gameObject;
        hitboxHighlight.SetActive(false);
    }

    void Update()
    {
        switch(state)
        {
            case State.APPEAR:
                if (Mathf.Abs(destinationX - transform.localPosition.x) > 20)
                {
                    transform.position += new Vector3(10f, 0f, 0f);
                }
                else
                {
                    state = State.ATTACK;
                    animator.Play(PawAttackAnimationName);
                    pawAttackHitbox.enabled = true;
                }
                break;
            case State.ATTACK:
                if(attackTimer > 0)
                {
                    attackTimer -= Time.deltaTime;
                }
                else
                {
                    state = State.AWAIT;
                    pawHitbox = Instantiate(pawHitboxPrefab);
                    hitboxHighlight.SetActive(true);
                    pawHitbox.SetEnemy(this, new Vector2(1.7f, -1f), new Vector2(.5f, 2f));
                }
                break;
            case State.AWAIT:
                if(awaitTimer > 0)
                {
                    awaitTimer -= Time.deltaTime;
                }
                else
                {
                    Disappear();
                }
                break;
            case State.DISAPPEAR:
                if (Mathf.Abs(originX - transform.localPosition.x) > 20)
                {
                    transform.position -= new Vector3(30f, 0f, 0f);
                }
                else
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    public void Disappear()
    {
        hitboxHighlight.SetActive(false);
        state = State.DISAPPEAR;
        animator.Play(PawDissapearAnimationName);
        pawAttackHitbox.enabled = false;
    }

    public void TakeDamage()
    {
        if(state == State.AWAIT)
        {
            Disappear();
            // HEALTH -= 1;
        }
    }
}
