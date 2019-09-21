using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    public static bool music = true;
    public static bool sounds = true;

    private void Start()
    {
        // Check if instance exists
        if (instance == null) // Instance was found
        {
            instance = this; // Set reference to intance of this object
        }
        else // Instance already exists
        {
            Destroy(gameObject);
        }

        // Do not destroy object when switching scene
        DontDestroyOnLoad(gameObject);

        // Start inirialization
        InitializeManager();
    }

    // Load settings
    private void InitializeManager()
    {
        music = System.Convert.ToBoolean(PlayerPrefs.GetString("music", "true"));
        sounds = System.Convert.ToBoolean(PlayerPrefs.GetString("sounds", "true"));
    }

    // Save settings
    public static void saveSettings()
    {
        PlayerPrefs.SetString("music", music.ToString());
        PlayerPrefs.SetString("sounds", sounds.ToString());
        PlayerPrefs.Save();
    }
}
