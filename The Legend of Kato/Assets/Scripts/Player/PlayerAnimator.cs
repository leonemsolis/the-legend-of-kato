using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] SwordController sword;
    PlayerController player;
    Animator animator;

    const int ANIMATION_RIGHT_WALK = 0;
    const int ANIMATION_LEFT_WALK = 1;
    const int ANIMATION_LEFT_JUMP = 2;
    const int ANIMATION_RIGHT_JUMP = 3;
    const int ANIMATION_NO_SWORD_RIGHT_WALK = 4;
    const int ANIMATION_NO_SWORD_LEFT_WALK = 5;
    const int ANIMATION_NO_SWORD_RIGHT_JUMP = 6;
    const int ANIMATION_NO_SWORD_LEFT_JUMP = 7;
    const int ANIMATION_PICKUP_RIGHT = 8;
    const int ANIMATION_PICKUP_LEFT = 9;

    bool hasSword = false;

    private void Awake()
    {
        player = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        if (!hasSword)
        {
            sword.gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        if (player.CanMove)
        {
            if (player.Grounded)
            {
                if (player.FacingRight)
                {
                    if (hasSword)
                    {
                        SetAnimation(ANIMATION_RIGHT_WALK);
                    }
                    else
                    {
                        SetAnimation(ANIMATION_NO_SWORD_RIGHT_WALK);
                    }
                }
                else
                {
                    if (hasSword)
                    {
                        SetAnimation(ANIMATION_LEFT_WALK);
                    }
                    else
                    {
                        SetAnimation(ANIMATION_NO_SWORD_LEFT_WALK);
                    }
                }
            }
            else
            {
                if (player.FacingRight)
                {
                    if (hasSword)
                    {
                        SetAnimation(ANIMATION_RIGHT_JUMP);
                    }
                    else
                    {
                        SetAnimation(ANIMATION_NO_SWORD_RIGHT_JUMP);
                    }
                }
                else
                {
                    if (hasSword)
                    {
                        SetAnimation(ANIMATION_LEFT_JUMP);
                    }
                    else
                    {
                        SetAnimation(ANIMATION_NO_SWORD_LEFT_JUMP);
                    }
                }
            }
        }
    }

    private void SetAnimation(int animation_index)
    {
        switch (animation_index)
        {
            case ANIMATION_LEFT_JUMP:
                animator.Play("Player_left_jump");
                break;
            case ANIMATION_RIGHT_JUMP:
                animator.Play("Player_right_jump");
                break;
            case ANIMATION_LEFT_WALK:
                animator.Play("Player_left_walk");
                break;
            case ANIMATION_RIGHT_WALK:
                animator.Play("Player_right_walk");
                break;
            case ANIMATION_PICKUP_LEFT:
                animator.Play("char_sword_pickup_left");
                break;
            case ANIMATION_PICKUP_RIGHT:
                animator.Play("char_sword_pickup_right");
                break;
            case ANIMATION_NO_SWORD_LEFT_JUMP:
                animator.Play("char_nosword_jump_left");
                break;
            case ANIMATION_NO_SWORD_RIGHT_JUMP:
                animator.Play("char_nosword_jump_right");
                break;
            case ANIMATION_NO_SWORD_LEFT_WALK:
                animator.Play("char_nosword_left");
                break;
            case ANIMATION_NO_SWORD_RIGHT_WALK:
                animator.Play("char_nosword_right");
                break;
        }
    }

    public void StartPickupSword()
    {
        player.CanMove = false;
        if (player.FacingRight)
        {
            SetAnimation(ANIMATION_PICKUP_RIGHT);
        }
        else
        {
            SetAnimation(ANIMATION_PICKUP_LEFT);
        }
        StartCoroutine(EndPickupSword());
    }

    private IEnumerator EndPickupSword()
    {
        yield return new WaitForSeconds(2.3f);
        player.CanMove = true;
        hasSword = true;
        sword.gameObject.SetActive(true);
    }
}
