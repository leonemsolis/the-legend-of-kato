using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionUIScaler : MonoBehaviour
{
    [SerializeField] bool fillKeys;

    const float height = 725f;
    const float width = 693.75f;

    void Start()
    {
        float screenWidth = 1000f;
        float scaleFactor = screenWidth / width;

        // To leave a little free space
        scaleFactor -= .1f;

        transform.localScale = new Vector3(scaleFactor, scaleFactor, 0f);

        if(fillKeys)
        {
            FindObjectOfType<LevelSelectionKeyHolder>().FillKeys(scaleFactor);
        }
    }
}
