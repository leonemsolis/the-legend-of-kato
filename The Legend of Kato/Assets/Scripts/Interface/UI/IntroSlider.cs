using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSlider : MonoBehaviour
{

    enum State {FLIP_PAGE, FLIP_SLIDE, AWAIT, REMOVE_SLIDES, AWAIT_END}

    [SerializeField] AudioClip flip;
    [SerializeField] GameObject tutorialPromptPrefab;
    List<Animator> slides;


    int[] slidesInPage = {3, 4, 3, 4, 3};

    const float flipSlideTime = 2f;
    const float flipFirstSlideTime = .5f;
    const float flipPageTime = .4f;
    const float removeTime = .2f;
    float timer = flipPageTime;

    int currentPage = 0;
    int currentSlideInPage = 0;
    int currentTotalIndex = 0;

    bool pressed = false;


    State state;

    const string DisappearAnimationName = "SlideDisappear";
    const string AppearAnimationName = "SlideAppear";


    private void Start()
    {
        SetStateFirstFlipSlide();
        slides = new List<Animator>();
        for(int i = 0; i < 34; ++i)
        {
            slides.Add(transform.GetChild(i).gameObject.GetComponent<Animator>());
            slides[i].gameObject.SetActive(false);
        }
    }


    private void Update()
    {
        switch (state)
        {
            case State.FLIP_SLIDE:
                if (pressed)
                {
                    pressed = !pressed;
                    timer = 0;
                }
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    slides[currentTotalIndex * 2].gameObject.SetActive(true);
                    slides[currentTotalIndex * 2].Play(AppearAnimationName);
                    slides[currentTotalIndex * 2 + 1].gameObject.SetActive(true);
                    slides[currentTotalIndex * 2 + 1].Play(AppearAnimationName);

                    currentTotalIndex++;
                    currentSlideInPage++;

                    if (currentSlideInPage == slidesInPage[currentPage])
                    {
                        if(currentPage == slidesInPage.Length - 1)
                        {
                            SetState(State.AWAIT_END);
                        }
                        else
                        {
                            SetState(State.AWAIT);
                        }
                    }
                    else
                    {
                        SetState(State.FLIP_SLIDE);
                    }
                }
                break;
            case State.FLIP_PAGE:
                if(timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    currentPage++;
                    currentSlideInPage = 0;
                    SetStateFirstFlipSlide();
                }
                break;
            case State.REMOVE_SLIDES:
                for (int i = 0; i < slides.Count; ++i)
                {
                    if (slides[i].gameObject.activeInHierarchy)
                    {
                        slides[i].Play(DisappearAnimationName);
                        StartCoroutine(RemoveSlide(i));
                    }
                }
                SetState(State.FLIP_PAGE);
                break;
            case State.AWAIT:
                if(pressed)
                {
                    pressed = false;
                    FindObjectOfType<SoundPlayer>().PlaySound(flip);
                    SetState(State.REMOVE_SLIDES);
                }
                break;
            case State.AWAIT_END:
                if (pressed)
                {
                    pressed = false;
                    if (PlayerPrefs.GetInt(C.PREFS_FIRST_LAUNCH, 1) == 1)
                    {
                        GetComponent<BoxCollider2D>().enabled = false;
                        Instantiate(tutorialPromptPrefab, Vector3.zero, Quaternion.identity);
                        PlayerPrefs.SetInt(C.PREFS_FIRST_LAUNCH, 0);
                    }
                    else
                    {
                        SceneManager.LoadScene(C.LevelModeSceneIndex);
                    }
                }
                break;
        }
    }

    IEnumerator RemoveSlide(int i)
    {
        yield return new WaitForSeconds(removeTime);
        slides[i].gameObject.SetActive(false);
    }

    private void SetState(State s)
    {
        state = s;
        switch(state)
        {
            case State.FLIP_PAGE:
                timer = flipPageTime;
                break;
            case State.FLIP_SLIDE:
                timer = flipSlideTime;
                break;
            case State.REMOVE_SLIDES:
                timer = 0f;
                break;
            case State.AWAIT:
                break;
            case State.AWAIT_END:
                break;
        }
    }

    private void SetStateFirstFlipSlide()
    {
        state = State.FLIP_SLIDE;
        timer = flipFirstSlideTime;
    }

    private void OnMouseDown()
    {
        if(state == State.AWAIT || state == State.AWAIT_END)
        {
            if(!pressed)
            {
                pressed = true;
            }
        }
        if (state == State.FLIP_SLIDE)
        {
            if (timer > .5f)
            {
                if (!pressed)
                {
                    pressed = true;
                }
            }
        }
    }
}
