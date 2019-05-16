using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverBar : MonoBehaviour
{
    PlayerController player;
    const float maxWidth = 1f;
    float max = PlayerController.maxHoverDuration;
    float current = 0;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        current = player.GetRemainHoverTime();
    }

    void Update()
    {
        float currentWidth = maxWidth * player.GetRemainHoverTime() / max;
        float positionX = currentWidth / 2f - maxWidth / 2f;
        transform.localScale = new Vector3(currentWidth, .1f, 1f);
        transform.localPosition = new Vector3(positionX, transform.localPosition.y, transform.localPosition.z);
    }
}
