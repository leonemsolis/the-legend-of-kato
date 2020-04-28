using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemSelector : MonoBehaviour
{
    void Start()
    {   
        bool hasShields = PlayerPrefs.GetInt(C.PREFS_SHIELDS_COUNT, 0) > 0;
        bool hasSlow = PlayerPrefs.GetInt(C.PREFS_SLOWMOS_COUNT, 0) > 0;
        bool hasBoot = PlayerPrefs.GetInt(C.PREFS_BOOTS_COUNT, 0) > 0;
        if((hasShields || hasSlow || hasBoot) && SceneManager.GetActiveScene().buildIndex != C.Level0SceneIndex) {
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
        PlayerController player = FindObjectOfType<PlayerController>();
        foreach(ItemSelectionToggleUI i in FindObjectsOfType<ItemSelectionToggleUI>()) {
            if(i.selected) {
                switch(i.itemType) {
                    case ItemSelectionToggleUI.ItemType.BOOT:
                        player.bootsActivated = true;
                        PlayerPrefs.SetInt(C.PREFS_BOOTS_COUNT, PlayerPrefs.GetInt(C.PREFS_BOOTS_COUNT, 0) - 1);
                        break;
                    case ItemSelectionToggleUI.ItemType.SLOW:
                        player.slowActivated = true;
                        PlayerPrefs.SetInt(C.PREFS_SLOWMOS_COUNT, PlayerPrefs.GetInt(C.PREFS_SLOWMOS_COUNT, 0) - 1);
                        break;
                    case ItemSelectionToggleUI.ItemType.SHIELD:
                        player.shieldActivated = true;
                        PlayerPrefs.SetInt(C.PREFS_SHIELDS_COUNT, PlayerPrefs.GetInt(C.PREFS_SHIELDS_COUNT, 0) - 1);
                        break;
                }
            }
        }
        PlayerPrefs.Save();
    }

    private IEnumerator Pause() {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
    }


}
