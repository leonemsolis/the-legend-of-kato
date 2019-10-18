using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaidLockUnlockBoxcolliderHider : MonoBehaviour
{
    [SerializeField] bool hideIfUnlocked;

    private void Start()
    {
        // GAME OBJECT
        if(!hideIfUnlocked)
        {
            if(PlayerPrefs.GetInt(C.PREFS_GAME_UNLOCKED, 0) == 0)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
            } else if (PlayerPrefs.GetInt(C.PREFS_GAME_UNLOCKED, 0) == 1)
            {
                GetComponent<BoxCollider2D>().enabled = true;
                GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        // LOCKER
        if(PlayerPrefs.GetInt(C.PREFS_GAME_UNLOCKED, 0) == 1 && hideIfUnlocked)
        {
            gameObject.SetActive(false);
        }
    }
}
