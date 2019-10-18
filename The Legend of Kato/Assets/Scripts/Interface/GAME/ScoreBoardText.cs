using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBoardText : MonoBehaviour
{
    TextMeshProUGUI tmp;
    int score;

    bool practice;

    void Start()
    {
        practice = PlayerPrefs.GetInt(C.PREFS_PRACTICE_MODE, 0) == 1;

        score = PlayerPrefs.GetInt(C.PREFS_MONEY, 0);
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = score.ToString();
    }

    public void IncreaseScore(int value)
    {
        if(!practice)
        {
            FindObjectOfType<RecordTracker>().SoulCollected();
            if (score + value <= 99)
            {
                score += value;
                tmp.text = score.ToString();
            }
        }
    }

    public void DecreaseScore(int value)
    {
        score -= value;
        tmp.text = score.ToString();
    }

    public int GetCurrentScore()
    {
        return score;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(C.PREFS_MONEY, score);
        PlayerPrefs.Save();
    }
}
