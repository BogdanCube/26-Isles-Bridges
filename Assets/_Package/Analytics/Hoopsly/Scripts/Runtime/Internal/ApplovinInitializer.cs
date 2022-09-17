using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hoopsly.Settings;
using System.Threading.Tasks;
using System;


namespace Hoopsly.Internal.Applovin
{
    public class ApplovinInitializer : IInitilizable<MaxSdkBase.SdkConfiguration>
    {
        public async Task<MaxSdkBase.SdkConfiguration> Initilize(string uuid)
        {
            try
            {
                return  await InitApplovinAndroid(uuid);
            }
            catch (System.Exception ex)
            {
                HoopslyLogger.LogMessage(ex.StackTrace, HoopslyLogLevel.Suppress, H_LogType.Error);
                return null;
            }
        }

        private async Task<MaxSdkBase.SdkConfiguration> InitApplovinAndroid(string uuid)
        {
            bool initStatus = false;
            HoopslyLogger.LogMessage("==========[APPLOVIN_INIT]==========", HoopslyLogLevel.Suppress);
            if (HoopslySettings.Instance.ApplovinSettings.MaxSdkKey == "")
            {
                HoopslyLogger.LogMessage("==========[Applovin MAX sdk key was not set! Initialization STOPPED!]==========", HoopslyLogLevel.Suppress, H_LogType.Error);
                return null;
            }
            MaxSdkBase.SdkConfiguration configuration = null;
            MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
            {
                configuration = sdkConfiguration;
                if (HoopslySettings.Instance.ApplovinSettings.UseInterstitialAd)
                {
                    InitializeInterstitialAds();
                }
                if (HoopslySettings.Instance.ApplovinSettings.UseRewardedAd)
                {
                    InitializeRewardedAds();
                }
                if (HoopslySettings.Instance.ApplovinSettings.UseBannerAd)
                {
                    InitializeBannerAds();
                }
                if (HoopslySettings.Instance.ApplovinSettings.UseMRECAd)
                {
                    InitializeMrecAds();
                }

                if (HoopslySettings.Instance.ApplovinSettings.ShowMediationDebuggerOnLoad)
                    MaxSdk.ShowMediationDebugger();

                initStatus = true;
                m_OnApplovinInitilized.Invoke(sdkConfiguration);
            };
            MaxSdk.SetSdkKey(HoopslySettings.Instance.ApplovinSettings.MaxSdkKey);
            MaxSdk.SetUserId(uuid);
            MaxSdk.InitializeSdk();
            HoopslyLogger.LogMessage("==========[APPLOVIN_INIT_COMPLETE]==========", HoopslyLogLevel.Suppress);
            while(initStatus==false)
            {
                await Task.Yield();
            }
            return configuration;
        }

        public static event Action<MaxSdkBase.SdkConfiguration> m_OnApplovinInitilized = delegate { };

        #region Interstitial

        public static event Action<string, MaxSdkBase.AdInfo> m_OnInterstitialLoadedEvent = delegate { };
        public static event Action<string, MaxSdkBase.ErrorInfo> m_OnInterstitialLoadedFailedEvent = delegate { };
        public static event Action<string, MaxSdkBase.ErrorInfo, MaxSdkBase.AdInfo> m_OnInterstitialFailedToDisplayEvent = delegate { };
        public static event Action<string, MaxSdkBase.AdInfo> m_OnInterstitialDisplayedEvent = delegate { };
        public static event Action<string, MaxSdkBase.AdInfo> m_OnInterstitialClickedEvent = delegate { };
        public static event Action<string, MaxSdkBase.AdInfo> m_OnInterstitialHiddenEvent = delegate { };

        private int interstitialRetryAttempt;

        private void InitializeInterstitialAds()
        {
            if (String.IsNullOrEmpty(HoopslySettings.Instance.ApplovinSettings.InterstitialAdUnitID))
            {
                HoopslyLogger.LogMessage("===[Interstitial Ad Unit is EMPTY! Interstitial Initialization Skipped!]===", HoopslyLogLevel.Suppress, H_LogType.Error);
                return;
            }
            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
            MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
            LoadInterstitial();
        }

        void LoadInterstitial()
        {
            MaxSdk.LoadInterstitial(HoopslySettings.Instance.ApplovinSettings.InterstitialAdUnitID);
        }

        public bool IsInterstitialReady()
        {
            return MaxSdk.IsInterstitialReady(HoopslySettings.Instance.ApplovinSettings.InterstitialAdUnitID);
        }

        public void ShowInterstitial()
        {
            if (MaxSdk.IsInterstitialReady(HoopslySettings.Instance.ApplovinSettings.InterstitialAdUnitID))
            {
                MaxSdk.ShowInterstitial(HoopslySettings.Instance.ApplovinSettings.InterstitialAdUnitID);
            }
            else
            {
                HoopslyLogger.LogMessage("Ad was not redy!", HoopslyLogLevel.Debug, H_LogType.Warning);
            }
        }

        private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            HoopslyLogger.LogMessage("Interstitial loaded", HoopslyLogLevel.Verbose);
            m_OnInterstitialLoadedEvent(adUnitId, adInfo);
            interstitialRetryAttempt = 0;
        }

        private async void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            interstitialRetryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, interstitialRetryAttempt));
            HoopslyLogger.LogMessage("Interstitial failed to load with error code: " + errorInfo, HoopslyLogLevel.Debug, H_LogType.Error);
            m_OnInterstitialLoadedFailedEvent(adUnitId, errorInfo);
            HoopslyLogger.LogMessage($"Await for {(int)retryDelay * 1000} miliseconds", HoopslyLogLevel.Verbose, H_LogType.Warning);
            await Task.Delay((int)retryDelay * 1000);
            LoadInterstitial();
        }

        private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            HoopslyLogger.LogMessage("Interstitial failed to display with error code: " + errorInfo, HoopslyLogLevel.Debug, H_LogType.Error);
            m_OnInterstitialFailedToDisplayEvent(adUnitId, errorInfo, adInfo);
            LoadInterstitial();
        }
        private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            HoopslyLogger.LogMessage("Interstitial ad was displayed", HoopslyLogLevel.Verbose);
            m_OnInterstitialDisplayedEvent(adUnitId, adInfo);
        }

        private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            HoopslyLogger.LogMessage("Interstitial ad was clicked", HoopslyLogLevel.Verbose);
            m_OnInterstitialClickedEvent(adUnitId, adInfo);
        }

        private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            HoopslyLogger.LogMessage("Interstitial dismissed", HoopslyLogLevel.Verbose);
            m_OnInterstitialHiddenEvent(adUnitId, adInfo);
            LoadInterstitial();
        }

        #endregion

        #region Rewarded

        public static event Action<string, MaxSdkBase.AdInfo> m_OnRewardedAdLoadedEvent = delegate { };
        public static event Action<string, MaxSdkBase.ErrorInfo> m_OnRewardedAdLoadFailedEvent = delegate { };
        public static event Action<string, MaxSdkBase.ErrorInfo, MaxSdkBase.AdInfo> m_OnRewardedAdFailedToDisplayEvent = delegate { };
        public static event Action<string, MaxSdkBase.AdInfo> m_OnRewardedAdDisplayedEvent = delegate { };
        public static event Action<string, MaxSdkBase.AdInfo> m_OnRewardedAdClickedEvent = delegate { };
        public static event Action<string, MaxSdkBase.AdInfo> m_OnRewardedAdClosedEvent = delegate { };
        public static event Action<string, MaxSdk.Reward, MaxSdkBase.AdInfo, AdRewardType> m_OnRewardedAdReceivedRewardEvent = delegate { };

        private int rewardedRetryAttempt;

        private void InitializeRewardedAds()
        {
            if (String.IsNullOrEmpty(HoopslySettings.Instance.ApplovinSettings.RewardedAdUnitID))
            {
                HoopslyLogger.LogMessage("===[Rewarded Ad Unit is EMPTY! Reward Initialization Skipped!]===", HoopslyLogLevel.Suppress, H_LogType.Error);
                return;
            }

            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
            MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdClosedEvent;
            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

            LoadRewardedAd();
        }

        private void LoadRewardedAd()
        {
            MaxSdk.LoadRewardedAd(HoopslySettings.Instance.ApplovinSettings.RewardedAdUnitID);
        }

        private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            HoopslyLogger.LogMessage("Rewarded ad loaded", HoopslyLogLevel.Verbose);
            m_OnRewardedAdLoadedEvent(adUnitId, adInfo);
            rewardedRetryAttempt = 0;
        }

        private async void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            rewardedRetryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, rewardedRetryAttempt));
            HoopslyLogger.LogMessage("Rewarded ad failed to load with error code: " + errorInfo, HoopslyLogLevel.Debug, H_LogType.Error);
            m_OnRewardedAdLoadFailedEvent(adUnitId, errorInfo);
            HoopslyLogger.LogMessage($"Await for {(int)retryDelay * 1000} miliseconds", HoopslyLogLevel.Debug, H_LogType.Warning);
            await Task.Delay((int)retryDelay * 1000);
            LoadInterstitial();
        }

        private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            HoopslyLogger.LogMessage("Rewarded ad failed to display with error code: " + errorInfo, HoopslyLogLevel.Debug, H_LogType.Error);
            m_OnRewardedAdFailedToDisplayEvent(adUnitId, errorInfo, adInfo);
            LoadRewardedAd();
        }

        private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            HoopslyLogger.LogMessage("Rewarded ad displayed", HoopslyLogLevel.Verbose);
            m_OnRewardedAdDisplayedEvent(adUnitId, adInfo);
        }

        private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            HoopslyLogger.LogMessage("Rewarded ad clicked", HoopslyLogLevel.Verbose);
            m_OnRewardedAdClickedEvent(adUnitId, adInfo);
        }

        private void OnRewardedAdClosedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            HoopslyLogger.LogMessage("Rewarded ad was closed. Load next reward ad", HoopslyLogLevel.Verbose);
            m_OnRewardedAdClosedEvent(adUnitId, adInfo);
            LoadRewardedAd();
        }

        private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
        {
            HoopslyLogger.LogMessage("Rewarded ad received reward", HoopslyLogLevel.Verbose);
            m_OnRewardedAdReceivedRewardEvent(adUnitId, reward, adInfo, HoopslyIntegration.CurrentAdRewardType);
        }

        #endregion

        #region MREC

        public static event Action<string, MaxSdkBase.AdInfo> m_OnMREcClickedEvent = delegate { };
        public static event Action<string, MaxSdkBase.AdInfo> m_OnMREcCollapsed = delegate { };
        public static event Action<string, MaxSdkBase.AdInfo> m_OnMREcExpanded = delegate { };
        public static event Action<string, MaxSdkBase.AdInfo> m_OnMREcLoaded = delegate { };
        public static event Action<string, MaxSdkBase.ErrorInfo> m_OnMREcLoadFailed = delegate { };
        private void InitializeMrecAds()
        {
            if (String.IsNullOrEmpty(HoopslySettings.Instance.ApplovinSettings.MRECAdUnitID))
            {
                HoopslyLogger.LogMessage("===[MREC Ad Unit is EMPTY! MREC Initialization Skipped!]===", HoopslyLogLevel.Suppress, H_LogType.Error);
                return;
            }

            MaxSdkCallbacks.MRec.OnAdClickedEvent += OnMREcClicked;
            MaxSdkCallbacks.MRec.OnAdCollapsedEvent += OnMREcCollapsed;
            MaxSdkCallbacks.MRec.OnAdExpandedEvent += OnMREcExpanded;
            MaxSdkCallbacks.MRec.OnAdLoadedEvent += OnMREcLoaded;
            MaxSdkCallbacks.MRec.OnAdLoadFailedEvent += OnMREcLoadFailed;

            MaxSdk.CreateMRec(HoopslySettings.Instance.ApplovinSettings.MRECAdUnitID, HoopslySettings.Instance.ApplovinSettings.MrecPosition);
        }

        private void OnMREcClicked(string adUnitID, MaxSdkBase.AdInfo adInfo)
        {
            m_OnMREcClickedEvent.Invoke(adUnitID, adInfo);
        }

        private void OnMREcCollapsed(string adUnitID, MaxSdkBase.AdInfo adInfo)
        {
            m_OnMREcCollapsed.Invoke(adUnitID, adInfo);
        }

        private void OnMREcExpanded(string adUnitID, MaxSdkBase.AdInfo adInfo)
        {
            m_OnMREcExpanded.Invoke(adUnitID, adInfo);
        }

        private void OnMREcLoaded(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            m_OnMREcLoaded.Invoke(adUnitId, adInfo);
        }

        private void OnMREcLoadFailed(string AdUnitID, MaxSdkBase.ErrorInfo errorInfo)
        {
            m_OnMREcLoadFailed.Invoke(AdUnitID, errorInfo);
        }
        #endregion

        #region Banner
        public static event Action<string, MaxSdkBase.AdInfo> m_OnBannerClickedEvent = delegate { };
        public static event Action<string, MaxSdkBase.AdInfo> m_OnBannerCollapsed = delegate { };
        public static event Action<string, MaxSdkBase.AdInfo> m_OnBannerExpanded = delegate { };
        public static event Action<string, MaxSdkBase.AdInfo> m_OnBannerLoaded = delegate { };
        public static event Action<string, MaxSdkBase.ErrorInfo> m_OnBannerLoadFailed = delegate { };
        private void InitializeBannerAds()
        {
            if (String.IsNullOrEmpty(HoopslySettings.Instance.ApplovinSettings.BannerAdUnitID))
            {
                HoopslyLogger.LogMessage("===[BANNER Ad Unit is EMPTY! BANNER Initialization Skipped!]===", HoopslyLogLevel.Suppress, H_LogType.Error);
                return;
            }

            MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerClicked;
            MaxSdkCallbacks.Banner.OnAdCollapsedEvent += OnBannerCollapsed;
            MaxSdkCallbacks.Banner.OnAdExpandedEvent += OnBannerExpanded;
            MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerLoaded;
            MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerLoadFailed;

            MaxSdk.CreateBanner(HoopslySettings.Instance.ApplovinSettings.BannerAdUnitID, HoopslySettings.Instance.ApplovinSettings.BannerPosition);
            MaxSdk.SetBannerExtraParameter(HoopslySettings.Instance.ApplovinSettings.BannerAdUnitID, "adaptive_banner", HoopslySettings.Instance.ApplovinSettings.UseAdaptiveBanner ? "true" : "false");
            MaxSdk.SetBannerBackgroundColor(HoopslySettings.Instance.ApplovinSettings.BannerAdUnitID, HoopslySettings.Instance.ApplovinSettings.BannerBackgroundColor);
        }

        private void OnBannerClicked(string adUnitID, MaxSdkBase.AdInfo adInfo)
        {
            m_OnBannerClickedEvent.Invoke(adUnitID, adInfo);
        }

        private void OnBannerCollapsed(string adUnitID, MaxSdkBase.AdInfo adInfo)
        {
            m_OnBannerCollapsed.Invoke(adUnitID, adInfo);
        }

        private void OnBannerExpanded(string adUnitID, MaxSdkBase.AdInfo adInfo)
        {
            m_OnBannerExpanded.Invoke(adUnitID, adInfo);
        }

        private void OnBannerLoaded(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            m_OnBannerLoaded.Invoke(adUnitId, adInfo);
        }

        private void OnBannerLoadFailed(string AdUnitID, MaxSdkBase.ErrorInfo errorInfo)
        {
            m_OnBannerLoadFailed.Invoke(AdUnitID, errorInfo);
        }
        #endregion
    }
}
