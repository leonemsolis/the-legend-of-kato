using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScaler : MonoBehaviour
{
    void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex != C.Level0SceneIndex &&
            SceneManager.GetActiveScene().buildIndex != C.Level1SceneIndex &&
            SceneManager.GetActiveScene().buildIndex != C.Level2SceneIndex &&
            SceneManager.GetActiveScene().buildIndex != C.Level3SceneIndex) {
                Time.timeScale = 1f;
            }
        float unitPhysicPixelSize = Screen.width / 1000f;
        float cameraHeight = Screen.height / unitPhysicPixelSize;
        float cameraSize = cameraHeight / 2f;
        Camera.main.orthographicSize = cameraSize;
    }

    private void Update()
    {
        float unitPhysicPixelSize = Screen.width / 1000f;
        float cameraHeight = Screen.height / unitPhysicPixelSize;
        float cameraSize = cameraHeight / 2f;
        Camera.main.orthographicSize = cameraSize;
    }
}
