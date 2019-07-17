using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] Room nextRoom;

    PlatformGenerator platformGen;
    RoomGenerator roomGen;
    PlayerRoomDetector detector;
    bool shopGenerated = false;
    int currentRoomIndex = 1;

    private void Start()
    {
        platformGen = GetComponent<PlatformGenerator>();
        roomGen = GetComponent<RoomGenerator>();
        detector = FindObjectOfType<PlayerRoomDetector>();
    }

    private void FixedUpdate()
    {
        // If it's not the 4th room(boss room)
        if(currentRoomIndex != 5)
        {
            // In the room
            if (detector.GetCurrentRoom() != null)
            {
                if (detector.GetCurrentRoom().RoomID == nextRoom.RoomID)
                {
                    Generate();
                    currentRoomIndex++;
                }
            }
        }
    }

    private void Generate()
    {
        //Vector2 lastPlatform = platformGen.GeneratePlatforms(nextRoom.GetExitBlock());
        Vector2 lastPlatform = new Vector2();
        switch (currentRoomIndex)
        {
            case 1:
                if (!shopGenerated)
                {
                    //25% that shop will be spawned as 2nd room
                    int x = Random.Range(0, 4);
                    if (x == 0)
                    {
                        nextRoom = roomGen.GenerateRoom(lastPlatform, RoomType.SHOP);
                        shopGenerated = true;
                    }
                    else
                    {
                        nextRoom = roomGen.GenerateRoom(lastPlatform, RoomType.COMMON);
                    }
                }
                break;
            case 2:
                if (!shopGenerated)
                {
                    //30% that shop will be spawned as 2nd room
                    int x = Random.Range(0, 3);
                    if (x == 0)
                    {
                        nextRoom = roomGen.GenerateRoom(lastPlatform, RoomType.SHOP);
                        shopGenerated = true;
                    }
                    else
                    {
                        nextRoom = roomGen.GenerateRoom(lastPlatform, RoomType.COMMON);
                    }
                }
                else
                {
                    nextRoom = roomGen.GenerateRoom(lastPlatform, RoomType.COMMON);
                }
                break;
            case 3:
                if (!shopGenerated)
                {
                    nextRoom = roomGen.GenerateRoom(lastPlatform, RoomType.SHOP);
                }
                else
                {
                    nextRoom = roomGen.GenerateRoom(lastPlatform, RoomType.COMMON);
                }
                break;
            case 4:
                nextRoom = roomGen.GenerateRoom(lastPlatform, RoomType.BOSS);
                break;
        }
    }
}
