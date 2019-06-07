using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] int roomID;
    [SerializeField] GameObject exitBlock;

    public int RoomID
    {
        get
        {
            return roomID;
        }
    }

    const float roomHeight = 1500f;
    const float roomWidth = 1200f;

    public GameObject GetExitBlock()
    {
        return exitBlock;
    }
}
