using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroSlider : MonoBehaviour
{
    enum State { FLIP_PAGE, FLIP_SLIDE, AWAIT, REMOVE_SLIDES, AWAIT_END }

    [SerializeField] AudioClip flip;
    List<Animator> slides;


    int[] slidesInPage = { 4, 4, 3, 8, 7};
    int[] hasText = {0,  1, 0,  1, 0,  1, 0,     0,  1, 0,  1, 0,  1, 0,     1, 0,  1, 0,  1,0,     0, 0, 0, 0, 0,   1, 0  , 0,   1, 0,      0, 0, 0, 0, 0, 1, 0  , 1, 0   };

    const float flipSlideTime = 5f;
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
    const string AppearAnimationName = "SliderAppear";

    private void Start()
    {
        SetStateFirstFlipSlide();
        slides = new List<Animator>();
        for (int i = 0; i < 39; ++i)
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
                if(pressed)
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
                    if (hasText[currentTotalIndex] == 1)
                    {
                        slides[currentTotalIndex].gameObject.SetActive(true);
                        slides[currentTotalIndex + 1].gameObject.SetActive(true);
                        currentTotalIndex += 2;
                    }
                    else
                    {
                        slides[currentTotalIndex].gameObject.SetActive(true);
                        currentTotalIndex += 1;
                    }


                    currentSlideInPage++;

                    if (currentSlideInPage == slidesInPage[currentPage])
                    {
                        if (currentTotalIndex == hasText.Length)
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
                if (timer > 0)
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
                if (pressed)
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
                    SceneManager.LoadScene(C.MainMenuSceneIndex);
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
        switch (state)
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
        if (state == State.AWAIT || state == State.AWAIT_END)
        {
            if (!pressed)
            {
                pressed = true;
            }
        }

        if(state == State.FLIP_SLIDE)
        {
            if(timer > .5f)
            {
                if (!pressed)
                {
                    pressed = true;
                }
            }
        }
    }
}
