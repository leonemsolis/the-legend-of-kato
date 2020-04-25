using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject tmp;
    [SerializeField] AudioClip hurtSound;
    public const int MAX_HEALTH = 5;
    int currentHealth;

    bool canTakeDamage = true;
    SpriteRenderer playerSpriteRenderer;
    Color emptyColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    Color fullColor = new Color(1f, 1f, 1f, 1f);

    bool practice;

    private void Awake()
    {
        playerSpriteRenderer = FindObjectOfType<PlayerController>().GetComponent<SpriteRenderer>();

        practice = PlayerPrefs.GetInt(C.PREFS_PRACTICE_MODE, 0) == 1;

        if (practice)
        {
            for (int i = 0; i < MAX_HEALTH - currentHealth; ++i)
            {
                transform.GetChild(MAX_HEALTH - i - 1).GetComponent<SpriteRenderer>().enabled = false;
            }
            currentHealth = MAX_HEALTH;
        }
        else
        {
            tmp.SetActive(false);

            currentHealth = PlayerPrefs.GetInt(C.PREFS_CURRENT_HEALTH, 5);


            for (int i = 0; i < MAX_HEALTH - currentHealth; ++i)
            {
                transform.GetChild(MAX_HEALTH - i - 1).GetComponent<SpriteRenderer>().color = emptyColor;
            }
        }
    }

    public void Restart()
    {
        for (int i = 0; i < 5; ++i)
        {
            currentHealth++;
            if (currentHealth > 5)
            {
                currentHealth = 5;
            }
            transform.GetChild(currentHealth - 1).GetComponent<SpriteRenderer>().color = fullColor;
        }

        for (int i = 0; i < MAX_HEALTH - currentHealth; ++i)
        {
            transform.GetChild(MAX_HEALTH - i - 1).GetComponent<SpriteRenderer>().color = emptyColor;
        }
        canTakeDamage = false;
        StartCoroutine(ResetInvulnerable());
    }

    public bool RestoreHealth()
    {
        currentHealth++;
        if(currentHealth > 5)
        {
            currentHealth = 5;
            return false;
        }
        transform.GetChild(currentHealth - 1).GetComponent<SpriteRenderer>().color = fullColor;
        return true;
    }

    public void TakeDamage()
    {
        if (FindObjectOfType<PlayerController>().CanMove)
        {
            if (currentHealth != 0)
            {
                if (canTakeDamage)
                {
                    if (!practice)
                    {
                        currentHealth--;
                        transform.GetChild(currentHealth).GetComponent<SpriteRenderer>().color = emptyColor;
                    }

                    canTakeDamage = false;
                    playerSpriteRenderer.color = Color.red;
                    FindObjectOfType<SoundPlayer>().PlaySound(hurtSound);
                    StartCoroutine(ResetInvulnerable());
                }
            }
            else
            {
                
                FindObjectOfType<PlayerAnimator>().Die();
            }
        }
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(C.PREFS_CURRENT_HEALTH, currentHealth);
        PlayerPrefs.Save();
    }

    private IEnumerator ResetInvulnerable()
    {
        yield return new WaitForSeconds(1.5f);
        canTakeDamage = true;
        playerSpriteRenderer.color = Color.white;
    }
}
