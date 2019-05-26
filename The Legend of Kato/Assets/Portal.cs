using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    PlayerController player;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();    
    }

    public void Teleport(bool toA, bool right)
    {
        Vector2 destination;
        float shiftX = right ? 100f : -100f;
        if(toA)
        {
            destination = transform.GetChild(0).transform.position;
        }
        else
        {
            destination = transform.GetChild(1).transform.position;
        }
        player.transform.position = new Vector3(destination.x + shiftX, destination.y + 30f, 0f);
    }
}
