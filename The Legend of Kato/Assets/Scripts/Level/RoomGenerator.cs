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
        Room selected;
        switch (type)
        {
            case RoomType.SHOP:
                selected = shop;
                break;
            case RoomType.BOSS:
                selected = boss;
                break;
            default:
                int index = Random.Range(0, rooms.Count);
                selected = rooms[index];
                rooms.RemoveAt(index);
                break;
        }

        Vector2 position = lastPlatform + new Vector2(selected.RoomWidth/2f + 50f, -(selected.RoomHeight/2f - 250f));
        return Instantiate(selected, position, Quaternion.identity);
    }
}
