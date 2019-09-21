using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressDisplay : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Sprite[] indicatorSprites;
    PlayerRoomDetector prd;
    SpriteRenderer spriteRenderer;
    ProgressIndicator indicator;

    int currentRoomID = -1;
    int currentSpriteID = 0;

    bool justLeavedRoom = false;

    void Start()
    {
        prd = FindObjectOfType<PlayerRoomDetector>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        indicator = FindObjectOfType<ProgressIndicator>();

    }

    private void Update()
    {
        if(prd.InTheRoom())
        {
            // In the new room
            if(IsNewRoom())
            {
                justLeavedRoom = true;
                SetNextSprite();
                currentRoomID = prd.GetCurrentRoom().RoomID;
            }
        }
        else
        {
            if(justLeavedRoom)
            {
                SetNextSprite();
                justLeavedRoom = false;
            }
        }
    }

    private bool IsNewRoom()
    {
        switch(currentRoomID)
        {
            // Entrance
            case -1:
            // 1st group
            case 0:
            // 2nd group
            case 1:
            // 5th group
            case 999:
            // 6th group
            case 1000:
                if (prd.GetCurrentRoom().RoomID > currentRoomID)
                {
                    return true;
                }
                break;
            // 3rd group
            case 2:
            case 3:
                if (prd.GetCurrentRoom().RoomID > 3)
                {
                    return true;
                }
                break;
            // 4th group
            case 4:
            case 5:
            case 6:
                if (prd.GetCurrentRoom().RoomID > 6)
                {
                    return true;
                }
                break;
        }
        return false;
    }

    private void SetNextSprite()
    {
        spriteRenderer.sprite = sprites[currentSpriteID];
        indicator.ChangeSprite(indicatorSprites[currentSpriteID]);
        currentSpriteID++;
    }

}
