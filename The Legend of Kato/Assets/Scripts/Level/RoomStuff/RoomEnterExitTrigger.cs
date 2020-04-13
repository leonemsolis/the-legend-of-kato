using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnterExitTrigger : MonoBehaviour
{
    [SerializeField] bool isEnterTrigger = true;
    [SerializeField] Room room;
    [SerializeField] int index = 0;
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
                if (index != 0 && PlayerPrefs.GetInt(C.PREFS_PRACTICE_MODE, 0) == 0)
                {
                    if(PlayerPrefs.GetInt(C.PREFS_ROOM_UNLOCKED + index, 0) == 0)
                    {
                        PlayerPrefs.SetInt(C.PREFS_ROOM_UNLOCKED + index, 1);
                        PlayerPrefs.Save();
                    }
                }
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
