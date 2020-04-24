using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelector : MonoBehaviour
{
    void Start()
    {   
        bool hasShields = PlayerPrefs.GetInt(C.PREFS_SHIELDS_COUNT, 0) > 0;
        bool hasSlow = PlayerPrefs.GetInt(C.PREFS_SLOWMOS_COUNT, 0) > 0;
        bool hasBoot = PlayerPrefs.GetInt(C.PREFS_BOOTS_COUNT, 0) > 0;
        if(hasShields || hasSlow || hasBoot) {
            StartCoroutine(Pause());
        } else {
            Time.timeScale = C.DefaulTimeScale;
            FindObjectOfType<Announcer>().StartAnnounce();
            Destroy(gameObject);
        }
    }

    public void StartGame() {
        Time.timeScale = C.DefaulTimeScale;
        FindObjectOfType<Announcer>().StartAnnounce();
        Destroy(gameObject);

        foreach(ItemSelectionToggleUI i in FindObjectsOfType<ItemSelectionToggleUI>()) {
            if(i.selected) {
                switch(i.itemType) {
                    case ItemSelectionToggleUI.ItemType.BOOT:
                        print("BOOT ACTIVATED");
                        break;
                    case ItemSelectionToggleUI.ItemType.SLOW:
                        print("SLOW ACTIVATED");
                        break;
                    case ItemSelectionToggleUI.ItemType.SHIELD:
                        print("SHIELD ACTIVATED");
                        break;
                }
            }
        }
    }

    private IEnumerator Pause() {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    }


}
