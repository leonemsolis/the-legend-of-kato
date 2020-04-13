using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonsPosition : MonoBehaviour
{

    const float bottomGap = 115f;

    private void Start()
    {
        transform.position = new Vector3(0f, -Camera.main.orthographicSize + bottomGap, 0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Application.platform == RuntimePlatform.Android)
        {
            Application.Quit();
        }
    }
}
