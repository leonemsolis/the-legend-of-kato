using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionSettingsUI : FunctionUI
{
    public override void Function()
    {
        SceneManager.LoadScene(C.SettingsSceneIndex);
    }
}
