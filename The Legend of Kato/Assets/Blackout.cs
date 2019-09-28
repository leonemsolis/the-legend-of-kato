using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Blackout : MonoBehaviour
{
    [SerializeField] GameObject sceneLoader;
    LoadingBarMask loadingBarMask;

    const string blackin = "Blackin";

    Animator animator;

    float blackInTimer = .4f;

    void Start()
    {
        transform.localScale = new Vector3(10f, Camera.main.orthographicSize * 2f / 100f, 1f);
        animator = GetComponent<Animator>();
    }

    private IEnumerator Blackin(bool async, int index)
    {
        animator.Play(blackin);

        yield return new WaitForSecondsRealtime(blackInTimer);

        if (async)
        {
            Instantiate(sceneLoader, new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, 10f), Quaternion.identity);
            loadingBarMask = FindObjectOfType<LoadingBarMask>();
            StartCoroutine(LoadAsynchronously(index));
        }
        else
        {
            SceneManager.LoadScene(index);
        }
    }

    IEnumerator LoadAsynchronously(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        while (!operation.isDone)
        {
            loadingBarMask.SetPercentage(operation.progress);
            yield return null;
        }
    }

    public void LoadScene(int index)
    {
        StartCoroutine(Blackin(false, index));
    }

    public void LoadSceneAsynchronously(int index)
    {
        StartCoroutine(Blackin(true, index));
    }
}
