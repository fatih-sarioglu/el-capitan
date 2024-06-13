using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using System.Collections;

public class InterstitialAdManager : MonoBehaviour
{
    private InterstitialAd interstitialAd;

    private bool wasAdShown = false;

    void Start()
    {
        if (PlayerPrefs.GetInt("interstitialAdCount") == 3)
        {
            PlayerPrefs.SetInt("interstitialAdCount", 2);
        }
        else if (PlayerPrefs.GetInt("interstitialAdCount") == 2)
        {
            PlayerPrefs.SetInt("interstitialAdCount", 1);
        }
        else if (PlayerPrefs.GetInt("interstitialAdCount") == 0)
        {
            PlayerPrefs.SetInt("interstitialAdCount", 3);
        }

        wasAdShown = false;
        MobileAds.Initialize(initStatus => { });
        this.RequestInterstitial();
    }

    void Update()
    {
        Debug.Log(PlayerPrefs.GetInt("interstitialAdCount"));
        if (CharacterControllerScript.isGameOver && PlayerPrefs.GetInt("interstitialAdCount") == 1)
        {
            if (this.interstitialAd.IsLoaded())
            {
                if (!wasAdShown)
                {
                    StartCoroutine(WaitBeforeAd());
                    PlayerPrefs.SetInt("interstitialAdCount", 3);
                    wasAdShown = true;
                }
            }
        }
    }

    IEnumerator WaitBeforeAd()
    {
        yield return new WaitForSeconds(0.5f);
        this.interstitialAd.Show();

    }

    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-2481223380109873/2942138080";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
        #else
        string adUnitId = "unexpected_platform";
        #endif

        // Initialize an InterstitialAd.
        this.interstitialAd = new InterstitialAd(adUnitId);
        
        // Called when an ad request has successfully loaded.
        this.interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitialAd.OnAdOpening += HandleOnAdOpening;
        // Called when the ad is closed.
        this.interstitialAd.OnAdClosed += HandleOnAdClosed;
        
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitialAd.LoadAd(request);
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.ToString());
    }

    public void HandleOnAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpening event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }
}
