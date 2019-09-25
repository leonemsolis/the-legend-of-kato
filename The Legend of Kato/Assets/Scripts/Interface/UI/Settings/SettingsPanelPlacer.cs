using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanelPlacer : MonoBehaviour
{

    Transform title, music, sound, reset;

    const float titleHeight = 75f;
    const float topPartHeight = 375f;

    const float resetHeight = 181.25f;
    const float bottomGap = 300f;
    const float buttonHeight = 181.25f;

    void Start()
    {
        title = transform.GetChild(0);
        music = transform.GetChild(1);
        sound = transform.GetChild(2);
        reset = transform.GetChild(3);
    }

    void Update()
    {
        title.localPosition = new Vector3(0f, Camera.main.orthographicSize - topPartHeight - titleHeight / 2f, 0f);
        reset.localPosition = new Vector3(0f, -Camera.main.orthographicSize + bottomGap + resetHeight / 2f, 0f);

        sound.localPosition = new Vector3(0f, reset.localPosition.y + buttonHeight * 1.2f, 0f);
        music.localPosition = new Vector3(0f, sound.localPosition.y + buttonHeight * 1.2f, 0f);
    }
}
