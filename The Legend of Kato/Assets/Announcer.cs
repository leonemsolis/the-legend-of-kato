using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Announcer : MonoBehaviour
{
    [SerializeField] Sprite goText;
    [SerializeField] Sprite tutorialText;
    [SerializeField] Sprite entranceText;
    [SerializeField] Sprite depthsText;
    [SerializeField] Sprite oceanBedText;

    [SerializeField] AudioClip sound;

    const string animationName = "Go";

    Animator animator;
    SpriteRenderer spriteRenderer;

    bool ended = false;

    PauseButton pauseButton;

    void Start()
    {
        float freeSpace = Camera.main.orthographicSize * 2f - C.ButtonPanelHeight - C.InfoPanelHeight;
        transform.localPosition = new Vector3(0f, Camera.main.orthographicSize - C.InfoPanelHeight - freeSpace / 2f, 0f);

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case C.Level0SceneIndex:
                spriteRenderer.sprite = tutorialText;
                break;
            case C.Level1SceneIndex:
                spriteRenderer.sprite = entranceText;
                break;
            case C.Level2SceneIndex:
                spriteRenderer.sprite = depthsText;
                break;
            case C.Level3SceneIndex:
                spriteRenderer.sprite = oceanBedText;
                break;
        }


        pauseButton = FindObjectOfType<PauseButton>();
        pauseButton.gameObject.GetComponent<Collider2D>().enabled = false;
    }

    public void PlaySound()
    {
        FindObjectOfType<SoundPlayer>().PlaySound(sound);
    }

    private void Update()
    {
        if(!ended)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                spriteRenderer.sprite = goText;
                animator.Play(animationName);
                ended = true;
            }
        }
        else
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                pauseButton.gameObject.GetComponent<Collider2D>().enabled = true;
                Destroy(gameObject);
            }
        }
    }
}
