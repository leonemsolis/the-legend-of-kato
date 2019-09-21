using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBarMask : MonoBehaviour
{
    float initialX;
    private void Start()
    {
        initialX = transform.position.x;
    }

    public void SetPercentage(float percent)
    {
        transform.localPosition = new Vector3(initialX + percent * 600f, 0f, 0f);
    }

}
