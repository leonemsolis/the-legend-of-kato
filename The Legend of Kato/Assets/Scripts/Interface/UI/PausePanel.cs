using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    const float buttonPanelHeight = 400f;

    const int numberOfElements = 4;
    int selectedElementIndex = 0;

    Color selectedElementColor = new Color(0.5849056f, 0.08001069f, 0.08001069f);
    Color defaultElementColor = new Color(0f, 0f, 0f);

    Camera cam;
    
    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        float halfRestScreenHeight = (cam.orthographicSize * 2f - buttonPanelHeight) / 2;
        float yPos = -cam.orthographicSize + buttonPanelHeight + halfRestScreenHeight;
        transform.localPosition = new Vector3(0f, yPos, 0f);
    }

    public void SelectNextElement()
    {
        transform.GetChild(selectedElementIndex).GetComponent<SpriteRenderer>().color = defaultElementColor;
        if(selectedElementIndex + 1 != numberOfElements)
        {
            selectedElementIndex += 1;
        }
        else
        {
            selectedElementIndex = 0;
        }
        transform.GetChild(selectedElementIndex).GetComponent<SpriteRenderer>().color = selectedElementColor;
    }

    public void ActivateSelectedElement()
    {
        Debug.Log(transform.GetChild(selectedElementIndex).name);
    }
}
