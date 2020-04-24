using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemSelectionToggleUI : MonoBehaviour
{
    public enum ItemType{BOOT, SLOW, SHIELD};

    [SerializeField] TextMeshProUGUI descrption;

    [SerializeField] public ItemType itemType;

    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite offSprite;
    [SerializeField] Sprite notAvailable;
    

    SpriteRenderer spriteRenderer;
    [HideInInspector]
    public bool selected = false;

    private bool hasItem = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
        spriteRenderer.sprite = offSprite;

        switch(itemType) {
            case ItemType.BOOT:
                hasItem = PlayerPrefs.GetInt(C.PREFS_BOOTS_COUNT, 0) > 0;
                break;
            case ItemType.SLOW:
                hasItem = PlayerPrefs.GetInt(C.PREFS_SLOWMOS_COUNT, 0) > 0;
                break;
            case ItemType.SHIELD:
                hasItem = PlayerPrefs.GetInt(C.PREFS_SHIELDS_COUNT, 0) > 0;
                break;
        }
        if(!hasItem) {
            spriteRenderer.sprite = notAvailable;
        }
    }

    private void OnMouseDown()
    {
        if (CheckTouch(Input.mousePosition))
        {
            ButtonPressed();
        }
    }


    private bool CheckTouch(Vector3 pos)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D hit = Physics2D.OverlapPoint(touchPos);

        return hit && hit == gameObject.GetComponent<Collider2D>();
    }


    void ButtonPressed()
    {
        UpdateDescription();
        if(GetComponent<ButtonClickSound>() != null)
        {
            GetComponent<ButtonClickSound>().PlayClick();
        }
        if(hasItem) {
            selected = !selected;
            spriteRenderer.sprite = selected ? onSprite : offSprite;
        }
    }

    private void UpdateDescription() {
        if(hasItem) {
            switch(itemType) {
                case ItemType.BOOT:
                    descrption.SetText("GIVES TRIPLE JUMP");
                    break;
                case ItemType.SLOW:
                    descrption.SetText("GIVES SLOW MOTION JUMP");
                    break;
                case ItemType.SHIELD:
                    descrption.SetText("GIVES IMMUNITY TO PROJECTILES");
                    break;
            }
        } else {
            descrption.SetText("You don't have this item!");
        }
    }
}
