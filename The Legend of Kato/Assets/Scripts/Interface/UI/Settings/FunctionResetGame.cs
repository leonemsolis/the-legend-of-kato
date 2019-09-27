using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionResetGame : FunctionUI
{
    public override void Function()
    {
        PlayerPrefs.SetInt(C.PREFS_MONEY, 0);
        PlayerPrefs.SetInt(C.PREFS_KEY_1_STATE, 0);
        PlayerPrefs.SetInt(C.PREFS_KEY_2_STATE, 0);
        PlayerPrefs.SetInt(C.PREFS_KEY_3_STATE, 0);
        PlayerPrefs.SetInt(C.PREFS_FIRST_LAUNCH, 1);
        PlayerPrefs.SetInt(C.PREFS_MUSIC, 1);
        PlayerPrefs.SetInt(C.PREFS_SOUNDS, 1);
        PlayerPrefs.SetInt(C.PREFS_CURRENT_HEALTH, 5);
        PlayerPrefs.SetInt(C.PREFS_STAGE_2_OPENED, 0);
        PlayerPrefs.SetInt(C.PREFS_STAGE_3_OPENED, 0);
        PlayerPrefs.Save();

        SceneManager.LoadScene(C.SplashSceneIndex);
    }
}
