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
        switch(sceneEnum)
        {
            case SceneEnum.STAGE0:
            case SceneEnum.STAGE1:
                PlayerPrefs.SetInt(C.PREFS_CURRENT_HEALTH, 5);
                break;
            case SceneEnum.STAGE2:
            case SceneEnum.STAGE3:
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

        Instantiate(sceneLoader, new Vector3(0f, 0f, 0f), Quaternion.identity);
        loadingBarMask = FindObjectOfType<LoadingBarMask>();
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        int sceneID = C.Level1SceneIndex;
        switch(sceneEnum)
        {
            case SceneEnum.STAGE0:
                sceneID = C.Level0SceneIndex;
                break;
            case SceneEnum.STAGE1:
                sceneID = C.Level1SceneIndex;
                break;
            case SceneEnum.STAGE2:
                sceneID = C.Level2SceneIndex;
                break;
            case SceneEnum.STAGE3:
                sceneID = C.Level3SceneIndex;
                break;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);
        while(!operation.isDone)
        {
            loadingBarMask.SetPercentage(operation.progress);

            yield return null;
        }
    }
}
