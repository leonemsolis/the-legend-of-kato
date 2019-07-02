using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Transform selectors;

    int currentElementIndex = 0;
    int numberOfElements = 5;
    float topElementYPos;
    float gapSize;

    void Start()
    {
        float unitPhysicPixelSize = Screen.width / 1000f;
        float cameraHeight = Screen.height / unitPhysicPixelSize;

        float freeSpace = cameraHeight - (C.InfoPanelHeight + C.ButtonPanelHeight);
        gapSize = (freeSpace - (numberOfElements * C.Unit)) / (numberOfElements + 1);

        topElementYPos = Camera.main.orthographicSize - C.InfoPanelHeight - gapSize - C.Unit / 2f;

        for(int i = 0; i < numberOfElements; ++i)
        {
            transform.GetChild(i).transform.position = new Vector3(0f, topElementYPos - i * (C.Unit + gapSize), 0f);
        }

        selectors.position = new Vector3(0f, topElementYPos, 0f);
    }

    public void SelectNextElement()
    {
        currentElementIndex++;
        if(currentElementIndex == numberOfElements)
        {
            currentElementIndex = 0;
        }

        selectors.position = new Vector3(0f, topElementYPos - currentElementIndex * (C.Unit + gapSize), 0f);
    }

    public void ActivateCurrentElement()
    {
        switch(currentElementIndex)
        {
            case 0:
                Destroy(selectors.gameObject);
                SceneManager.LoadScene(C.Level1SceneIndex);
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
}
