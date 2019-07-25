using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WitchController : MonoBehaviour
{
    enum State { INTRO, IDLE, ATTACK, DAMAGE, DEATH, TPOUT, TPIN };
    // TODO: start shooting when player enters the room;
    // TODO: decreace animation
    [SerializeField] WitchProjectile projectile;
    [SerializeField] BossKey bossKey;
    List<Vector3> tpPoints;
    PlayerRoomDetector roomDetector;
    Animator animator;
    bool facingRight = false;
    State state;
    int lives = 5;

    const string IDLE_ANIMATION = "idle";
    const string ATTACK_ANIMATION = "attack";
    const string DEATH_ANIMATION = "death";
    const string DAMAGE_ANIMATION = "damage";
    const string TPIN_ANIMATION = "tpin";
    const string TPOUT_ANIMATION = "tpout";

    const float introTime = 1.5f;
    const float idleTime = .8f;
    const float attackTime = 1.5f;
    const float damageTime = 1f;
    const float tpTime = .6f;
    const float deathTime = 2f;
    const int tpPointsNumber = 6;
    int lastTpIndex = -1;

    float originX;

    float timer = 0;

    bool alive = false;


    void Start()
    {
        bossKey.gameObject.SetActive(false);
        roomDetector = FindObjectOfType<PlayerRoomDetector>();
        tpPoints = new List<Vector3>();
        for(int i = 0; i < tpPointsNumber; ++i)
        {
            tpPoints.Add(transform.GetChild(i).transform.position);
        }

        animator = GetComponent<Animator>();
        animator.Play(IDLE_ANIMATION);
        originX = transform.position.x;
        state = State.INTRO;
    }

    void Update()
    {
        if (roomDetector.InTheRoom() && roomDetector.GetCurrentRoom().RoomID == C.RoomIDBoss) 
        {
            alive = true;
        }
        if(alive)
        {
            switch (state)
            {
                case State.INTRO:
                    if (timer > introTime)
                    {
                        animator.Play(TPOUT_ANIMATION);
                        state = State.TPOUT;
                        timer = 0;
                    }
                    else
                    {
                        timer += Time.deltaTime;
                    }
                    break;
                case State.IDLE:
                    if (timer > idleTime)
                    {
                        animator.Play(ATTACK_ANIMATION);
                        state = State.ATTACK;
                        timer = 0;
                    }
                    else
                    {
                        timer += Time.deltaTime;
                    }
                    break;
                case State.ATTACK:
                    if (timer > attackTime)
                    {
                        animator.Play(IDLE_ANIMATION);
                        state = State.IDLE;
                        timer = 0;
                        Shoot();
                    }
                    else
                    {
                        timer += Time.deltaTime;
                    }
                    break;
                case State.DAMAGE:
                    if (timer > damageTime)
                    {
                        animator.Play(TPOUT_ANIMATION);
                        state = State.TPOUT;
                        timer = 0;
                    }
                    else
                    {
                        timer += Time.deltaTime;
                    }
                    break;
                case State.TPOUT:
                    if (timer > tpTime)
                    {
                        animator.Play(TPIN_ANIMATION);
                        state = State.TPIN;
                        timer = 0;
                        Teleport();
                    }
                    else
                    {
                        timer += Time.deltaTime;
                    }
                    break;
                case State.TPIN:
                    if (timer > tpTime)
                    {
                        animator.Play(IDLE_ANIMATION);
                        state = State.IDLE;
                        timer = 0;
                    }
                    else
                    {
                        timer += Time.deltaTime;
                    }
                    break;
                case State.DEATH:
                    if (timer > deathTime)
                    {
                        Die();
                    }
                    else
                    {
                        timer += Time.deltaTime;
                    }
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == C.SwordTag || collision.tag == C.BootsTag)
        {
            if(state == State.IDLE || state == State.ATTACK || state == State.INTRO || state == State.ATTACK)
            {
                if(lives > 0)
                {
                    animator.Play(DAMAGE_ANIMATION);
                    state = State.DAMAGE;
                    timer = 0;
                    lives--;
                }
                else
                {
                    animator.Play(DEATH_ANIMATION);
                    state = State.DEATH;
                    timer = 0;
                }
            }
        }
    }

    private void Teleport()
    {
        int tpIndex = -1;
        do
        {
            tpIndex = Random.Range(0, tpPointsNumber);
        }
        while (tpIndex == lastTpIndex);

        lastTpIndex = tpIndex;

        Vector3 newPosition = tpPoints[tpIndex];
        if(newPosition.x < originX)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        transform.position = newPosition;
    }

    private void Die()
    {
        bossKey.transform.position = transform.position;
        bossKey.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    private void Shoot()
    {
        float xPos = transform.localScale.x > 0 ? -37f : 37f;
        Instantiate(projectile, transform.position + new Vector3(xPos, -32f, 0f), Quaternion.identity);
    }
}
