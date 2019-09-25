using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionBackToMain : FunctionUI
{
    public override void Function()
    {
        SceneManager.LoadScene(C.MainMenuSceneIndex);
    }
}
