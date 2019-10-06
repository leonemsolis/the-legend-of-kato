using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class BannerAdController : MonoBehaviour
{
    private const string banner_placement_id = "bannedAd";
    private const string rewarderd_video_placement_id = "rewardedVideo";

    void Start()
    {
        StartCoroutine(ShowRewardedAd());
    }

    IEnumerator ShowRewardedAd()
    {
        while (!Monetization.IsReady(rewarderd_video_placement_id))
        {
            yield return null;
        }

        ShowAd();
    }

    void ShowAd()
    {
        ShowAdCallbacks options = new ShowAdCallbacks();
        options.finishCallback = HandleShowResult;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent(rewarderd_video_placement_id) as ShowAdPlacementContent;
        ad.Show(options);
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == UnityEngine.Monetization.ShowResult.Finished)
        {
            FindObjectOfType<ScoreBoardText>().IncreaseScore(40);
        }
        else if (result == ShowResult.Skipped)
        {
            Debug.LogWarning("The player skipped the video - DO NOT REWARD!");
            FindObjectOfType<ScoreBoardText>().IncreaseScore(20);
        }
        else if (result == ShowResult.Failed)
        {
            FindObjectOfType<ScoreBoardText>().IncreaseScore(30);
        }
    }
}
