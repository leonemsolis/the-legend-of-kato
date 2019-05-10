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
        transform.GetChild(currentHealth).GetComponent<SpriteRenderer>().color = Color.red;
        currentHealth++;
        if(currentHealth > 3)
        {
            currentHealth = 3;
        }
    }

    public void TakeDamage()
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    {
        if(canTakeDamage)
        {
            currentHealth--;
            transform.GetChild(currentHealth).GetComponent<SpriteRenderer>().color = Color.white;
            canTakeDamage = false;
            playerSpriteRenderer.color = Color.red;
            StartCoroutine(ResetInvulnerable());
            if (currentHealth == 0)
            {
                Destroy(FindObjectOfType<PlayerController>().gameObject);
                SceneManager.LoadScene(0);
            }
=======
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
>>>>>>> parent of 8a78d1b... Fix health, fix control on mobile devices
=======
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
>>>>>>> parent of 8a78d1b... Fix health, fix control on mobile devices
=======
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
>>>>>>> parent of 8a78d1b... Fix health, fix control on mobile devices
        }
    }

    private IEnumerator ResetInvulnerable()
    {
        yield return new WaitForSeconds(1f);
        canTakeDamage = true;
        playerSpriteRenderer.color = Color.white;
    }
}
