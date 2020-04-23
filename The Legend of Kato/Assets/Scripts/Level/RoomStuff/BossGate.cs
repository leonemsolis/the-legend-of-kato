using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.LWRP;

public class BossGate : MonoBehaviour
{
    [SerializeField] Sprite openGate;
    [SerializeField] GameObject sceneLoader;
    [SerializeField] bool tutorialLevel;
    BoxCollider2D boxCollider;
    LoadingBarMask loadingBarMask;

    bool practice = false;
    bool full_game_unlocked = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        practice = PlayerPrefs.GetInt(C.PREFS_PRACTICE_MODE, 0) == 1;
    }

    public void Open()
    {
        if(!practice)
        {
            if (SceneManager.GetActiveScene().buildIndex == C.Level1SceneIndex)
            {
                PlayerPrefs.SetInt(C.PREFS_STAGE_2_OPENED, 1);
                PlayerPrefs.Save();
            }
            else if (SceneManager.GetActiveScene().buildIndex == C.Level2SceneIndex)
            {
                PlayerPrefs.SetInt(C.PREFS_STAGE_3_OPENED, 1);
                PlayerPrefs.Save();
            }
        }

        GetComponent<Light2D>().intensity = 1.6f;
        GetComponent<SpriteRenderer>().sprite = openGate;
        boxCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == C.PlayerTag)
        {
            // Stop Player movement
            FindObjectOfType<PlayerController>().CanMove = false;
            // Stop Camera movement
            if(Camera.main.gameObject.GetComponent<CameraFollow>() != null)
            {
                Camera.main.gameObject.GetComponent<CameraFollow>().enabled = false;
            }

            FindObjectOfType<RecordTracker>().CompleteLevel();

            if (practice || tutorialLevel)
            {
                FindObjectOfType<Blackout>().LoadSceneAsynchronously(C.LevelSelectionSceneIndex);
            }
            else
            {
                FindObjectOfType<Blackout>().LoadSceneAsynchronously(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public bool IsOpened()
    {
        return boxCollider.enabled;
    }
}
