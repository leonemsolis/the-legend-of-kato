using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionBackToMainAsync : FunctionUI
{
    public override void Function()
    {
        if (FindObjectOfType<Blackout>() != null)
        {
            FindObjectOfType<Blackout>().LoadSceneAsynchronously(C.MainMenuSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(C.MainMenuSceneIndex);
        }
    }
}
