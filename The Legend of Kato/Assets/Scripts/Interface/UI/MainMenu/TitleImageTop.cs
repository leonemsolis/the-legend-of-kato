using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleImageTop : MonoBehaviour
{
    [SerializeField] AudioClip sword;
    const string activeAnimationName = "Active";

    public void Activate()
    {
        FindObjectOfType<SoundPlayer>().PlaySound(sword);
        GetComponent<Animator>().Play(activeAnimationName);
    }
}
