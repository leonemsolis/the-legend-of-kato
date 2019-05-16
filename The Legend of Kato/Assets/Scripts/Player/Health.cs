using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    int currentHealth = 3;
    bool canTakeDamage = true;
    SpriteRenderer playerSpriteRenderer;

    private void Awake()
    {
        playerSpriteRenderer = FindObjectOfType<PlayerController>().GetComponent<SpriteRenderer>();
    }

    public void RestoreHealth()
    {
        transform.GetChild(currentHealth).GetComponent<SpriteRenderer>().color = new Color(0.6320754f, 0.0506853f, 0.0506853f, 0.8941177f);
        currentHealth++;
        if(currentHealth > 3)
        {
            currentHealth = 3;
        }
    }

    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            currentHealth--;
            transform.GetChild(currentHealth).GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.8941177f);
            canTakeDamage = false;
            playerSpriteRenderer.color = Color.red;
            StartCoroutine(ResetInvulnerable());
            if (currentHealth == 0)
            {
                //Destroy(FindObjectOfType<PlayerController>().gameObject);
                SceneManager.LoadScene(0);
            }
        } 
    }
    
    private IEnumerator ResetInvulnerable()
    {
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
        playerSpriteRenderer.color = Color.white;
    }
}
