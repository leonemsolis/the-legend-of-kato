using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FunctionStartButton : FunctionUI
{
    float awaitTime = 1.1f;

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

        if (PlayerPrefs.GetInt(C.PREFS_FIRST_LAUNCH, 1) == 1)
        {
            FindObjectOfType<Blackout>().LoadScene(C.IntroSceneIndex);
        }
        else
        {
            FindObjectOfType<Blackout>().LoadScene(C.LevelSelectionSceneIndex);
        }

    }
}
