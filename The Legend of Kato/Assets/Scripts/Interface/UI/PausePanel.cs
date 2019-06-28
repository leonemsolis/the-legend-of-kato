#pragma warning disable 0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum MenuType {MAIN, SETTINGS, RESTART, QUIT, HELP};

public class PausePanel : MonoBehaviour
{
    [SerializeField] MenuElement menuElement;
    Camera cam;

    const float buttonPanelHeight = 400f;
    MenuType currentMenuType = MenuType.MAIN;

    // selector, confirmer and background
    const int nativeChildCount = 3;

    const int selectorIndex = 0;
    const int confirmerIndex = 1;
    const float oneUnitLocalSize = 8.1f;
    int selectedElementIndex = -1;

    Color selectedElementColor = new Color(0.4245283f, 0.4245283f, 0.4245283f);
    Color defaultElementColor = new Color(0f, 0f, 0f);

    
    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        float halfRestScreenHeight = (cam.orthographicSize * 2f - buttonPanelHeight) / 2;
        float yPos = -cam.orthographicSize + buttonPanelHeight + halfRestScreenHeight;
        transform.localPosition = new Vector3(0f, yPos, 0f);
        CreateMenu(MenuType.MAIN);
    }

    private void CreateMenu(MenuType type)
    {
        currentMenuType = type;
        ClearMenu();
        float top = 0;
        switch (type)
        {
            case MenuType.MAIN:
                top = TopYPosition(4);
                AddMenuElement(MenuElement.SETTINGS, NextElementYPos(top, 0));
                AddMenuElement(MenuElement.HELP, NextElementYPos(top, 1));
                AddMenuElement(MenuElement.RESTART, NextElementYPos(top, 2));
                AddMenuElement(MenuElement.QUIT, NextElementYPos(top, 3));
                break;
            case MenuType.SETTINGS:
                top = TopYPosition(3);
                AddMenuElement(MenuElement.SOUND, NextElementYPos(top, 0));
                AddMenuElement(MenuElement.MUSIC, NextElementYPos(top, 1));
                AddMenuElement(MenuElement.BACK, NextElementYPos(top, 2));
                break;
            case MenuType.RESTART:
                top = TopYPosition(2);
                AddMenuElement(MenuElement.RESTART, NextElementYPos(top, 0));
                AddMenuElement(MenuElement.BACK, NextElementYPos(top, 1));
                break;
            case MenuType.QUIT:
                top = TopYPosition(2);
                AddMenuElement(MenuElement.QUIT, NextElementYPos(top, 0));
                AddMenuElement(MenuElement.BACK, NextElementYPos(top, 1));
                break;
            case MenuType.HELP:
                top = TopYPosition(1);
                AddMenuElement(MenuElement.BACK, NextElementYPos(top, 0));
                break;
        }
        selectedElementIndex = 0;
        SetSelectorsPosition(top, false);
    }


    private void SetSelectorsPosition(float posY, bool smooth)
    {
        Vector3 oldPos = transform.GetChild(selectorIndex).transform.localPosition;
        transform.GetChild(selectorIndex).transform.localPosition = new Vector3(oldPos.x, posY, oldPos.z);
        oldPos = transform.GetChild(confirmerIndex).transform.localPosition;
        transform.GetChild(confirmerIndex).transform.localPosition = new Vector3(oldPos.x, posY, oldPos.z);
    }

    private float TopYPosition(int numberOfElements)
    {
        float pos = (numberOfElements * 2 - 1) * oneUnitLocalSize / 2f;
        return pos;
    }

    private float NextElementYPos(float topYPos, int index)
    {
        return topYPos - index * oneUnitLocalSize * 2f;
    }

    private void ClearMenu()
    {
        for (int i = 0; i < transform.childCount - nativeChildCount; ++i)
        {
            Destroy(transform.GetChild(i + 3).gameObject);
        }
    }

    private void AddMenuElement(int elemenType, float localYPos)
    {
        MenuElement element = Instantiate(menuElement, transform.position, Quaternion.identity);
        element.SetElement(elemenType);
        element.transform.SetParent(transform);
        element.transform.localPosition = new Vector3(element.transform.localPosition.x, localYPos, element.transform.localPosition.z);
        element.transform.localScale = new Vector3(.4f, 0.09090908f, 1f);
    }

    public void Activate()
    {
        switch(currentMenuType)
        {
            case MenuType.MAIN:
                switch(selectedElementIndex)
                {
                    case 0:
                        // GO TO SETTINGS
                        CreateMenu(MenuType.SETTINGS);
                        break;
                    case 1:
                        // GO TO HELP
                        CreateMenu(MenuType.HELP);
                        break;
                    case 2:
                        // GO TO RESTART
                        CreateMenu(MenuType.RESTART);
                        break;
                    case 3:
                        // GO TO QUIT
                        CreateMenu(MenuType.QUIT);
                        break;
                }
                break;
            case MenuType.SETTINGS:
                switch(selectedElementIndex)
                {
                    // GO TO SOUND SETTINGS
                    case 0:
                        break;
                    // GO TO MUSIC SETTINGS
                    case 1:
                        break;
                    // GO BACK TO MAIN
                    case 2:
                        CreateMenu(MenuType.MAIN);
                        break;
                }
                break;
            case MenuType.RESTART:
                switch (selectedElementIndex)
                {
                    // RESTART LEVEL
                    case 0:
                        FindObjectOfType<PauseButton>().ResumeGame();
                        SceneManager.LoadScene(0);
                        break;
                    // GO BACK TO MAIN
                    case 1:
                        CreateMenu(MenuType.MAIN);
                        break;
                }
                break;
            case MenuType.QUIT:
                switch(selectedElementIndex)
                {
                    // QUIT TO MAIN MENU
                    case 0:
                        break;
                    // GO BACK TO MAIN
                    case 1:
                        CreateMenu(MenuType.MAIN);
                        break;
                }
                break;
            case MenuType.HELP:
                switch(selectedElementIndex)
                {
                    // GO BACK TO MAIN
                    case 0:
                        CreateMenu(MenuType.MAIN);
                        break;
                }
                break;
        }
    }

    public void SelectNextElement()
    {
        int numberOfElements = transform.childCount - nativeChildCount;
        int offset = nativeChildCount;
        if (offset + selectedElementIndex == (transform.childCount - 1))
        {
            selectedElementIndex = 0;
        }
        else
        {
            selectedElementIndex++;
        }

        SetSelectorsPosition(transform.GetChild(offset + selectedElementIndex).transform.localPosition.y, true);
    }
}
