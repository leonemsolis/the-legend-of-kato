using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum SceneEnum { MAIN, LEVELS, STAGE1, STAGE2, STAGE3, STAGE0, SETTINGS };
public class FunctionLoadScene : FunctionUI
{

    [SerializeField] SceneEnum sceneEnum;

    public override void Function()
    {
        int sceneIndex = 0;
        switch(sceneEnum)
        {
            case SceneEnum.MAIN:
                sceneIndex = C.MainMenuSceneIndex;
                break;
            case SceneEnum.LEVELS:
                sceneIndex = C.LevelSelectionSceneIndex;
                break;
            case SceneEnum.STAGE1:
                sceneIndex = C.Level1SceneIndex;
                break;
            case SceneEnum.STAGE2:
                sceneIndex = C.Level2SceneIndex;
                break;
            case SceneEnum.STAGE3:
                sceneIndex = C.Level3SceneIndex;
                break;
            case SceneEnum.STAGE0:
                sceneIndex = C.Level0SceneIndex;
                break;
            case SceneEnum.SETTINGS:
                sceneIndex = C.SettingsSceneIndex;
                break;
        }
        SceneManager.LoadScene(sceneIndex);
    }
}
