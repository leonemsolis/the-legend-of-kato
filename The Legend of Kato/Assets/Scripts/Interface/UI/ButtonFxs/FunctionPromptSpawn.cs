using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionPromptSpawn : FunctionUI
{
    [SerializeField] GameObject prompt;

    List<GameObject> wasEnabled;

    private void OnEnable()
    {
        wasEnabled = new List<GameObject>();
    }

    public override void Function()
    {
        wasEnabled.Clear();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag(C.ButtonUITag))
        {
            if(g.GetComponent<Collider2D>().enabled)
            {
                wasEnabled.Add(g);
                g.GetComponent<Collider2D>().enabled = false;
            }
        }
        Instantiate(prompt, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f), Quaternion.identity);


        FindObjectOfType<FunctionCancelPrompt>().SetPromptSpawner(this);
    }

    public void ResetUIColliders()
    {
        foreach(GameObject g in wasEnabled)
        {
            g.GetComponent<Collider2D>().enabled = true;
        }
    }
}
