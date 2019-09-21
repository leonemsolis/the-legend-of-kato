using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardText : MonoBehaviour
{
    Text textField;
    int score;
    void Start()
    {
        score = PlayerPrefs.GetInt(C.PREFS_MONEY, 0);
        textField = GetComponent<Text>();
        textField.text = score.ToString();
    }

    public void IncreaseScore(int value)
    {
        if(score + value <= 99)
        {
            score += value;
            textField.text = score.ToString();
        }
    }

    public void DecreaseScore(int value)
    {
        score -= value;
        textField.text = score.ToString();
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
