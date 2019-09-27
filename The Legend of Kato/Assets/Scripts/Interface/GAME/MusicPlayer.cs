using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    enum MusicState { NONE, MENU, LEVEL, BOSS};

    [SerializeField] AudioClip MainMenuMusic;
    [SerializeField] AudioClip LevelBaseMusic;
    [SerializeField] AudioClip LevelBossMusic;

    AudioSource _as;

    MusicState lastState = MusicState.NONE;
    MusicState currentState = MusicState.NONE;

    bool fade = false;
    AudioClip fadeInClip;
    const float fadeOutTime = .2f;
    const float fadeInTime = 1f;
    const float highVolume = 1f;
    const float lowVolume = 0f;
    const float deltaOutVolume = (highVolume - lowVolume) / fadeOutTime;
    const float deltaInVolume = (highVolume - lowVolume) / fadeInTime;

    bool fadeOut = true;
    float timer = fadeOutTime;

    bool muted = false;

    void Start()
    {
        _as = GetComponent<AudioSource>();
        _as.loop = true;
        if(PlayerPrefs.GetInt(C.PREFS_MUSIC, 1) == 0)
        {
            Mute();
        }
    }

    private void Update()
    {
        if(fade)
        {
            Fade();
        }

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case C.MainMenuSceneIndex:
            case C.LevelSelectionSceneIndex:
            case C.SettingsSceneIndex:
            case C.IntroSceneIndex:
                currentState = MusicState.MENU;
                break;
            case C.Level0SceneIndex:
            case C.Level1SceneIndex:
            case C.Level2SceneIndex:
            case C.Level3SceneIndex:
                currentState = MusicState.LEVEL;
                if(FindObjectOfType<PlayerRoomDetector>() != null)
                {
                    if(FindObjectOfType<PlayerRoomDetector>().GetCurrentRoom() != null && FindObjectOfType<PlayerRoomDetector>().GetCurrentRoom().RoomID == C.RoomIDBoss)
                    {
                        if(FindObjectOfType<BossGate>().IsOpened() || FindObjectOfType<BossKey>() != null)
                        {
                            currentState = MusicState.LEVEL;
                        }
                        else
                        {
                            currentState = MusicState.BOSS;
                        }
                    }
                }
                break;
        }
        PlayNewMusicClip();
    }

    void PlayNewMusicClip()
    {
        if(currentState != lastState)
        {
            switch (currentState)
            {
                case MusicState.MENU:
                    //_as.clip = MainMenuMusic;
                    fadeInClip = MainMenuMusic;
                    break;
                case MusicState.LEVEL:
                    //_as.clip = LevelBaseMusic;
                    fadeInClip = LevelBaseMusic;
                    break;
                case MusicState.BOSS:
                    //_as.clip = LevelBossMusic;
                    fadeInClip = LevelBossMusic;
                    break;
            }
            lastState = currentState;
            fade = true;
            //_as.Play();
        }
    }

    public void Mute()
    {
        muted = true;
        _as.volume = 0f;
    }

    public void Unmute()
    {
        muted = false;
        _as.volume = 1f;
    }

    private void Fade()
    {
        if(muted)
        {
            fade = false;
            _as.clip = fadeInClip;
            _as.Play();
        }
        else
        {
            if (fadeOut)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                    _as.volume -= Time.deltaTime * deltaOutVolume;
                }
                else
                {
                    _as.volume = lowVolume;
                    fadeOut = false;
                    timer = fadeInTime;
                    _as.clip = fadeInClip;
                    _as.Play();
                }
            }
            else
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                    _as.volume += Time.deltaTime * deltaInVolume;
                }
                else
                {
                    _as.volume = highVolume;
                    fadeOut = true;
                    timer = fadeOutTime;

                    fade = false;
                }
            }
        }
    }
}
