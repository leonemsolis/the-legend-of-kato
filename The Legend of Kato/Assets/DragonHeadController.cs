using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonHeadController : MonoBehaviour
{
    enum MainState {AWAIT, SLEEP, AWAKE, ATTACK, PAUSE};
    enum AttackState { STATE_1, STATE_2, STATE_3 };


    [SerializeField] Sprite eyesClosed;
    [SerializeField] Sprite eyesOpenNoPupils;

    DragonEyes dragonEyes;
    Animator animator;
    SpriteRenderer spriteRenderer;
    const string ANIMATION_EYES_OPENING = "DragonEyesOpening";

    float sleepTime = 3f;
    float eyesOpeningTime = .85f;

    float timer = 0f;
    const float bigPauseTime = 4f;
    const float littlePauseTime = 2f;

    PlayerRoomDetector PRD;
    MainState mainState;
    AttackState attackState;


    void OnEnable()
    {
        PRD = FindObjectOfType<PlayerRoomDetector>();
        spriteRenderer = GetComponent<SpriteRenderer>();        
        dragonEyes = FindObjectOfType<DragonEyes>();
        dragonEyes.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        mainState = MainState.AWAIT;
        attackState = AttackState.STATE_1;
    }


    void Update()
    {
        switch(mainState)
        {
            case MainState.AWAIT:
                if (PRD.GetCurrentRoom().RoomID == C.RoomIDBoss)
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
                    timer = littlePauseTime;
                }
                break;
            case MainState.PAUSE:
                if(timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    mainState = MainState.ATTACK;
                }
                break;
            case MainState.ATTACK:
                switch(attackState)
                {
                    case AttackState.STATE_1:
                        //mainState = MainState.PAUSE;
                        //timer = bigPauseTime;
                        //attackState = AttackState.STATE_2;
                        break;
                    case AttackState.STATE_2:
                        break;
                    case AttackState.STATE_3:
                        break;
                }
                break;
        }
    }
}
