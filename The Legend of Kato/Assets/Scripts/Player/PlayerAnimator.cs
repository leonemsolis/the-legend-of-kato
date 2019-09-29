using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] AudioClip swordPickUp;
    [SerializeField] SwordController sword;
    PlayerController player;
    Animator animator;

    const int ANIMATION_WALK = 0;
    const int ANIMATION_JUMP = 1;
    const int ANIMATION_NO_SWORD_WALK = 2;
    const int ANIMATION_NO_SWORD_JUMP = 3;
    const int ANIMATION_PICKUP = 4;

    bool hasSword = true;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();

        if(SceneManager.GetActiveScene().buildIndex == C.Level1SceneIndex || SceneManager.GetActiveScene().buildIndex == C.Level0SceneIndex)
        {
            hasSword = false;
        }

        if (!hasSword)
        {
            sword.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // Default sprites turned right;
        if (player.FacingRight)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (player.CanMove)
        {
            if (player.Grounded)
            {
                if (hasSword)
                {
                    SetAnimation(ANIMATION_WALK);
                }
                else
                {
                    SetAnimation(ANIMATION_NO_SWORD_WALK);
                }
            }
            else
            {
                if (hasSword)
                {
                    SetAnimation(ANIMATION_JUMP);
                }
                else
                {
                    SetAnimation(ANIMATION_NO_SWORD_JUMP);
                }
            }
        }
    }

    private void SetAnimation(int animation_index)
    {
        switch (animation_index)
        {
            case ANIMATION_JUMP:
                animator.Play("char_jump");
                break;
            case ANIMATION_WALK:
                animator.Play("char_walk");
                break;
            case ANIMATION_PICKUP:
                animator.Play("char_sword_pickup");
                break;
            case ANIMATION_NO_SWORD_JUMP:
                animator.Play("char_nosword_jump");
                break;
            case ANIMATION_NO_SWORD_WALK:
                animator.Play("char_nosword_move");
                break;
        }
    }

    public void StartPickupSword()
    {
        player.CanMove = false;
        SetAnimation(ANIMATION_PICKUP);
        StartCoroutine(EndPickupSword());
        StartCoroutine(PlayPickUpSound());
    }

    private IEnumerator PlayPickUpSound()
    {
        yield return new WaitForSeconds(1.4f);
        FindObjectOfType<SoundPlayer>().PlaySound(swordPickUp, transform.position);
    }

    private IEnumerator EndPickupSword()
    {
        yield return new WaitForSeconds(2.3f);
        FindObjectOfType<SwordHolder>().LightOff();
        player.CanMove = true;
        hasSword = true;
        sword.gameObject.SetActive(true);
    }
}
