using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnterExitTrigger : MonoBehaviour
{
    [SerializeField] bool isEnterTrigger = true;
    [SerializeField] Room room;
    PlayerRoomDetector detector;

    void Start()
    {
        detector = FindObjectOfType<PlayerRoomDetector>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == C.PlayerTag)
        {
            if(isEnterTrigger)
            {
                if (!detector.InTheRoom())
                {
                    detector.EnteredRoom(room);
                    room.CloseDoor();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == C.PlayerTag)
        {
            if(!isEnterTrigger)
            {
                if (detector.gameObject.transform.position.x > transform.position.x)
                {
                    if (detector.InTheRoom())
                    {
                        detector.ExitedRoom();
                    }
                }
                else
                {
                    if (!detector.InTheRoom())
                    {
                        detector.EnteredRoom(room);
                    }
                }
            }
        }
    }
}
