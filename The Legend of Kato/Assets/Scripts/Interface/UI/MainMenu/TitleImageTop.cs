using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleImageTop : MonoBehaviour
{
    const string activeAnimationName = "Active";

    public void Activate()
    {
        GetComponent<Animator>().Play(activeAnimationName);
    }
}
