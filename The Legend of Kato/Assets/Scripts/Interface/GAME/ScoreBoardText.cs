using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardText : MonoBehaviour
{
    Text textField;
    int score = 0;
    void Start()
    {
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
}
