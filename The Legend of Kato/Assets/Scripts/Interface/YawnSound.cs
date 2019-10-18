using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YawnSound : MonoBehaviour
{
    [SerializeField] AudioClip yawn;

    public void PlaySound()
    {
        FindObjectOfType<SoundPlayer>().PlaySound(yawn);
    }
}
