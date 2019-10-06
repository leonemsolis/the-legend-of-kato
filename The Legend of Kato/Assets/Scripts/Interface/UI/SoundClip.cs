using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundClip : MonoBehaviour
{
    public void PlayClip(AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
        // Destroy clip after playing + delay for security
        Destroy(gameObject, clip.length + .2f);
    }
}
