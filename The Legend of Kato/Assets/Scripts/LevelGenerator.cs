using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    PlayerController player;
    GameObject bottomPlatform;

    void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Platform")) 
        {

        }
    }

    void Update()
    {

    }
}
