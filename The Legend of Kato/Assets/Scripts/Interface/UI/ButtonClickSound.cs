using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] List<AudioClip> sounds;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClick()
    {
        audioSource.clip = sounds[Random.Range(0, sounds.Count)];
        audioSource.Play();
    }
}
