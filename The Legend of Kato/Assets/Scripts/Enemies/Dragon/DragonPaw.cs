using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class DragonPaw : MonoBehaviour
{
    enum State { SPAWN, APPEAR, ATTACK, AWAIT, DISAPPEAR};
    State state = State.SPAWN;

    [SerializeField] DragonPawHitbox pawHitboxPrefab;
    [SerializeField] AudioClip appearSound;
    [SerializeField] AudioClip downSound;
    DragonPawHitbox pawHitbox;

    Animator animator;
    const string PawAttackAnimationName = "Paw";
    const string PawDissapearAnimationName = "PawDisappear";

    GameObject hitboxHighlight;

    float attackTimer = .5f;
    float awaitTimer = 2f;

    float destinationX = -200f;
    float originX = -900f;

    private CircleCollider2D pawAttackHitbox;

    bool left = true;

    public void Setup(bool l)
    {
        left = l;
        if(!left)
        {
            destinationX = 200f;
            originX = 900f;
            transform.localScale = new Vector3(-1f, 1f, 1f);

            FindObjectOfType<DragonSigns>().Enable(0);
            FindObjectOfType<DragonSigns>().Enable(1);
            FindObjectOfType<DragonSigns>().Enable(2);
        }
        else
        {
            FindObjectOfType<DragonSigns>().Enable(2);
            FindObjectOfType<DragonSigns>().Enable(3);
            FindObjectOfType<DragonSigns>().Enable(4);
        }

        transform.localPosition = new Vector3(originX, -314f, 0f);

        animator = GetComponent<Animator>();
        pawAttackHitbox = GetComponent<CircleCollider2D>();
        pawAttackHitbox.enabled = false;
        hitboxHighlight = transform.GetChild(0).gameObject;
        hitboxHighlight.SetActive(false);
        state = State.APPEAR;
        FindObjectOfType<SoundPlayer>().PlaySound(appearSound);
    }

    void Update()
    {
        switch(state)
        {
            case State.APPEAR:
                if (Mathf.Abs(destinationX - transform.localPosition.x) > 20)
                {
                    if(left)
                    {
                        transform.position += new Vector3(10f, 0f, 0f);
                    }
                    else
                    {
                        transform.position -= new Vector3(10f, 0f, 0f);
                    }
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
                    FindObjectOfType<SoundPlayer>().PlaySound(downSound);
                    state = State.AWAIT;
                    pawHitbox = Instantiate(pawHitboxPrefab);
                    pawHitbox.SetEnemy(this, new Vector2(1.7f, -1f), new Vector2(.5f, 2f), left);
                    pawHitbox.transform.parent = transform;
                    hitboxHighlight.SetActive(true);
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
                    FindObjectOfType<SoundPlayer>().PlaySound(appearSound);
                }
                break;
            case State.DISAPPEAR:
                if(pawHitbox != null)
                {
                    Destroy(pawHitbox.gameObject);
                }
                if (Mathf.Abs(originX - transform.localPosition.x) > 20)
                {
                    if(left)
                    {
                        transform.position -= new Vector3(30f, 0f, 0f);
                    }
                    else
                    {
                        transform.position += new Vector3(30f, 0f, 0f);
                    }
                }
                else
                {
                    FindObjectOfType<DragonHeadController>().AttackDone();
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
            FindObjectOfType<DragonHeadController>().TakeDamage();
        }
    }
}
