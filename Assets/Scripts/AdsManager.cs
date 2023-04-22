using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    //Initialization 
    [SerializeField] string androidGameId;
    [SerializeField] string iOSGameId;
    [SerializeField] bool testMode = true;
    private string gameId;

    //Interstitial Ads
    [SerializeField] string androidAd = "Interstitial_Android";
    [SerializeField] string iOsAd = "Interstitial_iOS";
    string interstitialAds;

    private void Awake()
    {
        InitializeAds();
        // Get the Ad Unit ID for the current platform:
        interstitialAds = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? iOsAd
            : androidAd;
    }

    public void InitializeAds()
    {
#if UNITY_IOS
        _gameId = _iOSGameId;
#elif UNITY_ANDROID
        gameId = androidGameId;
#elif UNITY_EDITOR
        _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, testMode, this);
        }
    }

    //Plays Interstitial ad
    public void PlayAd()
    {
        if (Advertisement.isInitialized)
        {
            Advertisement.Load(interstitialAds, this);
        }
    }

    //Initialization Listeners
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");
    }

    //Load listeners
    public void OnUnityAdsAdLoaded(string ad)
    {
        Advertisement.Show(ad, this);
    }
    public void OnUnityAdsFailedToLoad(string ad, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {ad} - {error} - {message}");
    }

    //Show Failure
    public void OnUnityAdsShowFailure(string ad, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {ad}: {error} - {message}");
    }

    public void OnUnityAdsShowStart(string ad)
    { 
    
    }
    public void OnUnityAdsShowClick(string ad)
    {
    
    }
    public void OnUnityAdsShowComplete(string ad, UnityAdsShowCompletionState showCompletionState)
    {
    
    }
}
