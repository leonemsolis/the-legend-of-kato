using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionUIScaler : MonoBehaviour
{
    const float height = 862.5f;

    void Start()
    {
        float screenHeight = Camera.main.orthographicSize * 2f;
        float scaleFactor = screenHeight / height;

        // To leave a little free space
        scaleFactor -= .1f;

        transform.localScale = new Vector3(scaleFactor, scaleFactor, 0f);
        FindObjectOfType<LevelSelectionKeyHolder>().FillKeys(scaleFactor);
    }
}
