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
        textField.text = "0";
    }

    public void AddScore(int value)
    {
        if(score + value <= 9999)
        {
            score+=value;
            textField.text = score.ToString();
        }
    }
}
