using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleController : MonoBehaviour
{

    [SerializeField] TentacleHitBox hitBoxPrefab;

    enum State { APPEAR, ATTACK, AWAIT, DAMAGED, DISAPPEAR};
    private const string IDLE_ANIM = "appear";
    private const string ATTACK_ANIM = "attack";

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private State state;

    private const float finalY = -200f;
    private const float startY = -570f;
    private const float speed = 200f;

    private float attackTimer = .7f;
    private float awaitTimer = 1f;
    private float damagedTimer = 1f;

    private Color colorDamaged;
    private Color colorDefault;
    private bool flickDamaged = false;
    private const float flickPeriod = .3f;
    private IEnumerator flickCourutine;

    private BoxCollider2D platformAttackHitBox;
    private CapsuleCollider2D tentacleAttackHitBox;

    TentacleHitBox hitBox;



    void Start()
    {
        state = State.APPEAR;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        colorDefault = new Color(1f, 1f, 1f, 1f);
        colorDamaged = new Color(0.8867924f, 0.3973834f, 0.3973834f, 1f);

        platformAttackHitBox = GetComponent<BoxCollider2D>();
        tentacleAttackHitBox = GetComponent<CapsuleCollider2D>();

        platformAttackHitBox.enabled = false;
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
                    platformAttackHitBox.enabled = true;
                    tentacleAttackHitBox.enabled = false;
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
                    state = State.AWAIT;
                    platformAttackHitBox.enabled = false;

                    hitBox = Instantiate(hitBoxPrefab);
                    hitBox.SetEnemy(this, new Vector2(0f, 0f), new Vector2(1.1875f, 4.25f));
                }
                break;
            case State.AWAIT:
                if(awaitTimer > 0)
                {
                    awaitTimer -= Time.deltaTime;
                }
                else
                {
                    state = State.DISAPPEAR;
                }
                break;
            case State.DAMAGED:
                if(damagedTimer > 0)
                {
                    damagedTimer -= Time.deltaTime;
                }
                else
                {
                    StopCoroutine(flickCourutine);
                    spriteRenderer.color = colorDefault;
                    state = State.DISAPPEAR;
                }
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



    public void TakeDamage()
    {
        flickCourutine = Flicker();
        StartCoroutine(flickCourutine);
        state = State.DAMAGED;
        spriteRenderer.color = colorDamaged;
        FindObjectOfType<TentaclesController>().TakeDamage();
    }

    private IEnumerator Flicker()
    {
        yield return new WaitForSeconds(flickPeriod);
        flickDamaged = !flickDamaged;
        spriteRenderer.color = flickDamaged ? colorDamaged : colorDefault;
        flickCourutine = Flicker();
        StartCoroutine(flickCourutine);
    }
}
