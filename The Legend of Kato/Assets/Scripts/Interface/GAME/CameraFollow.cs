using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{   
    const float delayTime = .3f;
    const float unitsToBottom = 1000f;
    const float unitsToBottomInRoom = 800f;
    const float cameraShiftX = 300f;

    PlayerRoomDetector roomDetector;
    PlayerController player;
    Vector3 v = Vector3.zero;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
        roomDetector = FindObjectOfType<PlayerRoomDetector>();
    }

    private void Update()
    {
        float posX = 0;
        float posY = 0;

        if (roomDetector.GetCurrentRoom() != null)
        {
            posX = roomDetector.GetCurrentRoom().transform.position.x;

            float roomBottomThresholdY = roomDetector.GetCurrentRoom().transform.position.y + Camera.main.orthographicSize - roomDetector.GetCurrentRoom().RoomHeight / 2f - C.ButtonPanelHeight;
            float roomTopThresholdY = roomDetector.GetCurrentRoom().transform.position.y - Camera.main.orthographicSize + roomDetector.GetCurrentRoom().RoomHeight / 2f + C.InfoPanelHeight;

            if(roomDetector.GetCurrentRoom().RoomHeight <= (Camera.main.orthographicSize * 2f - (C.ButtonPanelHeight + C.InfoPanelHeight)))
            {
                posY = roomBottomThresholdY;
            }
            else
            {
                if (player.transform.position.y > roomBottomThresholdY && player.transform.position.y < roomTopThresholdY)
                {
                    posY = player.transform.position.y + Camera.main.orthographicSize - unitsToBottomInRoom;
                }
                else if (player.transform.position.y < roomBottomThresholdY)
                {
                    posY = roomBottomThresholdY;
                }
                else
                {
                    posY = roomTopThresholdY;
                }
            }
        } 
        else
        {
            posX = player.FacingRight ? player.transform.position.x + cameraShiftX : player.transform.position.x - cameraShiftX;
            posY = player.transform.position.y + Camera.main.orthographicSize - unitsToBottom;
        }


        Vector3 destination = new Vector3(posX, posY, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref v, delayTime);
    }
}