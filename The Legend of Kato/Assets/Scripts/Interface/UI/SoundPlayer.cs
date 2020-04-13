using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] SoundClip soundClipPrefab;

    bool muted = false;
    bool simPlayed = false;

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
            if(Mathf.Abs(originPosition.x - transform.position.x) < 600f && Mathf.Abs(originPosition.y - transform.position.y) < Camera.main.orthographicSize)
            {
                SoundClip s = Instantiate(soundClipPrefab, transform.localPosition, Quaternion.identity);
                s.PlayClip(clip);
            }
        }
    }

    public void PlaySimultaniousSound(AudioClip clip, Vector2 originPosition)
    {
        if (!muted && !simPlayed)
        {
            if (Mathf.Abs(originPosition.x - transform.position.x) < 600f && Mathf.Abs(originPosition.y - transform.position.y) < Camera.main.orthographicSize)
            {
                SoundClip s = Instantiate(soundClipPrefab, transform.localPosition, Quaternion.identity);
                s.PlayClip(clip);
                simPlayed = true;
                StartCoroutine(ResetSimPlayer());
            }
        }
    }

    IEnumerator ResetSimPlayer()
    {
        yield return new WaitForSeconds(.4f);
        simPlayed = false;
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
