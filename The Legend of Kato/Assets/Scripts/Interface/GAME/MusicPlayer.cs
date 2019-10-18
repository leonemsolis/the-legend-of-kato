using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    enum MusicState { NONE, INTRO, MENU, LEVEL, BOSS};

    [SerializeField] AudioClip MainMenuMusic;
    [SerializeField] AudioClip LevelBaseMusic;
    [SerializeField] AudioClip LevelBossMusic;
    [SerializeField] AudioClip IntroMusic;

    AudioSource _as;

    MusicState lastState = MusicState.NONE;
    MusicState currentState = MusicState.NONE;

    bool fade = false;
    AudioClip fadeInClip;
    const float fadeOutTime = .2f;
    const float fadeInTime = 1f;
    const float highVolume = .7f;
    const float lowVolume = 0f;
    const float deltaOutVolume = (highVolume - lowVolume) / fadeOutTime;
    const float deltaInVolume = (highVolume - lowVolume) / fadeInTime;

    bool fadeOut = true;
    float timer = fadeOutTime;

    bool muted = false;

    void Start()
    {
        _as = GetComponent<AudioSource>();
        _as.volume = highVolume;
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
            case C.LevelModeSceneIndex:
            case C.RecordsSceneIndex:
                currentState = MusicState.MENU;
                break;
            case C.DeathSceneIndex:
            case C.IntroSceneIndex:
            case C.OutroSceneIndex:
                currentState = MusicState.INTRO;
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
                        if(FindObjectOfType<BossGate>() != null && FindObjectOfType<BossGate>().IsOpened())
                        {
                            currentState = MusicState.LEVEL;
                        }
                        else if(FindObjectOfType<PlayerAnimator>().Won())
                        {
                            currentState = MusicState.INTRO;
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
                    fadeInClip = MainMenuMusic;
                    break;
                case MusicState.LEVEL:
                    fadeInClip = LevelBaseMusic;
                    break;
                case MusicState.BOSS:
                    fadeInClip = LevelBossMusic;
                    break;
                case MusicState.INTRO:
                    fadeInClip = IntroMusic;
                    break;
            }
            lastState = currentState;
            fade = true;
        }
    }

    public void Mute()
    {
        muted = true;
        _as.volume = lowVolume;
    }

    public void Unmute()
    {
        muted = false;
        _as.volume = highVolume;
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
                    timer -= Time.unscaledTime;
                    _as.volume -= Time.unscaledTime * deltaOutVolume;
                    if (_as.volume < lowVolume)
                    {
                        _as.volume = lowVolume;
                    }
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
                    timer -= Time.unscaledTime;
                    _as.volume += Time.unscaledTime * deltaInVolume;
                    if(_as.volume > highVolume)
                    {
                        _as.volume = highVolume;
                    }
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
