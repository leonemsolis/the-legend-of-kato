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

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }

    public void Open()
    {
        // TODO: REMOVE
        if(SceneManager.GetActiveScene().buildIndex == C.Level1SceneIndex)
        {
            //PlayerPrefs.SetInt(C.PREFS_STAGE_2_OPENED, 1);
            //PlayerPrefs.Save();
        } else if(SceneManager.GetActiveScene().buildIndex == C.Level2SceneIndex)
        {
            PlayerPrefs.SetInt(C.PREFS_STAGE_3_OPENED, 1);
            PlayerPrefs.Save();
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

            if(FindObjectOfType<Blackout>() != null)
            {
                //if (tutorialLevel)
                //{
                //    FindObjectOfType<Blackout>().LoadSceneAsynchronously(C.LevelSelectionSceneIndex);
                //}
                //else
                //{
                //    FindObjectOfType<Blackout>().LoadSceneAsynchronously(SceneManager.GetActiveScene().buildIndex + 1);
                //}
                FindObjectOfType<Blackout>().LoadSceneAsynchronously(C.LevelSelectionSceneIndex);
            }
            else
            {
                GameObject g = Instantiate(sceneLoader, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 1f), Quaternion.identity);
                loadingBarMask = FindObjectOfType<LoadingBarMask>();
                StartCoroutine(LoadAsynchronously());
            }
        }
    }

    public bool IsOpened()
    {
        return boxCollider.enabled;
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation;
        if (tutorialLevel)
        {
            operation = SceneManager.LoadSceneAsync(C.LevelSelectionSceneIndex);
        }
        else
        {
            operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
        while (!operation.isDone)
        {
            loadingBarMask.SetPercentage(operation.progress);
            yield return null;
        }
    }
}
