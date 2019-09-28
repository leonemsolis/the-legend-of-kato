﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionRestartGame : FunctionUI
{
    [SerializeField] GameObject sceneLoader;

    LoadingBarMask loadingBarMask;

    public override void Function()
    {
        PlayerPrefs.SetInt(C.PREFS_CURRENT_HEALTH, 5);
        PlayerPrefs.SetInt(C.PREFS_MONEY, 0);
        PlayerPrefs.Save();

        Instantiate(sceneLoader, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f), Quaternion.identity);
        loadingBarMask = FindObjectOfType<LoadingBarMask>();

        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        while (!operation.isDone)
        {
            loadingBarMask.SetPercentage(operation.progress);
            yield return null;
        }
    }
}
