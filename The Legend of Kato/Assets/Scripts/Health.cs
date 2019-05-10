using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    int currentHealth = 3;
    bool invulnerable = false;
    SpriteRenderer playerSpriteRenderer;

    private void Awake()
    {
        playerSpriteRenderer = FindObjectOfType<PlayerController>().GetComponent<SpriteRenderer>();
    }

    public void RestoreHealth()
    {
        transform.GetChild(currentHealth).GetComponent<SpriteRenderer>().color = Color.red;
        currentHealth++;
        if(currentHealth > 3)
        {
            currentHealth = 3;
        }
    }

    public void TakeDamage()
    {
        currentHealth--;
        transform.GetChild(currentHealth).GetComponent<SpriteRenderer>().color = Color.white;
        invulnerable = true;
        playerSpriteRenderer.color = Color.red;
        StartCoroutine(ResetInvulnerable());
        if (currentHealth == 0)
        {
            Destroy(FindObjectOfType<PlayerController>().gameObject);
            SceneManager.LoadScene(0);
        }
    }

    private IEnumerator ResetInvulnerable()
    {
        yield return new WaitForSeconds(1f);
        invulnerable = true;
        playerSpriteRenderer.color = Color.white;
    }
}
