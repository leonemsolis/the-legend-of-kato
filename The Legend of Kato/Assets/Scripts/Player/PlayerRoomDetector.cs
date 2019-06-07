using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoomDetector : MonoBehaviour
{
    Room currentRoom;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<Room>() != null)
        {
            currentRoom = collision.GetComponent<Room>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Room>() != null)
        {
            currentRoom = null;
        }
    }

    public Room GetCurrentRoom()
    {
        return currentRoom;
    }
}
