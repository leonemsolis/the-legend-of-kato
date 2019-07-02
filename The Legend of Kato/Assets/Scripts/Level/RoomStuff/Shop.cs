using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] Text description;
    [SerializeField] Text price;
    [SerializeField] Transform selector;
    [SerializeField] Sprite soldOutSprite;

    List<SpriteRenderer> itemHolders;
    Health health;
    ScoreBoardText money;
    PlayerUpgrades playerUpgrades;

    const float selectorDeltaX = 150f;
    int selectorCurrentIndex = 0;

    string[] descriptions = new string[4];
    int[] prices = new int[4];
    int specialItemID;


    void Start()
    {
        health = FindObjectOfType<Health>();
        itemHolders = new List<SpriteRenderer>();
        for(int i = 1; i <= 4; ++i)
        {
            itemHolders.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
        }

        money = FindObjectOfType<ScoreBoardText>();
        playerUpgrades = FindObjectOfType<PlayerUpgrades>();
        PlaceItems();
    }

    private void PlaceItems()
    {
        if(health.GetCurrentShields() == 2)
        {
            descriptions[1] = C.SHOP_ITEM_SOLD_OUT_TEXT;
            prices[1] = 0;
            itemHolders[1].sprite = soldOutSprite;
        }
        else
        {
            descriptions[1] = "Gains 1 Extra HP";
            prices[1] = 1;
        }

        if(health.GetCurrentHealth() == 3)
        {
            descriptions[0] = C.SHOP_ITEM_SOLD_OUT_TEXT;
            prices[0] = 0;
            itemHolders[0].sprite = soldOutSprite;
        }
        else
        {
            descriptions[0] = "Restores 1 HP";
            prices[0] = 2;
        }


        descriptions[3] = "Leave Wizard Whale";
        prices[3] = 0;

        specialItemID = C.SHOP_ITEM_BOOTS;
        switch(specialItemID)
        {
            case C.SHOP_ITEM_BOOTS:
                descriptions[2] = "No Longer Takes Spike Damage";
                prices[2] = 5;
                break;
        }
        UpdateText();
    }

    public void SelectNextItem()
    {
        selectorCurrentIndex++;
        if(selectorCurrentIndex == 4)
        {
            selector.position += new Vector3(-selectorDeltaX * 3, 0f, 0f);
            selectorCurrentIndex = 0;
        }
        else
        {
            selector.position += new Vector3(selectorDeltaX, 0f, 0f);
        }
        UpdateText();
    }

    public void BuyItem()
    {
        switch(selectorCurrentIndex)
        {
            case 0:
                if (money.GetCurrentScore() >= prices[0])
                {
                    if(health.GetCurrentHealth() < 3)
                    {
                        money.DecreaseScore(prices[0]);
                        health.RestoreHealth();
                        if (health.GetCurrentHealth() == 3)
                        {
                            prices[0] = 0;
                            descriptions[0] = C.SHOP_ITEM_SOLD_OUT_TEXT;
                            itemHolders[0].sprite = soldOutSprite;

                            UpdateText();
                        }
                    }
                }
                break;
            case 1:
                if(money.GetCurrentScore() >= prices[1])
                {
                    if(health.GetCurrentShields() < 2)
                    {
                        money.DecreaseScore(prices[1]);
                        health.AddShield();
                        if(health.GetCurrentShields() == 2)
                        {
                            prices[1] = 0;
                            descriptions[1] = C.SHOP_ITEM_SOLD_OUT_TEXT;
                            itemHolders[1].sprite = soldOutSprite;

                            UpdateText();
                        }
                    }
                }
                break;
            case 2:
                if (money.GetCurrentScore() >= prices[2])
                {
                    money.DecreaseScore(prices[2]);
                    switch (specialItemID)
                    {
                        case C.SHOP_ITEM_BOOTS:
                            playerUpgrades.UpgradeTo(Upgrade.BOOTS);
                            break;
                    }
                    prices[2] = 0;
                    descriptions[2] = C.SHOP_ITEM_SOLD_OUT_TEXT;
                    itemHolders[2].sprite = soldOutSprite;

                    UpdateText();
                }
                break;
            case 3:
                FindObjectOfType<Whale>().LeaveWhale();
                break;
        }
    }

    private void UpdateText()
    {
        description.text = descriptions[selectorCurrentIndex];
        price.text = prices[selectorCurrentIndex].ToString();
    }
}
