﻿using System.Collections;
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
    float safeAreaBotShiftValue = 0f;
    float safeAreaTopShiftValue = 0f;

    private void Awake()
    {
        #if UNITY_EDITOR_OSX
            safeAreaBotShiftValue = 100f;
            safeAreaTopShiftValue = 100f;
        #else
            safeAreaTopShiftValue = Screen.height - Screen.safeArea.yMax;
            safeAreaBotShiftValue = Screen.safeArea.yMin;
        #endif

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
            
            // Threshold is yMin and yMax of the room
            float roomBottomThresholdY = roomDetector.GetCurrentRoom().transform.position.y + Camera.main.orthographicSize - roomDetector.GetCurrentRoom().RoomHeight / 2f - C.ButtonPanelHeight;
            float roomTopThresholdY = roomDetector.GetCurrentRoom().transform.position.y - Camera.main.orthographicSize + roomDetector.GetCurrentRoom().RoomHeight / 2f + C.InfoPanelHeight;

            // Room smaller that camera 
            if(roomDetector.GetCurrentRoom().RoomHeight <= (Camera.main.orthographicSize * 2f - (C.ButtonPanelHeight + C.InfoPanelHeight)))
            {
                posY = roomBottomThresholdY - safeAreaTopShiftValue;
            }
            // Room larger that camera
            else
            {
                // Player in center of the room
                if (player.transform.position.y > roomBottomThresholdY && player.transform.position.y < roomTopThresholdY)
                {
                    posY = player.transform.position.y + Camera.main.orthographicSize - unitsToBottomInRoom - safeAreaTopShiftValue;
                }
                // Player in bottom part
                else if (player.transform.position.y < roomBottomThresholdY)
                {
                    posY = roomBottomThresholdY - safeAreaTopShiftValue;
                }
                // Player in top part
                else
                {
                    posY = roomTopThresholdY + safeAreaTopShiftValue;
                }
            }
        } 
        else
        {
            posX = player.FacingRight ? player.transform.position.x + cameraShiftX : player.transform.position.x - cameraShiftX;
            posY = player.transform.position.y + Camera.main.orthographicSize - unitsToBottom - safeAreaTopShiftValue;
        }

        Vector3 destination = new Vector3(posX, posY, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref v, delayTime);
    }
}