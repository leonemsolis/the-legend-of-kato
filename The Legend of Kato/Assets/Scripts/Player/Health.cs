using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    [SerializeField] AudioClip hurtSound;
    int currentHealth = 3;

    int currentShields = 0;
    bool canTakeDamage = true;
    SpriteRenderer playerSpriteRenderer;
    Color emptyColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    Color fullColor = new Color(1f, 1f, 1f, 1f);
    AudioSource audio;


    private void Awake()
    {
        playerSpriteRenderer = FindObjectOfType<PlayerController>().GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();

        transform.GetChild(3).GetComponent<SpriteRenderer>().color = emptyColor;
        transform.GetChild(4).GetComponent<SpriteRenderer>().color = emptyColor;

        currentHealth--;
        transform.GetChild(currentHealth).GetComponent<SpriteRenderer>().color = emptyColor;
        currentHealth--;
        transform.GetChild(currentHealth).GetComponent<SpriteRenderer>().color = emptyColor;
    }

    public void RestoreHealth()
    {
        currentHealth++;
        if(currentHealth > 3)
        {
            currentHealth = 3;
        }
        transform.GetChild(currentHealth - 1).GetComponent<SpriteRenderer>().color = fullColor;
    }

    public void AddShield()
    {
        if(currentShields != 2)
        {
            currentShields++;
            transform.GetChild(2 + currentShields).GetComponent<SpriteRenderer>().color = fullColor;
        }
    }

    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            if(currentShields > 0)
            {
                transform.GetChild(2 + currentShields).GetComponent<SpriteRenderer>().color = emptyColor;
                currentShields--;
            }
            else
            {
                currentHealth--;
                transform.GetChild(currentHealth).GetComponent<SpriteRenderer>().color = emptyColor;
            }
            canTakeDamage = false;
            playerSpriteRenderer.color = Color.red;
            audio.clip = hurtSound;
            audio.Play();
            StartCoroutine(ResetInvulnerable());
            if (currentHealth == 0)
            {
                SceneManager.LoadScene(0);
            }
        } 
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetCurrentShields()
    {
        return currentShields;
    }

    private IEnumerator ResetInvulnerable()
    {
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
        playerSpriteRenderer.color = Color.white;
    }
}
