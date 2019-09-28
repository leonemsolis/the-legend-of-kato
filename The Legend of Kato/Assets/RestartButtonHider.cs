using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonHider : MonoBehaviour
{
    Color colorDisabled;

    private void Start()
    {
        colorDisabled = new Color(.2f, .2f, .2f, 1f);

        if (SceneManager.GetActiveScene().buildIndex == C.Level2SceneIndex || SceneManager.GetActiveScene().buildIndex == C.Level3SceneIndex)
        {
            GetComponent<SpriteRenderer>().color = colorDisabled;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
