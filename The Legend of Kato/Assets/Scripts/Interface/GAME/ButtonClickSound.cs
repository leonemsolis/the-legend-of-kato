using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    [SerializeField] List<AudioClip> sounds;

    public void PlayClick()
    {
        FindObjectOfType<SoundPlayer>().PlaySound(sounds[Random.Range(0, sounds.Count)]);
    }
}
