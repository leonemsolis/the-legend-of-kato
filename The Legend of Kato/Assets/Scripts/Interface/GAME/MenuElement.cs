using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuElement : MonoBehaviour
{
    // MAIN PAUSE MENU
    public const int SETTINGS = 0;
    public const int HELP = 1;
    public const int RESTART = 2;
    public const int QUIT = 3;
    // SETTINGS MENU
    public const int SOUND = 4;
    public const int MUSIC = 5;
    public const int BACK = 6;
    // RESTART MENU
    //public const int RESTART = 2;
    //public const int BACK = 6;
    // QUIT MENU
    //public const int QUIT = 3;
    //public const int BACK = 6;


    int menuElementType;

    public void SetElement(int type)
    {
        string text = "";
        menuElementType = type;
        switch (type)
        {
            case SETTINGS:
                text = "SETTINGS";
                break;
            case HELP:
                text = "HELP";
                break;
            case RESTART:
                text = "RESTART";
                break;
            case QUIT:
                text = "QUIT";
                break;
            case SOUND:
                text = "SOUND";
                break;
            case MUSIC:
                text = "MUSIC";
                break;
            case BACK:
                text = "BACK";
                break;
        }
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = text;
    }
}
