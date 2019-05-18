using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    bool running = true;
    PausePanel pausePanel;

    private void Start()
    {
        pausePanel = FindObjectOfType<PausePanel>();
        pausePanel.gameObject.SetActive(false);
    }

    public bool IsGameRunning()
    {
        return running;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        running = false;
        pausePanel.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        running = true;
        pausePanel.gameObject.SetActive(false);
    }
}
