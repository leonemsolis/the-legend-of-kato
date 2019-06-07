using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    int currentHealth = 3;
    bool canTakeDamage = true;
    SpriteRenderer playerSpriteRenderer;
    Color emptyColor = new Color(0.2f, 0.2f, 0.2f, 1f);
    Color fullColor = new Color(1f, 1f, 1f, 1f);

    private void Awake()
    {
        playerSpriteRenderer = FindObjectOfType<PlayerController>().GetComponent<SpriteRenderer>();
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

    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            //currentHealth--;
            //transform.GetChild(currentHealth).GetComponent<SpriteRenderer>().color = emptyColor;
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
