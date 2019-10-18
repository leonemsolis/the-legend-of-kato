using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class FunctionLoadLevel : FunctionUI
{
    [SerializeField] SceneEnum sceneEnum;

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

        FindObjectOfType<Blackout>().LoadSceneAsynchronously(loadingSceneID);
    }
}
