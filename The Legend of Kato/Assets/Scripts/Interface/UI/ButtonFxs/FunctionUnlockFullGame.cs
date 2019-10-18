using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FunctionUnlockFullGame : FunctionUI
{
    FunctionPromptSpawn fps;

    public void SetPromptSpawner(FunctionPromptSpawn f)
    {
        fps = f;
        transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().SetText("BUY FOR\n"+FindObjectOfType<Purchaser>().GetPrice());
    }

    public override void Function()
    {
        FindObjectOfType<Purchaser>().BuyUnlock();
        fps.ResetUIColliders();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Destroy(transform.parent.gameObject);
    }
}
