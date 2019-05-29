using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActive : MonoBehaviour
{
    [SerializeField] Sprite up;
    [SerializeField] Sprite down;

    PlayerController player;
    readonly RuntimePlatform platform = Application.platform;
    Pause pause;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        pause = FindObjectOfType<Pause>();
        player = FindObjectOfType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    CheckTouch(Input.GetTouch(i).position);
                }
            }
        }
        else if (platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.OSXEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckTouch(Input.mousePosition);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                TouchButton();
            }
        }
    }

    private void CheckTouch(Vector3 pos)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D hit = Physics2D.OverlapPoint(touchPos);

        if (hit && hit == gameObject.GetComponent<Collider2D>())
        {
            TouchButton();
        }
    }


    private void TouchButton()
    {
        PressButton();
        if (pause.IsGameRunning())
        {
            player.Jump();
        }
        else
        {
            FindObjectOfType<PausePanel>().Activate();
        }
    }

    public void PressButton()
    {
        spriteRenderer.sprite = down;
        StartCoroutine(ResetButton());
    }

    private IEnumerator ResetButton()
    {
        yield return new WaitForSecondsRealtime(.1f);
        spriteRenderer.sprite = up;
    }
}
