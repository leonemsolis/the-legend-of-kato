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
    [SerializeField] Sprite enchantItem;

    List<SpriteRenderer> itemHolders;
    Health health;
    ScoreBoardText money;

    const float selectorDeltaX = 150f * 1.4f;
    int selectorCurrentIndex = 0;

    string[] descriptions = new string[4];
    int[] prices = new int[4];
    const string enchantDesctiption = "ENCHANT (!)ONE KEY'S TIER\nMAX TIER IS 3";

    void Start()
    {
        transform.localScale = new Vector3(1.4f, 1.4f, 1f);

        health = FindObjectOfType<Health>();
        itemHolders = new List<SpriteRenderer>();
        for(int i = 1; i <= 4; ++i)
        {
            itemHolders.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
        }

        money = FindObjectOfType<ScoreBoardText>();
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        if (health.GetCurrentHealth() == Health.MAX_HEALTH)
        {
            descriptions[0] = C.SHOP_ITEM_SOLD_OUT_TEXT;
            prices[0] = 0;
            itemHolders[0].sprite = soldOutSprite;
        }
        else
        {
            descriptions[0] = "RESTORES 1 HP";
            prices[0] = 2;
        }

        if (MaxKeysBought())
        {
            descriptions[1] = C.SHOP_ITEM_SOLD_OUT_TEXT + "\n" + GetKeyInformation();
            prices[1] = 0;
            itemHolders[1].sprite = soldOutSprite;
        }
        else
        {
            descriptions[1] = "BUY TIER-1 KEY" + "\n" + GetKeyInformation();
            prices[1] = 5;
        }

        if(NoKeysAvailableForEnchant())
        {
            descriptions[2] = C.SHOP_ITEM_SOLD_OUT_TEXT +"\n"+ GetKeyInformation();
            prices[2] = 0;
            itemHolders[2].sprite = soldOutSprite;
        }
        else
        {
            descriptions[2] = enchantDesctiption + "\n" + GetKeyInformation();
            prices[2] = 5;
            itemHolders[2].sprite = enchantItem;
        }


        descriptions[3] = "LEAVE WIZARD WHALE";
        prices[3] = 0;


        UpdateText();
    }

    public bool NoKeysAvailableForEnchant()
    {
        if (PlayerPrefs.GetInt(C.PREFS_KEY_1_STATE, 0) == 1 || PlayerPrefs.GetInt(C.PREFS_KEY_1_STATE, 0) == 2)
        {
            return false;
        }
        if (PlayerPrefs.GetInt(C.PREFS_KEY_2_STATE, 0) == 1 || PlayerPrefs.GetInt(C.PREFS_KEY_2_STATE, 0) == 2)
        {
            return false;
        }
        if (PlayerPrefs.GetInt(C.PREFS_KEY_3_STATE, 0) == 1 || PlayerPrefs.GetInt(C.PREFS_KEY_3_STATE, 0) == 2)
        {
            return false;
        }
        return true;
    }

    public bool MaxKeysBought()
    {
        if(PlayerPrefs.GetInt(C.PREFS_KEY_1_STATE, 0) == 0)
        {
            return false;
        }
        if (PlayerPrefs.GetInt(C.PREFS_KEY_2_STATE, 0) == 0)
        {
            return false;
        }
        if (PlayerPrefs.GetInt(C.PREFS_KEY_3_STATE, 0) == 0)
        {
            return false;
        }

        return true;
    }

    public void EnchantKey()
    {
        int[] keyStates = new int[3];
        keyStates[0] = PlayerPrefs.GetInt(C.PREFS_KEY_1_STATE, 0);
        keyStates[1] = PlayerPrefs.GetInt(C.PREFS_KEY_2_STATE, 0);
        keyStates[2] = PlayerPrefs.GetInt(C.PREFS_KEY_3_STATE, 0);

        int maxEnchantedKeyID = -1;
        int maxEnchantedKeyValue = 0;

        for (int i = 0; i < 3; ++i)
        {
            if(keyStates[i] > maxEnchantedKeyValue && keyStates[i] != 3)
            {
                maxEnchantedKeyID = i;
                maxEnchantedKeyValue = keyStates[i];
            }
        }

        switch(maxEnchantedKeyID)
        {
            case 0:
                PlayerPrefs.SetInt(C.PREFS_KEY_1_STATE, keyStates[0] + 1);
                break;
            case 1:
                PlayerPrefs.SetInt(C.PREFS_KEY_2_STATE, keyStates[1] + 1);
                break;
            case 2:
                PlayerPrefs.SetInt(C.PREFS_KEY_3_STATE, keyStates[2] + 1);
                break;
        }
        PlayerPrefs.Save();
    }

    public void BuyKey()
    {
        if(PlayerPrefs.GetInt(C.PREFS_KEY_1_STATE, 0) == 0)
        {
            PlayerPrefs.SetInt(C.PREFS_KEY_1_STATE, 1);
        } 
        else if (PlayerPrefs.GetInt(C.PREFS_KEY_2_STATE, 0) == 0)
        {
            PlayerPrefs.SetInt(C.PREFS_KEY_2_STATE, 1);
        }
        else if (PlayerPrefs.GetInt(C.PREFS_KEY_3_STATE, 0) == 0)
        {
            PlayerPrefs.SetInt(C.PREFS_KEY_3_STATE, 1);
        }
        PlayerPrefs.Save();
    }

    public string GetKeyInformation()
    {
        int k1 = PlayerPrefs.GetInt(C.PREFS_KEY_1_STATE, 0);
        int k2 = PlayerPrefs.GetInt(C.PREFS_KEY_2_STATE, 0);
        int k3 = PlayerPrefs.GetInt(C.PREFS_KEY_3_STATE, 0);
        string availableKeys = ((k1 == 0) ? "" : "TIER-" + k1 + "  ") + ((k2 == 0) ? "" : "TIER-" + k2 + "  ") + ((k3 == 0) ? "" : "TIER-" + k3);
        if(availableKeys.Length == 0)
        {
            availableKeys = "NONE";
        }
        return "KEYS AVAILABLE:"+"\n"+ availableKeys;
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
            // HEALTH
            case 0:
                if (money.GetCurrentScore() >= prices[0])
                {
                    if(health.GetCurrentHealth() < Health.MAX_HEALTH)
                    {
                        money.DecreaseScore(prices[0]);
                        health.RestoreHealth();
                        UpdateInfo();
                    }
                }
                break;
            // KEY
            case 1:
                if(money.GetCurrentScore() >= prices[1])
                {
                    if(!MaxKeysBought())
                    {
                        money.DecreaseScore(prices[1]);
                        BuyKey();
                        UpdateInfo();
                    }
                }
                break;
            // ENCHANT
            case 2:
                if (money.GetCurrentScore() >= prices[2])
                {
                    if(!NoKeysAvailableForEnchant())
                    {
                        money.DecreaseScore(prices[2]);
                        EnchantKey();
                        UpdateInfo();
                    }
                }
                break;
            // LEAVE
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
