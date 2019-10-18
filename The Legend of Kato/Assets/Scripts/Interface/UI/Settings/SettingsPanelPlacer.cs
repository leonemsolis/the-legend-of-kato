﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanelPlacer : MonoBehaviour
{

    Transform title, music, sound, restore;

    const float titleHeight = 75f;
    const float topPartHeight = 375f;


    const float restoreToTogglesGap = 400f;
    const float restoreHeight = 262.5f;
    const float bottomGap = 300f;
    const float buttonHeight = 181.25f;

    void Start()
    {
        title = transform.GetChild(0);
        music = transform.GetChild(1);
        sound = transform.GetChild(2);
        restore = transform.GetChild(3);



        title.localPosition = new Vector3(0f, Camera.main.orthographicSize - topPartHeight - titleHeight / 2f, 0f);
        restore.localPosition = new Vector3(0f, -Camera.main.orthographicSize + bottomGap + restoreHeight / 2f, 0f);

        music.localPosition = new Vector3(music.localPosition.x, restore.localPosition.y + restoreToTogglesGap, 0f);
        sound.localPosition = new Vector3(sound.localPosition.x, restore.localPosition.y + restoreToTogglesGap, 0f);
    }
}
