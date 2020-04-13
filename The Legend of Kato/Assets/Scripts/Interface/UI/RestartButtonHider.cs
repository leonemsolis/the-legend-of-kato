using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonHider : MonoBehaviour
{
    private void Start()
    {
        if(PlayerPrefs.GetInt(C.PREFS_PRACTICE_MODE, 0) == 0)
        {
            if (SceneManager.GetActiveScene().buildIndex == C.Level2SceneIndex || SceneManager.GetActiveScene().buildIndex == C.Level3SceneIndex)
            {
                GetComponent<SpriteRenderer>().color = new Color(.2f, .2f, .2f, 1f);
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
