using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{

    [SerializeField] AudioClip hurtSound;
    public const int MAX_HEALTH = 5;
    int currentHealth;

    bool canTakeDamage = true;
    SpriteRenderer playerSpriteRenderer;
    Color emptyColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    Color fullColor = new Color(1f, 1f, 1f, 1f);
    AudioSource audio; 


    private void Awake()
    {
        currentHealth = PlayerPrefs.GetInt(C.PREFS_CURRENT_HEALTH, 5);

        playerSpriteRenderer = FindObjectOfType<PlayerController>().GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();


        for(int i = 0; i < MAX_HEALTH - currentHealth; ++i)
        {
            transform.GetChild(MAX_HEALTH - i - 1).GetComponent<SpriteRenderer>().color = emptyColor;
        }
    }

    public void RestoreHealth()
    {
        currentHealth++;
        if(currentHealth > 5)
        {
            currentHealth = 5;
        }
        transform.GetChild(currentHealth - 1).GetComponent<SpriteRenderer>().color = fullColor;
    }

    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            //if (currentHealth > 1)
            //{
                currentHealth--;
                transform.GetChild(currentHealth).GetComponent<SpriteRenderer>().color = emptyColor;
            //}

            canTakeDamage = false;
            playerSpriteRenderer.color = Color.red;
            audio.clip = hurtSound;
            audio.Play();
            StartCoroutine(ResetInvulnerable());
            if (currentHealth == 0)
            {
                SceneManager.LoadScene(C.DeathSceneIndex);
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
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
        playerSpriteRenderer.color = Color.white;
    }
}
