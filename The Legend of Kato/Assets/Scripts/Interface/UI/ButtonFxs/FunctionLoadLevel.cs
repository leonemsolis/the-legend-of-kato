using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FunctionLoadLevel : FunctionUI
{
    [SerializeField] SceneEnum sceneEnum;
    [SerializeField] GameObject sceneLoader;

    public int keyTier = 1;
    public int keyIndex = 0;

    LoadingBarMask loadingBarMask;

    public override void Function()
    {
        int loadingSceneID = 0;
        switch(sceneEnum)
        {
            case SceneEnum.STAGE0:
                PlayerPrefs.SetInt(C.PREFS_CURRENT_HEALTH, 5);
                loadingSceneID = C.Level0SceneIndex;
                break;
            case SceneEnum.STAGE1:
                loadingSceneID = C.Level1SceneIndex;
                PlayerPrefs.SetInt(C.PREFS_CURRENT_HEALTH, 5);
                break;
            case SceneEnum.STAGE2:
                loadingSceneID = C.Level2SceneIndex;
                PlayerPrefs.SetInt(C.PREFS_CURRENT_HEALTH, keyTier + 2);
                switch (keyIndex)
                {
                    case 0:
                        PlayerPrefs.SetInt(C.PREFS_KEY_1_STATE, 0);
                        break;
                    case 1:
                        PlayerPrefs.SetInt(C.PREFS_KEY_2_STATE, 0);
                        break;
                    case 2:
                        PlayerPrefs.SetInt(C.PREFS_KEY_3_STATE, 0);
                        break;
                }
                break;
            case SceneEnum.STAGE3:
                loadingSceneID = C.Level3SceneIndex;
                PlayerPrefs.SetInt(C.PREFS_CURRENT_HEALTH, keyTier + 2);
                switch(keyIndex)
                {
                    case 0:
                        PlayerPrefs.SetInt(C.PREFS_KEY_1_STATE, 0);
                        break;
                    case 1:
                        PlayerPrefs.SetInt(C.PREFS_KEY_2_STATE, 0);
                        break;
                    case 2:
                        PlayerPrefs.SetInt(C.PREFS_KEY_3_STATE, 0);
                        break;
                }
                break;
        }
        PlayerPrefs.SetInt(C.PREFS_MONEY, 0);
        PlayerPrefs.Save();

        if (FindObjectOfType<Blackout>() != null)
        {
            FindObjectOfType<Blackout>().LoadSceneAsynchronously(loadingSceneID);
        }
        else
        {
            Instantiate(sceneLoader, new Vector3(0f, 0f, 0f), Quaternion.identity);
            loadingBarMask = FindObjectOfType<LoadingBarMask>();
            StartCoroutine(LoadAsynchronously(loadingSceneID));
        }
    }

    IEnumerator LoadAsynchronously(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        while (!operation.isDone)
        {
            loadingBarMask.SetPercentage(operation.progress);
            yield return null;
        }
    }
}
