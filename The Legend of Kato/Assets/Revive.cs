using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Revive : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI description;

    void Start()
    {
        description.SetText("YOU DIED\nREVIVE FOR "+C.RevivePrice+" DEPOSITED SOULS?");
        StartCoroutine(Pause());
    }
    
    private IEnumerator Pause() {
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 0f;
    }

    public void Confirm() {
        PlayerPrefs.SetInt(C.PREFS_DEPOSIT_COUNT, PlayerPrefs.GetInt(C.PREFS_DEPOSIT_COUNT, 0) - C.RevivePrice);
        PlayerPrefs.Save();
        Time.timeScale = C.DefaulTimeScale;
        FindObjectOfType<PlayerAnimator>().Revive();
        Destroy(gameObject);
    }

    public void Cancel() {
        Time.timeScale = C.DefaulTimeScale;
        FindObjectOfType<PlayerAnimator>().DieConfirm();
        Destroy(gameObject);
    }
}
