using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;
using UnityEngine.Advertisements;

public class BannerAdController : MonoBehaviour
{

    readonly RuntimePlatform platform = Application.platform;

    private string gameID;

    private const string banner_placement_id = "bannedAd";
    private const string video_placement_id = "rewardedVideo";

    private const bool test = true;

    //void Start()
    //{
    //    store_id = (platform == RuntimePlatform.Android) ? "3315936" : "3315937";

    //    //Monetization.Initialize(store_id, test);
    //    Advertisement.Initialize(store_id, test);
    //}

    //void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.V))
    //    {
    //        if(Monetization.IsReady(video_placement_id))
    //        {
    //            ShowAdPlacementContent ad = null;
    //            ad = Monetization.GetPlacementContent(video_placement_id) as ShowAdPlacementContent;

    //            if(ad != null)
    //            {
    //                ad.Show();
    //            }
    //        }
    //    }

    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        if (Advertisement.IsReady(banner_placement_id))
    //        {
    //            Advertisement.Banner.Show(banner_placement_id);
    //        }
    //    }
    //}

    void Start()
    {
        gameID = (platform == RuntimePlatform.Android) ? "3315936" : "3315937";
        Advertisement.Initialize(gameID, test);
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(banner_placement_id))
        {
            Debug.Log(Advertisement.IsReady(banner_placement_id));
            yield return new WaitForSeconds(0.1f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show(banner_placement_id);
    }
}
