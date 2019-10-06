using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFire : MonoBehaviour
{
    float timer = 1.5f;

    public void Setup(float rotationZ)
    {
        transform.parent = FindObjectOfType<DragonHeadController>().transform;
        transform.localScale = new Vector3(10f, 10f, 10f);
        transform.localPosition = new Vector3(0f, -166f, 0f);
        transform.localEulerAngles = new Vector3(0f, 0f, rotationZ);
    }

    private void Update()
    {   
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            FindObjectOfType<DragonHeadController>().AttackDone();
            Destroy(gameObject, .2f);
        }
    }
}
