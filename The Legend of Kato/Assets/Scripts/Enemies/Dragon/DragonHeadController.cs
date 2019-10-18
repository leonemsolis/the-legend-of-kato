using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class DragonHeadController : MonoBehaviour
{
    enum MainState {AWAIT, SLEEP, AWAKE, ATTACK_START, ATTACK, PAUSE, DEAD};
    enum AttackState { STATE_1, STATE_2};

    [SerializeField] AudioClip dragonFire;

    [SerializeField] Sprite eyesClosed;
    [SerializeField] Sprite eyesOpenNoPupils;
    [SerializeField] Sprite fireHead;
    [SerializeField] DragonPaw dragonPawPrefab;
    [SerializeField] DragonFire dragonFirePrefab;

    DragonEyes dragonEyes;
    Animator animator;
    SpriteRenderer spriteRenderer;
    DragonSigns dragonSigns;
    const string ANIMATION_EYES_OPENING = "DragonEyesOpening";
    const string ANIMATION_EYES_DEAD = "Dead";

    float sleepTime = 1.5f;
    float eyesOpeningTime = .85f;

    float timer = 0f;
    const float pauseTime = 1f;

    PlayerRoomDetector PRD;
    MainState mainState;
    AttackState attackState;


    int health = 3;
    int fireCount = 1;

    private void Start()
    {
        PRD = FindObjectOfType<PlayerRoomDetector>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dragonEyes = FindObjectOfType<DragonEyes>();
        dragonEyes.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        mainState = MainState.AWAIT;
        attackState = AttackState.STATE_1;
        dragonSigns = FindObjectOfType<DragonSigns>();
    }

    void Update()
    {
        switch(mainState)
        {
            case MainState.AWAIT:
                if (PRD.GetCurrentRoom() != null && PRD.GetCurrentRoom().RoomID == C.RoomIDBoss)
                {
                    mainState = MainState.SLEEP;
                }
                break;
            case MainState.SLEEP:
                if(sleepTime > 0)
                {
                    sleepTime -= Time.deltaTime;
                }
                else
                {
                    mainState = MainState.AWAKE;
                    animator.Play(ANIMATION_EYES_OPENING);
                }
                break;
            case MainState.AWAKE:
                if(eyesOpeningTime > 0f)
                {
                    eyesOpeningTime -= Time.deltaTime;
                }
                else
                {
                    animator.enabled = false;
                    dragonEyes.gameObject.SetActive(true);
                    spriteRenderer.sprite = eyesOpenNoPupils;
                    mainState = MainState.PAUSE;
                    timer = pauseTime;
                }
                break;
            case MainState.PAUSE:
                if(timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    mainState = MainState.ATTACK_START;
                }
                break;
            case MainState.ATTACK_START:
                switch(attackState)
                {
                    case AttackState.STATE_1:
                        mainState = MainState.ATTACK;
                        fireCount--;
                        timer = pauseTime;
                        StartCoroutine(FireBreath());
                        break;
                    case AttackState.STATE_2:
                        mainState = MainState.ATTACK;
                        timer = pauseTime;
                        DragonPaw dragonPaw = Instantiate(dragonPawPrefab, transform.position, Quaternion.identity);
                        dragonPaw.transform.parent = transform;
                        dragonPaw.Setup(Random.Range(0, 2) == 0 ? true : false);
                        break;
                }
                break;
            case MainState.DEAD:
                if(!FindObjectOfType<PlayerAnimator>().Won())
                {
                    dragonEyes.gameObject.SetActive(false);
                    animator.enabled = true;
                    animator.Play(ANIMATION_EYES_DEAD);
                    FindObjectOfType<RecordTracker>().CompleteLevel();
                    FindObjectOfType<PlayerAnimator>().Win();
                }
                break;
        }
    }

    public IEnumerator FireBreath()
    {
        float rotationZ = 0f;
        switch (Random.Range(0, 4))
        {
            case 0:
                dragonSigns.Enable(0);
                dragonSigns.Enable(1);
                rotationZ = -159.418f;
                break;
            case 1:
                dragonSigns.Enable(1);
                dragonSigns.Enable(2);
                rotationZ = -129.405f;
                break;
            case 2:
                dragonSigns.Enable(2);
                dragonSigns.Enable(3);
                rotationZ = -91.39001f;
                break;
            case 3:
                dragonSigns.Enable(3);
                dragonSigns.Enable(4);
                rotationZ = -56.917f;
                break;
        }
        yield return new WaitForSeconds(1f);
        FindObjectOfType<SoundPlayer>().PlaySound(dragonFire);
        StartCoroutine(LightUpDown(true));
        Instantiate(dragonFirePrefab, transform.position, Quaternion.identity).Setup(rotationZ);
    }

    public void AttackDone()
    {
        if(attackState == AttackState.STATE_1)
        {
            StartCoroutine(LightUpDown(false));
            if (fireCount == 0)
            {
                attackState = AttackState.STATE_2;
            }
        }
        if(mainState != MainState.DEAD)
        {
            mainState = MainState.PAUSE;
        }
    }

    public void TakeDamage()
    {
        health--;
        if(health == 0)
        {
            mainState = MainState.DEAD;
        }
    }

    private IEnumerator LightUpDown(bool up)
    {
        if(up)
        {
            spriteRenderer.sprite = fireHead;
        }
        float elapsed = 0.0f;
        float i_start = up ? .2f : 1f;
        float i_end = up ? 1f : .2f;
        float duration = .5f;
        while (elapsed < duration)
        {
            GetComponent<Light2D>().intensity = Mathf.Lerp(i_start, i_end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        GetComponent<Light2D>().intensity = i_end;
        if (!up)
        {
            spriteRenderer.sprite = eyesOpenNoPupils;
        }
    }
}
