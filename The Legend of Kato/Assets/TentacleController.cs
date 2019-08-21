using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleController : MonoBehaviour
{

    enum State { APPEAR, ATTACK, DAMAGED, DISAPPEAR};
    private const string IDLE_ANIM = "appear";
    private const string ATTACK_ANIM = "attack";

    private Animator animator;
    private State state;

    private const float finalY = -200f;
    private const float startY = -570f;
    private const float speed = 200f;

    private float attackTimer = .7f;

    void Start()
    {
        state = State.APPEAR;
        animator = GetComponent<Animator>();   
    }


    void Update()
    {
        switch(state)
        {
            case State.APPEAR:
                if(transform.localPosition.y < finalY)
                {
                    transform.localPosition += new Vector3(0f, speed * Time.deltaTime, 0f);
                }
                else
                {
                    state = State.ATTACK;
                }
                break;
            case State.ATTACK:
                animator.Play(ATTACK_ANIM);
                if(attackTimer > 0)
                {
                    attackTimer -= Time.deltaTime;
                }
                else
                {
                    animator.Play(IDLE_ANIM);
                    state = State.DISAPPEAR;
                }
                break;
            case State.DAMAGED:
                break;
            case State.DISAPPEAR:
                if (transform.localPosition.y > startY)
                {
                    transform.localPosition += new Vector3(0f, -speed * Time.deltaTime, 0f);
                }
                else
                {
                    Destroy(gameObject);
                }
                break;
        }
    }


}
