using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber_controller : MonoBehaviour
{
    enum State { IDLE, ATTACK, DEATH};

    [SerializeField] BomberHitBox hitBoxPrefab;
    [SerializeField] Bomb bombPrefab;
    [SerializeField] bool turnedLeft = false;
    [SerializeField] float xForce = 100f;
    [SerializeField] float yForce = 100f;
    BomberHitBox hitBox;

    State state = State.IDLE;
    Animator animator;

    string ANIMATION_ATTACK = "Bomber_attack";
    string ANIMATION_DEATH = "Bomber_death";
    string ANIMATION_IDLE = "Bomber_idle";

    const float ATTACK_TIME = .85f;
    const float DEATH_TIME = 1.2f;
    const float ATTACK_PERIOD = 3f;

    float timer = ATTACK_PERIOD;


    void Start()
    {
        animator = GetComponent<Animator>();
        hitBox = Instantiate(hitBoxPrefab);
        hitBox.SetEnemy(this, new Vector2(0f, 0f), new Vector2(1f, 1f));

        if(!turnedLeft)
        {
            transform.localScale = new Vector3(-1f, 1f, 0f);
        }
    }


    private void FixedUpdate()
    {
        switch (state)
        {
            case State.IDLE:
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    timer = ATTACK_TIME;
                    animator.Play(ANIMATION_ATTACK);
                    state = State.ATTACK;
                }
                break;
            case State.ATTACK:
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    timer = ATTACK_PERIOD;
                    state = State.IDLE;
                    animator.Play(ANIMATION_IDLE);
                    Bomb b = Instantiate(bombPrefab, transform.position + new Vector3(turnedLeft ? -15f : 15f, 30f, 0f), Quaternion.identity);
                    b.transform.SetParent(transform);
                }
                break;
            case State.DEATH:
                if(timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    public void TakeHit()
    {
        // Push Player
        FindObjectOfType<PlayerController>().Push(4000f);
        timer = DEATH_TIME;
        state = State.DEATH;
        animator.Play(ANIMATION_DEATH);
    }

    public bool IsTurnedLeft()
    {
        return turnedLeft;
    }

    public float GetForceX()
    {
        return xForce;
    }

    public float GetForceY()
    {
        return yForce;
    }
}
