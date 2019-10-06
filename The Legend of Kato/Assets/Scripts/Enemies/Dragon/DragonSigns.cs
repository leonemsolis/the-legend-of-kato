using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSigns : MonoBehaviour
{
    List<KrakenSign> signs;
    float warnTime = 2f;

    private void OnEnable()
    {
        signs = new List<KrakenSign>();
        for (int i = 0; i < 5; ++i)
        {
            signs.Add(transform.GetChild(i).gameObject.GetComponent<KrakenSign>());
            signs[i].Deactivate();
        }
    }

    public void Enable(int pos)
    {
        StartCoroutine(StartWarn(pos));
    }

    private IEnumerator StartWarn(int index)
    {
        signs[index].Activate();
        yield return new WaitForSeconds(warnTime);
        signs[index].Deactivate();
    }
}
