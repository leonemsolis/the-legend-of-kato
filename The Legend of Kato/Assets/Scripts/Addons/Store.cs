using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Store : MonoBehaviour
{
    [SerializeField] List<GameObject> items;
    [SerializeField] Transform selector;

    [SerializeField] TextMeshProUGUI description;
    [SerializeField] Text price;
    [SerializeField] TextMeshProUGUI moneyText;

    [SerializeField] Sprite soldOutSprite;
    [SerializeField] Sprite enchantItem;

    [SerializeField] AudioClip healthSound;
    [SerializeField] AudioClip keySound;

    private string[] descriptions = new string[5];
    private int[] prices = new int[5];

    private int selectorCurrentIndex = 0;
    private int money = 0;
    private string enchantDesctiption;
    Translator translator;

    private void Start()
    {
        translator = FindObjectOfType<Translator>();
        translator.SetFont(FontType.BLACK_WHITE, description);
        enchantDesctiption = translator.GetTranslation("store_1");
        money = PlayerPrefs.GetInt(C.PREFS_DEPOSIT_COUNT, 0);
        moneyText.text = money.ToString();
        UpdateInfo();   
    }

    private void SpendMoney(int value) {
        money -= value;
        moneyText.text = money.ToString();
        PlayerPrefs.SetInt(C.PREFS_DEPOSIT_COUNT, money);
        PlayerPrefs.Save();
    }

    private void SetCustomItemSprite(int itemIndex, Sprite sprite) {
        items[itemIndex].GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private void UpdateInfo() {
        if (MaxKeysBought())
        {
            descriptions[0] = translator.GetTranslation("store_3") + "\n" + GetKeyInformation();
            prices[0] = 0;
            SetCustomItemSprite(0, soldOutSprite);
        }
        else
        {
            descriptions[0] = translator.GetTranslation("store_2") + "\n" + GetKeyInformation();
            prices[0] = C.SHOP_KEY_COST;
        }

        if(NoKeysAvailableForEnchant())
        {
            descriptions[1] = translator.GetTranslation("store_3") +"\n"+ GetKeyInformation();
            prices[1] = 0;
            SetCustomItemSprite(1, soldOutSprite);
        }
        else
        {
            descriptions[1] = enchantDesctiption + "\n" + GetKeyInformation();
            prices[1] = C.SHOP_ENCHANT_COST;
            SetCustomItemSprite(1, enchantItem);
        }

        descriptions[2] = translator.GetTranslation("store_7").Replace("#", PlayerPrefs.GetInt(C.PREFS_BOOTS_COUNT, 0).ToString());
        prices[2] = C.SHOP_BOOT_COST;

        descriptions[3] = translator.GetTranslation("store_8").Replace("#", PlayerPrefs.GetInt(C.PREFS_SLOWMOS_COUNT, 0).ToString());
        prices[3] = C.SHOP_SLOW_COST;

        descriptions[4] = translator.GetTranslation("store_9").Replace("#", PlayerPrefs.GetInt(C.PREFS_SHIELDS_COUNT, 0).ToString());
        prices[4] = C.SHOP_SHIELD_COST;

        UpdateText();
    }

    private void UpdateText() {
        description.SetText(descriptions[selectorCurrentIndex]);
        price.text = prices[selectorCurrentIndex].ToString();
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
        string availableKeys = ((k1 == 0) ? "" : k1 + translator.GetTranslation("store_4") + " ") + ((k2 == 0) ? "" : k2+translator.GetTranslation("store_4") + " ") + ((k3 == 0) ? "" : k3 + translator.GetTranslation("store_4"));
        if(availableKeys.Length == 0)
        {
            availableKeys = translator.GetTranslation("store_5");
        }
        return translator.GetTranslation("store_6")+"\n"+ availableKeys;
    }

    public void SelectCertainItem(int index) {
        selectorCurrentIndex = index;
        selector.localPosition = items[index].transform.localPosition;
        UpdateText();
    }

    public void BuyItem() {
        if(money >= prices[selectorCurrentIndex]) {
            switch(selectorCurrentIndex) {
                // Key
                case 0:
                    if(!MaxKeysBought())
                    {
                        FindObjectOfType<SoundPlayer>().PlaySound(keySound);
                        SpendMoney(prices[selectorCurrentIndex]);
                        BuyKey();
                    }
                    break;
                // Enchant
                case 1:
                    if(!NoKeysAvailableForEnchant())
                    {
                        FindObjectOfType<SoundPlayer>().PlaySound(keySound);
                        SpendMoney(prices[selectorCurrentIndex]);
                        EnchantKey();
                    }
                    break;
                // Boot
                case 2:
                    FindObjectOfType<SoundPlayer>().PlaySound(healthSound);
                    PlayerPrefs.SetInt(C.PREFS_BOOTS_COUNT, PlayerPrefs.GetInt(C.PREFS_BOOTS_COUNT, 0) + 1);
                    PlayerPrefs.Save();
                    SpendMoney(prices[selectorCurrentIndex]);
                    break;
                // Slowmo
                case 3:
                    FindObjectOfType<SoundPlayer>().PlaySound(healthSound);
                    PlayerPrefs.SetInt(C.PREFS_SLOWMOS_COUNT, PlayerPrefs.GetInt(C.PREFS_SLOWMOS_COUNT, 0) + 1);
                    PlayerPrefs.Save();
                    SpendMoney(prices[selectorCurrentIndex]);
                    break;
                // Shield
                case 4:
                    FindObjectOfType<SoundPlayer>().PlaySound(healthSound);
                    PlayerPrefs.SetInt(C.PREFS_SHIELDS_COUNT, PlayerPrefs.GetInt(C.PREFS_SHIELDS_COUNT, 0) + 1);
                    PlayerPrefs.Save();
                    SpendMoney(prices[selectorCurrentIndex]);
                    break;
            }
            UpdateInfo();
        }
    }
}
