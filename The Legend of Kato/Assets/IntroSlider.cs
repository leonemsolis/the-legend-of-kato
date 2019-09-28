using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSlider : MonoBehaviour
{
    [SerializeField] List<Sprite> slideSprites;
    [SerializeField] GameObject slidePrefab;
    [SerializeField] GameObject tutorialPromptPrefab;


    int[] slidesInPage = {3, 4, 3, 4, 3};

    const float slideNormalTime = 1.5f;
    const float slideNextPageTime = .1f;
    const float removeTime = .2f;
    float timer = 1f;

    int currentPage = 0;
    int currentSlideInPage = 1;

    int layer = 0;

    int spriteIndex = 0;

    const string DisappearAnimationName = "SlideDisappear";

    bool await = false;

    void Update()
    {
        if(!await)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                if (currentSlideInPage == slidesInPage[currentPage])
                {
                    currentPage++;
                    currentSlideInPage = 1;
                    await = true;
                    timer = slideNextPageTime;
                }
                else
                {
                    currentSlideInPage++;
                    timer = slideNormalTime;
                }

                GameObject slide = Instantiate(slidePrefab, Vector3.zero, Quaternion.identity);
                slide.transform.SetParent(transform);
                slide.GetComponent<SpriteRenderer>().sprite = slideSprites[spriteIndex++];
                slide.GetComponent<SpriteRenderer>().sortingOrder = layer++;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (CheckTouch(Input.mousePosition))
                {
                    if (currentPage == slidesInPage.Length)
                    {
                        for (int i = 0; i < transform.childCount; ++i)
                        {
                            transform.GetChild(i).GetComponent<Animator>().Play(DisappearAnimationName);
                        }
                        if (PlayerPrefs.GetInt(C.PREFS_FIRST_LAUNCH, 1) == 1)
                        {
                            GetComponent<BoxCollider2D>().enabled = false;
                            Instantiate(tutorialPromptPrefab, Vector3.zero, Quaternion.identity);
                            PlayerPrefs.SetInt(C.PREFS_FIRST_LAUNCH, 0);
                        }
                        else
                        {
                            SceneManager.LoadScene(C.SettingsSceneIndex);
                        }
                    }
                    else
                    {
                        RemovePreviousSlides();
                    }
                }
            }
        }
    }

    private void RemovePreviousSlides()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            transform.GetChild(i).GetComponent<Animator>().Play(DisappearAnimationName);
        }
        StartCoroutine(WaitToRemove());
    }

    private IEnumerator WaitToRemove()
    {
        yield return new WaitForSeconds(removeTime);
        for (int i = 0; i < transform.childCount; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        await = false;
    }


    private bool CheckTouch(Vector3 pos)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D hit = Physics2D.OverlapPoint(touchPos);

        return hit && hit == gameObject.GetComponent<Collider2D>();
    }
}
