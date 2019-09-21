using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoomDetector : MonoBehaviour
{
    Room currentRoom;

    public Room GetCurrentRoom()
    {
        return currentRoom;
    }

    public bool InTheRoom()
    {
        return currentRoom != null;
    }


    public void EnteredRoom(Room r)
    {
        currentRoom = r;
    }

    public void ExitedRoom()
    {
        currentRoom = null;
    }

}
