using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBar : MonoBehaviour
{
    PlayerController player;
    const float maxWidth = 1f;
    readonly float maxHoverDuration = PlayerController.maxHoverDuration;
    float current = 0;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        current = player.GetRemainHoverTime();
    }

    void Update()
    {
        float currentWidth = maxWidth * player.GetRemainHoverTime() / maxHoverDuration;
        float positionX = (currentWidth * 100f / 2f) - 50f;
        transform.localScale = new Vector3(currentWidth, .1f, 1f);
        transform.localPosition = new Vector3(positionX, transform.localPosition.y, transform.localPosition.z);
    }
}
