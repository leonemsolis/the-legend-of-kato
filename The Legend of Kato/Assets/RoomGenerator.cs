using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoomType {COMMON, SHOP, BOSS};

public class RoomGenerator : MonoBehaviour
{
    [SerializeField] public List<Room> rooms;
    [SerializeField] public Room boss;
    [SerializeField] public Room shop;

    public Room GenerateRoom(Vector2 lastPlatform, RoomType type)
    {
        Room r;
        switch (type)
        {
            case RoomType.SHOP:
                r = Instantiate(shop, lastPlatform + new Vector2(650f, -500f), Quaternion.identity);
                break;
            case RoomType.BOSS:
                r = Instantiate(boss, lastPlatform + new Vector2(650f, -500f), Quaternion.identity);
                break;
            default:
                int index = Random.Range(0, rooms.Count);
                r = Instantiate(rooms[index], lastPlatform + new Vector2(650f, -500f), Quaternion.identity);
                rooms.RemoveAt(index);
                break;
        }
        return r;
    }
}
