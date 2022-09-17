using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hoopsly;
using Hoopsly.Settings;
using System.Threading.Tasks;

namespace Hoopsly.Internal.Adjust
{
    using com.adjust.sdk;
    public class AdjustInitializer : IInitilizable
    {
        public AdjustInitializer()
        {
            HoopslyCallbacks.OnApplovinInitialized += SubscribeOnApplovinEvents;

        }

        private string m_uuid;
        public async Task Initilize(string uuid)
        {

            m_uuid = uuid;
            try
            {
                await InitAdjust(uuid);
            }
            catch (System.Exception ex)
            {
                HoopslyLogger.LogMessage("======[Adjust initialization FAILED!]======", HoopslyLogLevel.Suppress, H_LogType.Error);
                HoopslyLogger.LogMessage(ex.StackTrace, HoopslyLogLevel.Suppress, H_LogType.Error);
                HoopslyLogger.LogMessage("===========================================", HoopslyLogLevel.Suppress, H_LogType.Error);

            }
        }

        private async Task InitAdjust(string uuid)
        {
            HoopslyLogger.LogMessage("==========[ADJUST_INIT]==========", HoopslyLogLevel.Suppress);
            Adjust.addSessionCallbackParameter("user_id", uuid);
            AdjustConfig adjustConfig = new AdjustConfig(HoopslySettings.Instance.AdjustSettings.AdjustAppToken, HoopslySettings.Instance.AdjustSettings.AdjustEnviroment, (HoopslySettings.Instance.AdjustSettings.AdjustLogLevel == AdjustLogLevel.Suppress));
            adjustConfig.setLogLevel(HoopslySettings.Instance.AdjustSettings.AdjustLogLevel);
            adjustConfig.setSendInBackground(HoopslySettings.Instance.AdjustSettings.AdjustSendInBackground);
            adjustConfig.setEventBufferingEnabled(HoopslySettings.Instance.AdjustSettings.AdjustEventBufferingEnabled);
            adjustConfig.setLaunchDeferredDeeplink(HoopslySettings.Instance.AdjustSettings.AdjustLaunchDeferredDeeplink);
            Adjust.start(adjustConfig);
            await Task.Yield();
        }

        private void SubscribeOnApplovinEvents(MaxSdkBase.SdkConfiguration sdkConfiguration)
        {
            MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnRevenuePaidEvent;
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRevenuePaidEvent;
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnRevenuePaidEvent;
            MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent += OnRevenuePaidEvent;

            HoopslyCallbacks.OnApplovinInitialized -= SubscribeOnApplovinEvents;
        }

        private void OnRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            HoopslyLogger.LogMessage($"===[On_interstitial_revenue_paid! Placement:{adInfo.Placement} Amount:{adInfo.Revenue}]===", HoopslyLogLevel.Verbose);
            var adRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAppLovinMAX);
            adRevenue.setRevenue(adInfo.Revenue, "USD");
            adRevenue.setAdRevenueNetwork(adInfo.NetworkName);
            adRevenue.setAdRevenueUnit(adInfo.AdUnitIdentifier);
            adRevenue.setAdRevenuePlacement(adInfo.Placement);
            adRevenue.addCallbackParameter("user_id", m_uuid);
            Adjust.trackAdRevenue(adRevenue);
        }
    }
}
