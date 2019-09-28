using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionKeyHolder : MonoBehaviour
{
    [SerializeField] LevelSelectionKey keyPrefab;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        OrderKeys();
    }


    void Update()
    {
        // Activate mask if there's no keys
        if(transform.childCount == 0)
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }


    // Order keys descending
    private void OrderKeys()
    {
        int[] keys = new int[3];

        // TODO: REMOVE!
        //keys[0] = PlayerPrefs.GetInt(C.PREFS_KEY_1_STATE, 0);
        keys[0] = 3;
        keys[1] = PlayerPrefs.GetInt(C.PREFS_KEY_2_STATE, 0);
        keys[2] = PlayerPrefs.GetInt(C.PREFS_KEY_3_STATE, 0);

        for(int i = 0; i < 3; ++i)
        {
            for(int j = i + 1; j < 3; ++j)
            {
                if(keys[i] < keys[j]) {
                    int temp = keys[j];
                    keys[j] = keys[i];
                    keys[i] = temp;
                }
            }
        }

        PlayerPrefs.SetInt(C.PREFS_KEY_1_STATE, keys[0]);
        PlayerPrefs.SetInt(C.PREFS_KEY_2_STATE, keys[1]);
        PlayerPrefs.SetInt(C.PREFS_KEY_3_STATE, keys[2]);
        PlayerPrefs.Save();
    }



    public void FillKeys(float scaleFactor)
    {
        int numberOfKeys = 0;
        int[] keys = new int[3];

        keys[0] = PlayerPrefs.GetInt(C.PREFS_KEY_1_STATE, 0);
        keys[1] = PlayerPrefs.GetInt(C.PREFS_KEY_2_STATE, 0);
        keys[2] = PlayerPrefs.GetInt(C.PREFS_KEY_3_STATE, 0);

        for(int i = 0; i < 3; ++i)
        {
            if(keys[i] != 0)
            {
                numberOfKeys++;
            }
        }


        for (int i = 0; i < numberOfKeys; ++i)
        {
            float gap = 87f * scaleFactor;
            float origin = gap + .4f;
            LevelSelectionKey key = Instantiate(keyPrefab, transform.position + new Vector3(origin * i - gap, 3.2f * scaleFactor, 0f), Quaternion.identity);
            key.SetTier(keys[i]);
            key.keyPrefIndex = i;
            key.transform.localScale = new Vector3(scaleFactor, scaleFactor, 0f);
            key.transform.parent = transform;
        }
    }
}
