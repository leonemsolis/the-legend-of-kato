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

    [SerializeField] AudioClip healthSound;
    [SerializeField] AudioClip keySound;
    [SerializeField] AudioClip shopSound;
    [SerializeField] List<GameObject> items;

    Health health;
    ScoreBoardText money;

    const float selectorDeltaX = 150f * 1.4f;
    int selectorCurrentIndex = 0;

    string[] descriptions = new string[8];
    int[] prices = new int[8];
    const string enchantDesctiption = "ENCHANT (!)ONE KEY'S TIER\nMAX TIER IS 3";

    void Start()
    {
        FindObjectOfType<SoundPlayer>().PlaySound(shopSound);
        transform.localScale = new Vector3(1.4f, 1.4f, 1f);
        health = FindObjectOfType<Health>();
        money = FindObjectOfType<ScoreBoardText>();
        UpdateInfo();
    }

    private void UpdateInfo()
    {
        descriptions[0] = "GIVES TRIPLE JUMP. DURATION - 1 LEVEL. YOU HAVE "+PlayerPrefs.GetInt(C.PREFS_BOOTS_COUNT, 0);
        prices[0] = C.SHOP_BOOT_COST;

        descriptions[1] = "DEPOSIT 1 SOUL TO THE WIZARD WHALE. YOU CAN SPEND DEPOSITED SOULS LATER. YOU HAVE "+PlayerPrefs.GetInt(C.PREFS_DEPOSIT_COUNT, 0);
        prices[1] = 1;

        descriptions[2] = "GIVES SLOW MOTION JUMP. DURATION - 1 LEVEL. YOU HAVE "+PlayerPrefs.GetInt(C.PREFS_SLOWMOS_COUNT, 0);
        prices[2] = C.SHOP_SLOW_COST;

        descriptions[3] = "GIVES IMMUNITY TO PROJECTILES AND SPIKES. DURATION - 1 LEVEL. YOU HAVE "+PlayerPrefs.GetInt(C.PREFS_SHIELDS_COUNT, 0);
        prices[3] = C.SHOP_SHIELD_COST;

        descriptions[7] = "LEAVE WIZARD WHALE";
        prices[7] = 0;

        if (health.GetCurrentHealth() == Health.MAX_HEALTH)
        {
            descriptions[4] = C.SHOP_ITEM_SOLD_OUT_TEXT;
            prices[4] = 0;
            SetCustomItemSprite(4, soldOutSprite);
        }
        else
        {
            descriptions[4] = "RESTORES 1 HP";
            prices[4] = C.SHOP_HEART_COST;
        }

        if (MaxKeysBought())
        {
            descriptions[5] = C.SHOP_ITEM_SOLD_OUT_TEXT + "\n" + GetKeyInformation();
            prices[5] = 0;
            SetCustomItemSprite(5, soldOutSprite);
        }
        else
        {
            descriptions[5] = "BUY TIER-1 KEY" + "\n" + GetKeyInformation();
            prices[5] = C.SHOP_KEY_COST;
        }

        if(NoKeysAvailableForEnchant())
        {
            descriptions[6] = C.SHOP_ITEM_SOLD_OUT_TEXT +"\n"+ GetKeyInformation();
            prices[6] = 0;
            SetCustomItemSprite(6, soldOutSprite);
        }
        else
        {
            descriptions[6] = enchantDesctiption + "\n" + GetKeyInformation();
            prices[6] = C.SHOP_ENCHANT_COST;
            SetCustomItemSprite(6, enchantItem);
        }


        UpdateText();
    }

    private void SetCustomItemSprite(int itemIndex, Sprite sprite) {
        items[itemIndex].GetComponent<SpriteRenderer>().sprite = sprite;
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
        if(selectorCurrentIndex == 8)
        {
            selectorCurrentIndex = 0;
        }
        selector.localPosition = items[selectorCurrentIndex].transform.localPosition + new Vector3(0f, 111f, 0f);
        UpdateText();
    }

    public void SelectCertainItem(int index)
    {
        selectorCurrentIndex = index;
        selector.localPosition = items[selectorCurrentIndex].transform.localPosition + new Vector3(0f, 111f, 0f);
        UpdateText();
    }

    public void BuyItem()
    {
        if(selectorCurrentIndex == 7) {
            FindObjectOfType<SoundPlayer>().PlaySound(shopSound);
            FindObjectOfType<Whale>().LeaveWhale();
        } else {
            if(money.GetCurrentScore() >= prices[selectorCurrentIndex]) {
                switch(selectorCurrentIndex) {
                    // Boot
                    case 0:
                        PlayerPrefs.SetInt(C.PREFS_BOOTS_COUNT, PlayerPrefs.GetInt(C.PREFS_BOOTS_COUNT, 0) + 1);
                        PlayerPrefs.Save();
                        FindObjectOfType<SoundPlayer>().PlaySound(healthSound);
                        money.DecreaseScore(prices[selectorCurrentIndex]);
                        break;
                    // Deposit
                    case 1:
                        PlayerPrefs.SetInt(C.PREFS_DEPOSIT_COUNT, PlayerPrefs.GetInt(C.PREFS_DEPOSIT_COUNT, 0) + 1);
                        PlayerPrefs.Save();
                        FindObjectOfType<SoundPlayer>().PlaySound(keySound);
                        money.DecreaseScore(prices[selectorCurrentIndex]);
                        break;
                    // Slowmo
                    case 2:
                        PlayerPrefs.SetInt(C.PREFS_SLOWMOS_COUNT, PlayerPrefs.GetInt(C.PREFS_SLOWMOS_COUNT, 0) + 1);
                        PlayerPrefs.Save();
                        FindObjectOfType<SoundPlayer>().PlaySound(healthSound);
                        money.DecreaseScore(prices[selectorCurrentIndex]);
                        break;
                    // Shield
                    case 3:
                        PlayerPrefs.SetInt(C.PREFS_SHIELDS_COUNT, PlayerPrefs.GetInt(C.PREFS_SHIELDS_COUNT, 0) + 1);
                        PlayerPrefs.Save();
                        FindObjectOfType<SoundPlayer>().PlaySound(healthSound);
                        money.DecreaseScore(prices[selectorCurrentIndex]);
                        break;
                    // Heart
                    case 4:
                        if(health.GetCurrentHealth() < Health.MAX_HEALTH)
                        {
                            FindObjectOfType<SoundPlayer>().PlaySound(healthSound);
                            money.DecreaseScore(prices[selectorCurrentIndex]);
                            health.RestoreHealth();
                        }
                        break;
                    // Key
                    case 5:
                        if(!MaxKeysBought())
                        {
                            FindObjectOfType<SoundPlayer>().PlaySound(keySound);
                            money.DecreaseScore(prices[selectorCurrentIndex]);
                            BuyKey();
                        }
                        break;
                    // Enchant
                    case 6:
                        if(!NoKeysAvailableForEnchant())
                        {
                            FindObjectOfType<SoundPlayer>().PlaySound(keySound);
                            money.DecreaseScore(prices[selectorCurrentIndex]);
                            EnchantKey();
                        }
                        break;
                }
                UpdateInfo();
            }
        }
    }

    private void UpdateText()
    {
        description.text = descriptions[selectorCurrentIndex];
        price.text = prices[selectorCurrentIndex].ToString();
    }
}
