using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMove : MonoBehaviour
{
    PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnMouseDown()
    {
        player.ChangeDirection();
    }
}
