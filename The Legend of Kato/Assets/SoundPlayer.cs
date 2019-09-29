using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] SoundClip soundClipPrefab;

    bool muted = false;

    void Start()
    {
        if (PlayerPrefs.GetInt(C.PREFS_SOUNDS, 1) == 0)
        {
            Mute();
        }
    }

    public void Mute()
    {
        muted = true;
    }

    public void Unmute()
    {
        muted = false;
    }

    public void PlaySound(AudioClip clip, Vector2 originPosition)
    {
        if(!muted)
        {
            if(Mathf.Abs(originPosition.x - transform.position.x) < 600f && Mathf.Abs(originPosition.y - transform.position.y) < 600f)
            {
                SoundClip s = Instantiate(soundClipPrefab, transform.localPosition, Quaternion.identity);
                s.PlayClip(clip);
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (!muted)
        {
            SoundClip s = Instantiate(soundClipPrefab, transform.localPosition, Quaternion.identity);
            s.PlayClip(clip);
        }
    }
}
