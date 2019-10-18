using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class AdVideoController : MonoBehaviour
{

    private const string placement_id = "rewardedVideo";

    void Start()
    {
        if(PlayerPrefs.GetInt(C.PREFS_GAME_UNLOCKED, 0) == 0)
        {
            Monetization.Initialize((Application.platform == RuntimePlatform.Android) ? "3315936" : "3315937", false);
        }
    }

    public void ShowAdIfNotUnlocked()
    {
        if(PlayerPrefs.GetInt(C.PREFS_GAME_UNLOCKED, 0) == 0)
        {
            StartCoroutine(ShowRewardedAd());
        }
    }

    IEnumerator ShowRewardedAd()
    {
        while (!Monetization.IsReady(placement_id))
        {
            yield return null;
        }
        ShowAd();
    }

    void ShowAd()
    {
        ShowAdCallbacks options = new ShowAdCallbacks();
        //options.finishCallback = HandleShowResult;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent(placement_id) as ShowAdPlacementContent;
        ad.Show(options);
    }

    //void HandleShowResult(ShowResult result)
    //{
    //    if (result == ShowResult.Finished)
    //    {

    //    }
    //    else if (result == ShowResult.Skipped)
    //    {

    //    }
    //    else if (result == ShowResult.Failed)
    //    {
    //    }
    //}
}
