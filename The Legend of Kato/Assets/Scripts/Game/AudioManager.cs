using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    public static int music = 1;
    public static int sounds = 1;

    private void Start()
    {
        // Check if instance exists
        if (instance == null) // Instance was found
        {
            //TODO: REMOVE?
            Monetization.Initialize((Application.platform == RuntimePlatform.Android) ? "3315936" : "3315937", false);
            instance = this; // Set reference to intance of this object
        }
        else // Instance already exists
        {
            Destroy(gameObject);
        }

        // Do not destroy object when switching scene
        DontDestroyOnLoad(gameObject);

        LoadSettings();
    }

    private void Update()
    {
        if(Camera.main != null)
        {
            transform.position = Camera.main.transform.position;
        }
    }

    // Load settings
    public void LoadSettings()
    {
        music = PlayerPrefs.GetInt(C.PREFS_MUSIC, 1);
        sounds = PlayerPrefs.GetInt(C.PREFS_SOUNDS, 1);

        if(music == 0)
        {
            FindObjectOfType<MusicPlayer>().Mute();
        }
        else
        {
            FindObjectOfType<MusicPlayer>().Unmute();
        }

        if(sounds == 0)
        {
            FindObjectOfType<SoundPlayer>().Mute();
        }
        else
        {
            FindObjectOfType<SoundPlayer>().Unmute();
        }
    }
}
