﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenSigns : MonoBehaviour
{

    List<GameObject> signs;
    float warnTime = 2f;

    private void OnEnable()
    {
        signs = new List<GameObject>();
        for (int i = 0; i < 5; ++i)
        {
            signs.Add(transform.GetChild(i).gameObject);
            signs[i].SetActive(false);
        }

        Enable(3, true);
    }

    // Enable warning signs depending of tentacle position(1-4) and it's side
    public void Enable(int pos, bool left)
    {
        switch (pos)
        {
            case 1:
                if(left)
                {
                    StartCoroutine(StartWarn(0));
                }
                else
                {
                    StartCoroutine(StartWarn(1));
                    StartCoroutine(StartWarn(2));
                }
                break;
            case 2:
                if (left)
                {
                    StartCoroutine(StartWarn(0));
                    StartCoroutine(StartWarn(1));
                }
                else
                {
                    StartCoroutine(StartWarn(2));
                    StartCoroutine(StartWarn(3));
                }
                break;
            case 3:
                if (left)
                {
                    StartCoroutine(StartWarn(1));
                    StartCoroutine(StartWarn(2));
                }
                else
                {
                    StartCoroutine(StartWarn(3));
                    StartCoroutine(StartWarn(4));
                }
                break;
            case 4:
                if (left)
                {
                    StartCoroutine(StartWarn(2));
                    StartCoroutine(StartWarn(3));
                }
                else
                {
                    StartCoroutine(StartWarn(4));
                }
                break;
        }
    }

    private IEnumerator StartWarn(int index)
    {
        signs[index].SetActive(true);
        yield return new WaitForSeconds(warnTime);
        signs[index].SetActive(false);
    }

}
