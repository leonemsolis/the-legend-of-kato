using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RecordTracker : MonoBehaviour
{
    float startTime = 0f;
    int monstersKilled = 0;
    int soulsCollected = 0;


    void Start()
    {
        startTime = Time.time;
        monstersKilled = PlayerPrefs.GetInt(C.PREFS_MONSTERS_KILLED, 0);
        soulsCollected = PlayerPrefs.GetInt(C.PREFS_SOULS_COLLECTED, 0);
    }

    public void CompleteLevel()
    {
        float time = Time.time - startTime;
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case C.Level0SceneIndex:
                if (PlayerPrefs.GetFloat(C.PREFS_TUTORIAL_BEST_TIME, 99999999999999999999999999999999999999f) > time)
                {
                    PlayerPrefs.SetFloat(C.PREFS_TUTORIAL_BEST_TIME, time);
                }
                break;
            case C.Level1SceneIndex:
                if (PlayerPrefs.GetFloat(C.PREFS_ENTRANCE_BEST_TIME, 99999999999999999999999999999999999999f) > time)
                {
                    PlayerPrefs.SetFloat(C.PREFS_ENTRANCE_BEST_TIME, time);
                }
                break;
            case C.Level2SceneIndex:
                if (PlayerPrefs.GetFloat(C.PREFS_DEPTHS_BEST_TIME, 99999999999999999999999999999999999999f) > time)
                {
                    PlayerPrefs.SetFloat(C.PREFS_DEPTHS_BEST_TIME, time);
                }
                break;
            case C.Level3SceneIndex:
                if (PlayerPrefs.GetFloat(C.PREFS_OCEAN_BED_BEST_TIME, 99999999999999999999999999999999999999f) > time)
                {
                    PlayerPrefs.SetFloat(C.PREFS_OCEAN_BED_BEST_TIME, time);
                }
                break;
        }
    }

    public void Dead()
    {
        float time = Time.time - startTime;
        PlayerPrefs.SetInt(C.PREFS_DEATH_COUNT, PlayerPrefs.GetInt(C.PREFS_DEATH_COUNT, 0) + 1);
    }

    public void MonsterKill()
    {
        monstersKilled++;
    }

    public void SoulCollected()
    {
        soulsCollected++;
    }

    public void Save()
    {
        PlayerPrefs.SetInt(C.PREFS_MONSTERS_KILLED, monstersKilled);
        PlayerPrefs.SetInt(C.PREFS_SOULS_COLLECTED, soulsCollected);
        PlayerPrefs.Save();
    }
}
