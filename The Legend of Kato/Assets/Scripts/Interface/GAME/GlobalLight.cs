using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class GlobalLight : MonoBehaviour
{

    enum ParameterType {G, B, INTENSITY};

    Light2D _light;

    [SerializeField] float minG = .44f;
    [SerializeField] float maxG = .85f;

    [SerializeField] float minB = .44f;
    [SerializeField] float maxB = .85f;

    [SerializeField] float minIntensity = .6f;
    [SerializeField] float maxIntensity = .9f;

    [SerializeField] float minTimeGrad = 1f;
    [SerializeField] float maxTimeGrad = 3f;

    void Start()
    {
        _light = GetComponent<Light2D>();

        StartCoroutine(Gradient(ParameterType.G));
        StartCoroutine(Gradient(ParameterType.B));
        StartCoroutine(Gradient(ParameterType.INTENSITY));
    }


    private IEnumerator Gradient(ParameterType t)
    {
        float elapsed = 0.0f;

        float start = 1f;
        float end = 1f;

        switch (t)
        {
            case ParameterType.G:
                start = _light.color.g;
                end = Random.Range(minG, maxG);
                break;
            case ParameterType.B:
                start = _light.color.b;
                end = Random.Range(minB, maxB);
                break;
            case ParameterType.INTENSITY:
                start = _light.intensity;
                end = Random.Range(minIntensity, maxIntensity);
                break;
        }

        float duration = Random.Range(minTimeGrad, maxTimeGrad);
        while (elapsed < duration)
        {
            switch(t)
            {
                case ParameterType.G:
                    _light.color = new Color(_light.color.r, Mathf.Lerp(start, end, elapsed / duration), _light.color.b);
                    break;
                case ParameterType.B:
                    _light.color = new Color(_light.color.r, _light.color.g, Mathf.Lerp(start, end, elapsed / duration));
                    break;
                case ParameterType.INTENSITY:
                    _light.intensity = Mathf.Lerp(start, end, elapsed / duration);
                    break;
            }
            elapsed += Time.deltaTime;
            yield return null;
        }

        switch (t)
        {
            case ParameterType.G:
                _light.color = new Color(_light.color.r, end, _light.color.b);
                break;
            case ParameterType.B:
                _light.color = new Color(_light.color.r, _light.color.g, end);
                break;
            case ParameterType.INTENSITY:
                _light.intensity = end;
                break;
        }

        StartCoroutine(Gradient(t));
    }
}
