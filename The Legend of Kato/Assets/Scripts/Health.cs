using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    int currentHealth = 3;

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
        if(currentHealth == 0)
        {
            Destroy(FindObjectOfType<PlayerController>().gameObject);
        }
    }
}
