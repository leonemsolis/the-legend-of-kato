﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionBackToMain : FunctionUI
{
    public override void Function()
    {
        if(FindObjectOfType<Blackout>() != null)
        {
            FindObjectOfType<Blackout>().LoadScene(C.MainMenuSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(C.MainMenuSceneIndex);
        }
    }
}