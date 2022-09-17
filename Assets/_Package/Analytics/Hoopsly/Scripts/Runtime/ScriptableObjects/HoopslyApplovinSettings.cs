using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hoopsly.Settings
{
    public class HoopslyApplovinSettings : ScriptableObject
    {
        #region private fields
        //
        //---------Base-settings---------
        //
        [SerializeField] private string m_maxSdkKey = "_wOZWz-JINT4NAeLckviPsx8oVbzyzoTi2Ph4g0yTwoK8qG7PLd4a0yV9iP9QKkXg93dMU34ohb3b3EaJcmb3x";
        [SerializeField] private bool m_enableVerboseLogging = false;
        [SerializeField] private bool m_showMediationDebuggerOnStart = false;
        //
        //---------Interstitial-settings---------
        //
        [SerializeField] private bool m_useInterstitialAd;
        //private string m_interstitialAdUnitId = "";
        [SerializeField] private string m_interstitialAdUnitId_IOS = "";
        [SerializeField] private string m_interstitialAdUnitId_ANDROID = "";
        //
        //---------Rewarded-Ad-settings---------
        //
        [SerializeField] private bool m_useRewardedAd;
        //private string m_rewardedAdUnitId = "";
        [SerializeField] private string m_rewardedAdUnitId_IOS = "";
        [SerializeField] private string m_rewardedAdUnitId_ANDROID = "";
        //
        //---------MREC-Ad-settings---------
        //
        [SerializeField] private bool m_useMRECAd;
        //private string m_MRECAdUnitId = "";
        [SerializeField] private string m_MRECAdUnitId_IOS = "";
        [SerializeField] private string m_MRECAdUnitId_ANDROID = "";
        [SerializeField] private MaxSdkBase.AdViewPosition m_mrecPosition = MaxSdkBase.AdViewPosition.Centered;
        //
        //---------Banner-Ad-settings---------
        //
        [SerializeField] private bool m_useBannerAd;
        //private string m_bannerAdUnitId = "";
        [SerializeField] private string m_bannerAdUnitId_IOS = "";
        [SerializeField] private string m_bannerAdUnitId_ANDROID = "";
        [SerializeField] private bool m_useAdaptiveBanner = false;
        [SerializeField] private MaxSdkBase.BannerPosition m_bannerPosition = MaxSdkBase.BannerPosition.BottomCenter;
        [SerializeField] private Color m_bannerBackgroundColor = Color.black;
        #endregion

        #region public_properties

        public string MaxSdkKey
        {
            get { return m_maxSdkKey; }
            set { m_maxSdkKey = value; }
        }

        public bool EnableVerboseLogging
        {
            get { return m_enableVerboseLogging; }
            set { m_enableVerboseLogging = value; }
        }

        public bool ShowMediationDebuggerOnLoad
        {
            get { return m_showMediationDebuggerOnStart; }
            set { m_showMediationDebuggerOnStart = value; }
        }

        public bool UseInterstitialAd
        {
            get { return m_useInterstitialAd; }
            set { m_useInterstitialAd = value; }
        }

        public bool UseRewardedAd
        {
            get { return m_useRewardedAd; }
            set { m_useRewardedAd = value; }
        }

        public bool UseMRECAd
        {
            get { return m_useMRECAd; }
            set { m_useMRECAd = value; }
        }

        public bool UseBannerAd
        {
            get { return m_useBannerAd; }
            set { m_useBannerAd = value; }
        }

        public bool UseAdaptiveBanner
        {
            get { return m_useAdaptiveBanner; }
            set { m_useAdaptiveBanner = value; }
        }

        public string InterstitialAdUnitID
        {
            get 
            { 
                return Application.platform == RuntimePlatform.IPhonePlayer ? m_interstitialAdUnitId_IOS : m_interstitialAdUnitId_ANDROID; 
            }
        }

        public string InterstitialAdUnitID_IOS
        {
            get { return m_interstitialAdUnitId_IOS; }
            set { m_interstitialAdUnitId_IOS = value; }
        }

        public string InterstitialAdUnitID_ANDROID
        {
            get { return m_interstitialAdUnitId_ANDROID; }
            set { m_interstitialAdUnitId_ANDROID = value; }
        }


        public string RewardedAdUnitID
        {
            get 
            { 
                return Application.platform == RuntimePlatform.IPhonePlayer ? m_rewardedAdUnitId_IOS : m_rewardedAdUnitId_ANDROID; 
            }
        }

        public string RewardedAdUnitID_IOS
        {
            get { return m_rewardedAdUnitId_IOS; }
            set { m_rewardedAdUnitId_IOS = value; }
        }

        public string RewardedAdUnitID_ANDROID
        {
            get { return m_rewardedAdUnitId_ANDROID; }
            set { m_rewardedAdUnitId_ANDROID = value; }
        }

        public string MRECAdUnitID
        {
            get 
            { 
                return Application.platform == RuntimePlatform.IPhonePlayer ? m_MRECAdUnitId_IOS : m_MRECAdUnitId_ANDROID; 
            }
        }

        public string MRECAdUnitID_IOS
        {
            get { return m_MRECAdUnitId_IOS; }
            set { m_MRECAdUnitId_IOS = value; }
        }

        public string MRECAdUnitID_ANDROID
        {
            get { return m_MRECAdUnitId_ANDROID; }
            set { m_MRECAdUnitId_ANDROID = value; }
        }

        public string BannerAdUnitID
        {
            get 
            { 
                return Application.platform == RuntimePlatform.IPhonePlayer ? m_bannerAdUnitId_IOS : m_bannerAdUnitId_ANDROID; 
            }
        }

        public string BannerAdUnitID_IOS
        {
            get { return m_bannerAdUnitId_IOS; }
            set { m_bannerAdUnitId_IOS = value; }
        }

        public string BannerAdUnitID_ANDROID
        {
            get { return m_bannerAdUnitId_ANDROID; }
            set { m_bannerAdUnitId_ANDROID = value; }
        }

        public MaxSdk.BannerPosition BannerPosition
        {
            get { return this.m_bannerPosition; }
            set { this.m_bannerPosition = value; }
        }

        public MaxSdkBase.AdViewPosition MrecPosition
        {
            get { return this.m_mrecPosition; }
            set { this.m_mrecPosition = value; }
        }

        public Color BannerBackgroundColor
        {
            get { return m_bannerBackgroundColor; }
            set { m_bannerBackgroundColor = value; }
        }
        #endregion
    }
}
