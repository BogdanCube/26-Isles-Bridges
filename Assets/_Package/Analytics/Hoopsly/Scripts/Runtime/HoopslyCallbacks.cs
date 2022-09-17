using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hoopsly.Internal;
using Hoopsly.Internal.Applovin;

public class HoopslyCallbacks : MonoBehaviour
{
    private static HoopslyCallbacks m_instance;
    public static HoopslyCallbacks Instance
    {
        get 
        {
            if (m_instance == null) { CreateInstance(); }
            return m_instance; 
        }
    }

    public static event Action OnHoopslyInitialized
    {
        add
        {
            HoopslyLauncher.m_OnHoopslyInitialized += value;
        }
        remove
        {
            HoopslyLauncher.m_OnHoopslyInitialized -= value;
        }
    }

    public static event Action<MaxSdkBase.SdkConfiguration> OnApplovinInitialized
    {
        add
        {
            ApplovinInitializer.m_OnApplovinInitilized += value;
        }
        remove
        {
            ApplovinInitializer.m_OnApplovinInitilized -= value;
        }
    }

    public static event Action<string, int> OnRemoteConfigRecived
    {
        add
        {
            HoopslyLauncher.m_OnRemoteConfigRecived += value;
        }
        remove
        {
            HoopslyLauncher.m_OnRemoteConfigRecived -= value;
        }
    }

    //OnAppMoveToBackgroun
    //OnAppOutOfBacground

    public class Interstitial
    {
        public static event Action<string, MaxSdkBase.AdInfo> OnAdClickedEvent
        {
            add 
            {
                ApplovinInitializer.m_OnInterstitialClickedEvent += value; 
            }
            remove { ApplovinInitializer.m_OnInterstitialClickedEvent -= value; }
        }

        public static event Action<string, MaxSdkBase.AdInfo> OnAdDisplayedEvent
        {
            add { ApplovinInitializer.m_OnInterstitialDisplayedEvent += value; }
            remove { ApplovinInitializer.m_OnInterstitialDisplayedEvent -= value; }
        }

        public static event Action<string, MaxSdkBase.ErrorInfo, MaxSdkBase.AdInfo> OnAdDisplayFailedEvent
        {
            add { ApplovinInitializer.m_OnInterstitialFailedToDisplayEvent += value; }
            remove { ApplovinInitializer.m_OnInterstitialFailedToDisplayEvent -= value; }
        }

        public static event Action<string, MaxSdkBase.AdInfo> OnAdHiddenEvent
        {
            add { ApplovinInitializer.m_OnInterstitialHiddenEvent += value; }
            remove { ApplovinInitializer.m_OnInterstitialHiddenEvent += value; }
        }

        public static event Action<string, MaxSdkBase.AdInfo> OnAdLoadedEvent
        {
            add { ApplovinInitializer.m_OnInterstitialLoadedEvent += value; }
            remove { ApplovinInitializer.m_OnInterstitialLoadedEvent -= value; }
        }
                
        public static event Action<string, MaxSdkBase.ErrorInfo> OnAdLoadFailedEvent
        {
            add { ApplovinInitializer.m_OnInterstitialLoadedFailedEvent += value; }
            remove { ApplovinInitializer.m_OnInterstitialLoadedFailedEvent -= value; }
        }
    }

    public class Rewarded
    {
        public static event Action<string, MaxSdkBase.AdInfo> OnAdClickedEvent
        {
            add { ApplovinInitializer.m_OnRewardedAdClickedEvent += value; }
            remove { ApplovinInitializer.m_OnRewardedAdClickedEvent -= value; }
        }
        public static event Action<string, MaxSdkBase.AdInfo> OnAdDisplayedEvent
        {
            add { ApplovinInitializer.m_OnRewardedAdDisplayedEvent += value; }
            remove { ApplovinInitializer.m_OnRewardedAdDisplayedEvent -= value; }
        }
        public static event Action<string, MaxSdkBase.ErrorInfo, MaxSdkBase.AdInfo> OnAdDisplayFailedEvent
        {
            add { ApplovinInitializer.m_OnRewardedAdFailedToDisplayEvent += value; }
            remove { ApplovinInitializer.m_OnRewardedAdFailedToDisplayEvent -= value; }
        }
        public static event Action<string, MaxSdkBase.AdInfo> OnAdHiddenEvent
        {
            add { ApplovinInitializer.m_OnRewardedAdClosedEvent += value; }
            remove { ApplovinInitializer.m_OnRewardedAdClosedEvent -= value; }
        }
        public static event Action<string, MaxSdkBase.AdInfo> OnAdLoadedEvent
        {
            add { ApplovinInitializer.m_OnRewardedAdLoadedEvent += value; }
            remove { ApplovinInitializer.m_OnRewardedAdLoadedEvent -= value; }
        }
        public static event Action<string, MaxSdkBase.ErrorInfo> OnAdLoadFailedEvent
        {
            add { ApplovinInitializer.m_OnRewardedAdLoadFailedEvent += value; }
            remove { ApplovinInitializer.m_OnRewardedAdLoadFailedEvent -= value; }
        }
        public static event Action<string, MaxSdkBase.Reward, MaxSdkBase.AdInfo, AdRewardType> OnAdReceivedRewardEvent
        {
            add { ApplovinInitializer.m_OnRewardedAdReceivedRewardEvent += value; }
            remove { ApplovinInitializer.m_OnRewardedAdReceivedRewardEvent -= value; }
        }
    }

    public class Banner
    {
        public static event Action<string, MaxSdkBase.AdInfo> OnAdClickedEvent
        {
            add { ApplovinInitializer.m_OnBannerClickedEvent += value; }
            remove { ApplovinInitializer.m_OnBannerClickedEvent -= value; }
        }
        public static event Action<string, MaxSdkBase.AdInfo> OnAdCollapsedEvent
        {
            add { ApplovinInitializer.m_OnBannerCollapsed += value; }
            remove { ApplovinInitializer.m_OnBannerCollapsed -= value; }
        }
        public static event Action<string, MaxSdkBase.AdInfo> OnAdExpandedEvent
        {
            add { ApplovinInitializer.m_OnBannerExpanded += value; }
            remove { ApplovinInitializer.m_OnBannerExpanded -= value; }
        }
        public static event Action<string, MaxSdkBase.AdInfo> OnAdLoadedEvent
        {
            add { ApplovinInitializer.m_OnBannerLoaded += value; }
            remove { ApplovinInitializer.m_OnBannerLoaded -= value; }
        }
        public static event Action<string, MaxSdkBase.ErrorInfo> OnAdLoadFailedEvent
        {
            add { ApplovinInitializer.m_OnBannerLoadFailed += value; }
            remove { ApplovinInitializer.m_OnBannerLoadFailed -= value; }
        }
    }

    public class MREC
    {
        public static event Action<string, MaxSdkBase.AdInfo> OnAdClickedEvent
        {
            add { ApplovinInitializer.m_OnMREcClickedEvent += value; }
            remove { ApplovinInitializer.m_OnMREcClickedEvent -= value; }
        }
        public static event Action<string, MaxSdkBase.AdInfo> OnAdCollapsedEvent
        {
            add { ApplovinInitializer.m_OnMREcCollapsed += value; }
            remove { ApplovinInitializer.m_OnMREcCollapsed -= value; }
        }
        public static event Action<string, MaxSdkBase.AdInfo> OnAdExpandedEvent
        {
            add { ApplovinInitializer.m_OnMREcExpanded += value; }
            remove { ApplovinInitializer.m_OnMREcExpanded -= value; }
        }
        public static event Action<string, MaxSdkBase.AdInfo> OnAdLoadedEvent
        {
            add { ApplovinInitializer.m_OnMREcLoaded += value; }
            remove { ApplovinInitializer.m_OnMREcLoaded -= value; }
        }
        public static event Action<string, MaxSdkBase.ErrorInfo> OnAdLoadFailedEvent
        {
            add { ApplovinInitializer.m_OnMREcLoadFailed += value; }
            remove { ApplovinInitializer.m_OnMREcLoadFailed -= value; }
        }
    }


    private void OnDestroy()
    {
        Destroy(this.gameObject);
    }

    public static void CreateInstance()
    {
        if (m_instance != null) { return; }
        m_instance = new GameObject("HoopslyCallbacks").AddComponent<HoopslyCallbacks>();
        DontDestroyOnLoad(m_instance.gameObject);
    }
}

