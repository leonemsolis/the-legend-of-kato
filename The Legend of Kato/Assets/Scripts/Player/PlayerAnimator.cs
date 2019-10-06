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

    const string ANIMATION_WALK = "char_walk";
    const string ANIMATION_JUMP = "char_jump";
    const string ANIMATION_NO_SWORD_WALK = "char_nosword_move";
    const string ANIMATION_NO_SWORD_JUMP = "char_nosword_jump";
    const string ANIMATION_PICKUP = "char_sword_pickup";
    const string ANIMATION_DEATH = "char_death";
    const string ANIMATION_WIN = "char_win";

    Health health;

    bool dead = false;

    bool hasSword = true;

    bool win = false;

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
        health = FindObjectOfType<Health>();
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
                    animator.Play(ANIMATION_WALK);
                }
                else
                {
                    animator.Play(ANIMATION_NO_SWORD_WALK);
                }
            }
            else
            {
                if (hasSword)
                {
                    animator.Play(ANIMATION_JUMP);
                }
                else
                {
                    animator.Play(ANIMATION_NO_SWORD_JUMP);
                }
            }
        }
    }

    public void Die()
    {
        if(!dead)
        {
            animator.Play(ANIMATION_DEATH);
            player.GetComponent<SpriteRenderer>().color = Color.white;
            FindObjectOfType<PauseButton>().gameObject.SetActive(false);
            player.CanMove = false;
            sword.gameObject.SetActive(false);
            dead = true;
        }
    }

    public void Win()
    {
        win = true;
        animator.Play(ANIMATION_WIN);
        sword.gameObject.SetActive(false);
        player.CanMove = false;
        player.GetComponent<SpriteRenderer>().color = Color.white;
        FindObjectOfType<PauseButton>().gameObject.SetActive(false);
        StartCoroutine(CountdownWin());
    }

    public bool Won()
    {
        return win;
    }


    private IEnumerator CountdownWin()
    {
        yield return new WaitForSeconds(6f);
        if (FindObjectOfType<Blackout>() != null)
        {
            FindObjectOfType<Blackout>().LoadScene(C.MainMenuSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(C.MainMenuSceneIndex);
        }
    }


    // Called from animation
    public void EndGame()
    {
        if(FindObjectOfType<Blackout>() != null)
        {
            FindObjectOfType<Blackout>().LoadScene(C.DeathSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(C.DeathSceneIndex);
        }
    }
    public void StartPickupSword()
    {
        player.CanMove = false;
        animator.Play(ANIMATION_PICKUP);
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
