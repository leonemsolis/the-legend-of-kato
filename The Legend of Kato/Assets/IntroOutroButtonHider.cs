using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroOutroButtonHider : MonoBehaviour
{
    [SerializeField] bool isIntro;
    void Start()
    {
        if(isIntro)
        {
            if(PlayerPrefs.GetInt(C.PREFS_FIRST_LAUNCH, 1) == 1)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .2f);
            }
        }
        else
        {
            if(PlayerPrefs.GetInt(C.PREFS_GAME_COMPLETED, 0) == 0)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .2f);
            }
        }
    }
}
