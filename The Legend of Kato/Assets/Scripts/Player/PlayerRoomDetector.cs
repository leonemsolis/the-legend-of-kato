using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoomDetector : MonoBehaviour
{
    Room currentRoom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Room>() != null)
        {
            currentRoom = collision.GetComponent<Room>();
        }
    }

    public Room GetCurrentRoom()
    {
        return currentRoom;
    }
}
