﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionStartButton : FunctionUI
{
    float awaitTime = 2f;

    public override void Function()
    {
        FindObjectOfType<TitleImageTop>().Activate();
        FindObjectOfType<TitleTextPlacer>().Stop();
        FindObjectOfType<TapTextPlacer>().Hide();
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(awaitTime);
        SceneManager.LoadScene(1);
    }
}
