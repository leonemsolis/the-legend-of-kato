using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class FunctionLoadLevel : FunctionUI
{
    [SerializeField] int SceneID;
    [SerializeField] GameObject sceneLoader;

    public int keyTier = 1;
    public int keyIndex = 0;

    LoadingBarMask loadingBarMask;

    public override void Function()
    {
        switch(SceneID)
        {
            case C.Level1SceneIndex:
                PlayerPrefs.SetInt(C.PREFS_CURRENT_HEALTH, 5);
                break;
            case C.Level2SceneIndex:
            case C.Level3SceneIndex:
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
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneID);
        while(!operation.isDone)
        {
            loadingBarMask.SetPercentage(operation.progress);

            yield return null;
        }
    }
}
