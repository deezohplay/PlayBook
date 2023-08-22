using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using TMPro;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdsManager Instance { set; get; }
    //Initialization 
    [SerializeField] string androidGameId;
    [SerializeField] string iOSGameId;
    [SerializeField] bool testMode = true;
    private string gameId;

    //Interstitial Ads
    [SerializeField] string androidAd = "Interstitial_Android";
    [SerializeField] string iOsAd = "Interstitial_iOS";
    string interstitialAds;

    //Rewarded Ads
    [SerializeField] string androidAdUnitId = "Rewarded_Android";
    [SerializeField] string iOSAdUnitId = "Rewarded_iOS";
    string rewardedAds; 

    //Banner Ads
    [SerializeField] BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;
    [SerializeField] string androidAdUnitIdBanner = "Banner_Android";
    [SerializeField] string iOSAdUnitIdBanner = "Banner_iOS";
    string bannerAds; 

    private void Awake()
    {
        Instance = this;
        InitializeAds();
        // Get the Ad Unit ID for the current platform:
        interstitialAds = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? iOsAd
            : androidAd;
        // Gets the Ad Unit ID for the current platform:
        rewardedAds = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? iOSAdUnitId
            : androidAdUnitId;
        bannerAds = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? iOSAdUnitIdBanner
            : androidAdUnitIdBanner;
    }
    void Start()
    {
        if (Advertisement.isInitialized)
        {
            Advertisement.Load(bannerAds, this);
        }
        Advertisement.Banner.SetPosition(bannerPosition);
        Advertisement.Banner.Show(bannerAds);
    }

    public void InitializeAds()
    {
#if UNITY_IOS
        gameId = _iOSGameId;
#elif UNITY_ANDROID
        gameId = androidGameId;
#elif UNITY_EDITOR
        gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, testMode, this);
        }
    }
    
    // REWARDED AD ! Call this public method when you want to get an ad ready to show.
    public void LoadRewardedAd()
    {
        if (Advertisement.isInitialized)
        {
            Advertisement.Load(rewardedAds, this);
        }
    }

    //Show Rewarded Ads
    public void ShowRewardedAds()
    {
        // Then show the ad:
        Advertisement.Show(rewardedAds, this);
    }

    //Plays Interstitial ad
    public void LoadInterstitialAd()
    {
        if (Advertisement.isInitialized)
        {
            Advertisement.Load(interstitialAds, this);
        }
    }

    //Initialization Listeners
    public void OnInitializationComplete()
    {
      // LoadRewardedAd(); //loads rewared ad
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        
    }

    //Load listeners
    public void OnUnityAdsAdLoaded(string ad)
    {
        if(ad.Equals(interstitialAds))
        {
            Advertisement.Show(ad, this);
        }
     
        if (ad.Equals(rewardedAds))
        {
            ShowRewardedAds();
        }


        
    }
    public void OnUnityAdsFailedToLoad(string ad, UnityAdsLoadError error, string message)
    {
        
    }

    //Show Failure
    public void OnUnityAdsShowFailure(string ad, UnityAdsShowError error, string message)
    {
        
    }

    public void OnUnityAdsShowStart(string ad)
    { 
        
    }
    public void OnUnityAdsShowClick(string ad)
    {
    
    }
    public void OnUnityAdsShowComplete(string ad, UnityAdsShowCompletionState showCompletionState)
    {
        if (ad.Equals(rewardedAds) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
        }
    }
}
