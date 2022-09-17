using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;

using Hoopsly.Settings;
using Hoopsly.Internal;
using Hoopsly.Internal.Time;
using Hoopsly.Internal.Events;

public class HoopslyIntegration : MonoBehaviour
{

    private static HoopslyIntegration m_instance;
    public static HoopslyIntegration Instance
    {
        get { return m_instance; }
    }
    private static bool m_sdkInitializedFinished;
    private static AdRewardType m_currentRewardType = AdRewardType.other;
    public static AdRewardType CurrentAdRewardType
    {
        get { return m_currentRewardType; }
    }

    private static Queue<HoopslyAnalicsEvent> m_firebaseEventQueue = new Queue<HoopslyAnalicsEvent>();

    private static FPS_MeasureTool m_measureTool;
    private static FPS_MeasureTool FPS_MeasureTool
    {
        get 
        {
            if (m_measureTool == null)
                m_measureTool = new FPS_MeasureTool();
            return m_measureTool; 
        }
    }

    #region TIME
    private static event Action m_OnAdOpen = delegate { };
    private static event Action m_OnAdClosed = delegate { };

    private static Timer m_adInterruptionTimer;
    private static Timer AdInterrupterTimer
    {
        get
        {
            if (m_adInterruptionTimer == null)
            {
                m_adInterruptionTimer = new Timer(true, "AdInterupter_Timer");
                m_OnAdOpen += m_adInterruptionTimer.PlayTimer;
                m_OnAdClosed += m_adInterruptionTimer.PauseTimer;
            }
            return m_adInterruptionTimer;
        }
    }

    private static Timer m_sessionTimer;
    private static Timer SessionTimer
    {
        get
        {
            if (m_sessionTimer == null) { m_sessionTimer = new Timer(true, "Session_Timer"); }
            return m_sessionTimer;
        }
    }

    private static Timer m_backGroundTimer;
    private static Timer BackGroundTimer
    {
        get
        {
            if (m_backGroundTimer == null) { m_backGroundTimer = new Timer(true); }
            return m_backGroundTimer;
        }
    }
    private const int timeUntilTimerReset = 300;

    private void UpdateTimersStateOnPause(bool pauseState)
    {
        if (pauseState)
        {
            if (!BackGroundTimer.IsTimerCounting)
            {
                BackGroundTimer.PlayTimer();
                if (SessionTimer.IsTimerCounting)
                {
                    SessionTimer.PauseTimer();
                }

                HoopslyLogger.LogMessage("APP MOVE IN BACKGROUND! STARTING BACKGROUND TIMER!", HoopslyLogLevel.Verbose);
            }
        }
        else
        {
            if (BackGroundTimer.IsTimerCounting)
            {
                int backgroundTime = BackGroundTimer.FinishTimerGetTime();
                if (backgroundTime < timeUntilTimerReset)
                {
                    SessionTimer.PlayTimer();
                    HoopslyLogger.LogMessage("APP MOVE OUT OF BACKGROUND! STOPING BACKGROUND TIMER!", HoopslyLogLevel.Verbose);
                    HoopslyLogger.LogMessage($"Application was {backgroundTime} seconds in background", HoopslyLogLevel.Verbose);
                }
                else
                {
                    AdInterrupterTimer.ResetTimer();
                    BeginSessionTimer();

                    HoopslyLogger.LogMessage($"Application was too long in background mode! {backgroundTime} seconds in background!", HoopslyLogLevel.Verbose);
                }
            }
        }
    }

    private static void BeginSessionTimer()
    {
        SessionTimer.ResetTimer();
        SessionTimer.PlayTimer();
    }

    private static int FinishSessionTimer()
    {
        return SessionTimer.FinishTimerGetTime();
    }
    #endregion

    #region FIREBASE
    private static void DispatchEventsInQuque()
    {
        HoopslyLogger.LogMessage($"===[In queue: {m_firebaseEventQueue.Count.ToString()} events]===", HoopslyLogLevel.Debug);
        if (!m_sdkInitializedFinished) { HoopslyLogger.LogMessage("===[Dispatch delayed! Firebase initialization incomplite.]===", HoopslyLogLevel.Debug, H_LogType.Warning); return; }
        while (m_firebaseEventQueue.Count > 0)
        {
            HoopslyAnalicsEvent hoopslyEvent = m_firebaseEventQueue.Dequeue();
            if (hoopslyEvent == null)
            {
                HoopslyLogger.LogMessage("======[EVENT IS NULL! SKIPPING TO NEXT!]======", HoopslyLogLevel.Debug, H_LogType.Warning);
            }
            else
            {
                HoopslyLogger.LogMessage($"======[DISPATCHING EVENT: {hoopslyEvent.EventName}]======", HoopslyLogLevel.Debug);
                hoopslyEvent.Dispatch();
            }
        }
    }

    private void RaiseInitEvent()
    {
        if (PlayerPrefs.HasKey("InitSended")) { return; }
        PlayerPrefs.SetInt("InitSended", 1);
        FirebaseAnalytics.LogEvent("Init", GenerateInitParameterArray());
        SetFirstTimeOpenUserProperty();
        Debug.Log("===[Firebase: Init was logged]===");
    }

    private Parameter[] GenerateInitParameterArray()
    {
        List<Parameter> parameters = new List<Parameter>();
        parameters.Add(new Parameter("GPU", SystemInfo.graphicsDeviceName.ToString()));
        parameters.Add(new Parameter("CPU", SystemInfo.processorType.ToString()));
        parameters.Add(new Parameter("RAM", SystemInfo.systemMemorySize));
        parameters.Add(new Parameter("screen_res_x", Screen.width));
        parameters.Add(new Parameter("screen_res_y", Screen.height));
        parameters.Add(new Parameter("hoopsly_sdk_ver", HoopslySettings.Instance.GeneralSettings.SdkVersion));
        parameters.Add(new Parameter("adjust_environment", HoopslySettings.Instance.AdjustSettings.AdjustEnviroment.ToString()));
#if UNITY_IOS
        parameters.Add(new Parameter("idfv", UnityEngine.iOS.Device.vendorIdentifier));
#endif
        return parameters.ToArray();
    }

    #region level_update_events
    public static void RaiseLevelStartEvent(string level_id, bool measureFPS = true, string skin = "", string gameType = "")
    {
        List<HoopslyEventParameter> hoopslyEventParameters = new List<HoopslyEventParameter>();
        hoopslyEventParameters.Add(new HoopslyEventParameter("level_id", level_id));
        hoopslyEventParameters.Add(new HoopslyEventParameter("time", FinishSessionTimer()));
        hoopslyEventParameters.Add(new HoopslyEventParameter("ad_time", AdInterrupterTimer.CurrentDuration));

        if (skin != "")
            hoopslyEventParameters.Add(new HoopslyEventParameter("skin", skin));
        if (gameType != "")
            hoopslyEventParameters.Add(new HoopslyEventParameter("type", gameType));

        m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("level_start", hoopslyEventParameters.ToArray(), new HoopslyEvent_String[]
        {
            new HoopslyEvent_String(level_id, DispatchOrder.BeforeLog, new StringDelegate(RaiseLevelIdUserProperety)),
            new HoopslyEvent_String("game", DispatchOrder.AfterLog, new StringDelegate(SetScreenUserProperty))
        }));
        DispatchEventsInQuque();
        AdInterrupterTimer.FinishTimerGetTime();
        BeginSessionTimer();
        if (FPS_MeasureTool.MeasurmentInProcess)
        {
            FPS_MeasureTool.StopMeasurement();
            HoopslyLogger.LogMessage("===[FPS measurment was not over properly! Force stop previos measurment.]===", HoopslyLogLevel.Debug, H_LogType.Warning);
        }
        if (measureFPS == true)
        {
            FPS_MeasureTool.StartMeasurement();
        }
    }

    public static void RaiseLevelFinishedEvent(string level_id, LevelFinishedResult result, string reason = "", string enemy = "", string gameType = "")
    {

        List<HoopslyEventParameter> eventParams = new List<HoopslyEventParameter>()
        {
            new HoopslyEventParameter("level_id", level_id.ToString()),
            new HoopslyEventParameter("result", result.ToString()),
            new HoopslyEventParameter("time", FinishSessionTimer()),
            new HoopslyEventParameter("ad_time", AdInterrupterTimer.CurrentDuration)
        };

        if (reason != "")
            eventParams.Add(new HoopslyEventParameter("reason", reason));

        if (enemy != "")
            eventParams.Add(new HoopslyEventParameter("enemy", enemy));

        if (gameType != "")
            eventParams.Add(new HoopslyEventParameter("type", gameType));

        if (FPS_MeasureTool.MeasurmentInProcess)
        {
            int[] measureResult = FPS_MeasureTool.StopMeasurement();
            eventParams.Add(new HoopslyEventParameter("fps_avg", measureResult[0]));
            eventParams.Add(new HoopslyEventParameter("fps_perc_1", measureResult[1]));
            eventParams.Add(new HoopslyEventParameter("fps_perc_5", measureResult[2]));
        }

        m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("level_end", eventParams.ToArray(), new HoopslyEvent_String("menu", DispatchOrder.AfterLog, new StringDelegate(SetScreenUserProperty))));

        DispatchEventsInQuque();

        BeginSessionTimer();

        AdInterrupterTimer.FinishTimerGetTime();

        LevelCountEvent();

    }
    private static void LevelCountEvent()
    {
        int currentLevelCount = PlayerPrefs.GetInt("levelCount", 0);
        currentLevelCount++;
        if (currentLevelCount == 5)
        {
            m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("opt_lvl_5"));
            DispatchEventsInQuque();
        }
        else if (currentLevelCount == 10)
        {
            m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("opt_lvl_10"));
            DispatchEventsInQuque();
        }
        else if (currentLevelCount == 20)
        {
            m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("opt_lvl_20"));
            DispatchEventsInQuque();
        }
        PlayerPrefs.SetInt("levelCount", currentLevelCount);

    }

    public static void RaiseTransactionEvent(string type, string detail, string content, int addedSoft_c, int totalSoft_c, int addedHard_c, int totalHard_c, string contentLevel)
    {
        HoopslyEventParameter[] transactionEventParameters = new HoopslyEventParameter[]
        {
            new HoopslyEventParameter("type", type),
            new HoopslyEventParameter("detail", detail),
            new HoopslyEventParameter("content", content),
            new HoopslyEventParameter("soft_c", addedSoft_c),
            new HoopslyEventParameter("soft_total", totalSoft_c),
            new HoopslyEventParameter("hard_c", addedHard_c),
            new HoopslyEventParameter("hard_total", totalHard_c),
            new HoopslyEventParameter("content_level", contentLevel)
        };
        m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("transaction", transactionEventParameters));
        DispatchEventsInQuque();
    }
    #endregion

    #region user_property_events
    private static void RaiseLevelIdUserProperety(string levelId)
    {
        FirebaseAnalytics.SetUserProperty("level_id", levelId);
    }
    private static void SetScreenUserProperty(string gameScreen)
    {
        FirebaseAnalytics.SetUserProperty("screen", gameScreen);
    }

    private static void SetFirstTimeOpenUserProperty()
    {
        FirebaseAnalytics.SetUserProperty("custom_first_open_time", Hoopsly.Internal.Time.TimeUtils.GetUnixTimeInSeconds().ToString());
    }
    #endregion

    #region Ad_events
    private static void RaiseAdAttemptEvent(AdRewardType adRewardType)
    {
        m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("ad_attempt", new HoopslyEventParameter("reward_type", adRewardType.ToString())));
        DispatchEventsInQuque();
    }

    private static void RaiseAdWatchedEvent(string adUnitID, MaxSdkBase.Reward reward, MaxSdkBase.AdInfo adInfo, AdRewardType rewardType)
    {
        m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("ad_watched", new HoopslyEventParameter[]
        {
            new HoopslyEventParameter("ad_network", adInfo.NetworkName),
            new HoopslyEventParameter("reward_type", rewardType.ToString())
        }));
        DispatchEventsInQuque();
    }

    public static void RaiseAdOfferEvent(AdRewardType rewardType)
    {
        m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("ad_offer", new HoopslyEventParameter("reward_type", rewardType.ToString())));
        DispatchEventsInQuque();
    }

    private void RaiseAdRevenueEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        double revenue = adInfo.Revenue;
        m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("ad_revenue", new HoopslyEventParameter[]
        {
            new HoopslyEventParameter("value", revenue),
            new HoopslyEventParameter("ad_source", adInfo.NetworkName),
            new HoopslyEventParameter("ad_format", adInfo.AdFormat),
            new HoopslyEventParameter("currency", "USD"),
            
        }));
        DispatchEventsInQuque();

    }
    #endregion

    #region RemoteConfig
    public static string GetAbGroup()
    {
        return HoopslySettings.Instance.GeneralSettings.AB_Group;
    }

    public static int GetAbTest()
    {
        return HoopslySettings.Instance.GeneralSettings.AB_Test;
    }
    #endregion

    public static void RaiseConsumableEvent(string consumableId)
    {
        m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("consumable", new HoopslyEventParameter("consumable_id", consumableId)));
        DispatchEventsInQuque();
    }

    public static void RaiseUpgradeEvent(string content_id, int level, ChangeCondition upgradeCondition)
    {
        m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("upgrade", new HoopslyEventParameter[]
        {
            new HoopslyEventParameter("content_id", content_id),
            new HoopslyEventParameter("level", level),
            new HoopslyEventParameter("condition", upgradeCondition.ToString())
        }));
        DispatchEventsInQuque();
    }

    public static void RaiseUnlockEvent(string content_id, ChangeCondition unlockCondition)
    {
        m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("unlock", new HoopslyEventParameter[]
        {
            new HoopslyEventParameter("content_id", content_id),
            new HoopslyEventParameter("condition", unlockCondition.ToString())
        }));
        DispatchEventsInQuque();
    }

    public static void RaiseTutoralEvent(string stepName, int stepNum)
    {
        m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("tutoral", new HoopslyEventParameter[]
        {
            new HoopslyEventParameter("step_name", stepName),
            new HoopslyEventParameter("step_num", stepNum)
        }));
        DispatchEventsInQuque();
    }

    public static void RaiseRateUsEvent(int rate)
    {
        m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("rate_us", new HoopslyEventParameter("stars", rate)));
        DispatchEventsInQuque();
    }

    #endregion

    #region Applovin

    #region Interstitial
    public static bool IsInterstitialReady()
    {
        return MaxSdk.IsInterstitialReady(HoopslySettings.Instance.ApplovinSettings.InterstitialAdUnitID);
    }

    public static void ShowInterstitial(InterstitialTypes interstitialType)
    {
        if (MaxSdk.IsInterstitialReady(HoopslySettings.Instance.ApplovinSettings.InterstitialAdUnitID))
        {
            MaxSdk.ShowInterstitial(HoopslySettings.Instance.ApplovinSettings.InterstitialAdUnitID, interstitialType.ToString());
            m_OnAdOpen.Invoke();
        }
        else
        {
            Debug.LogWarning("Ad was not redy!");
        }
    }

    public static void ShowInterstitial()
    {
        if (MaxSdk.IsInterstitialReady(HoopslySettings.Instance.ApplovinSettings.InterstitialAdUnitID))
        {
            MaxSdk.ShowInterstitial(HoopslySettings.Instance.ApplovinSettings.InterstitialAdUnitID);
            m_OnAdOpen.Invoke();
        }
        else
        {
            Debug.LogWarning("Ad was not redy!");
        }
    }

    private void InterWatchCountEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        int currentIntCount = PlayerPrefs.GetInt("interCount", 0);
        currentIntCount++;

        if (currentIntCount == 10)
        {
            m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("opt_int_10"));
            DispatchEventsInQuque();
        }
        else if (currentIntCount == 20)
        {
            m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("opt_int_20"));
            DispatchEventsInQuque();
        }
        else if (currentIntCount == 40)
        {
            m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("opt_int_40"));
            DispatchEventsInQuque();
        }
        PlayerPrefs.SetInt("interCount", currentIntCount);
    }
    #endregion

    #region Rewarded
    public static bool IsRewardedReady()
    {
        return MaxSdk.IsRewardedAdReady(HoopslySettings.Instance.ApplovinSettings.RewardedAdUnitID);
    }

    public static void ShowRewarded(AdRewardType rewardType)
    {
        if (MaxSdk.IsRewardedAdReady(HoopslySettings.Instance.ApplovinSettings.RewardedAdUnitID))
        {
            MaxSdk.ShowRewardedAd(HoopslySettings.Instance.ApplovinSettings.RewardedAdUnitID, rewardType.ToString());
            m_currentRewardType = rewardType;
            m_OnAdOpen.Invoke();
            RaiseAdAttemptEvent(rewardType);
        }
        else
        {
            HoopslyLogger.LogMessage("AD Not ready!", HoopslyLogLevel.Suppress, H_LogType.Error);
            //LoadRewardedAd();
        }
    }

    private void RewardWatchCountEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        int currentRewardCount = PlayerPrefs.GetInt("rewardCount", 0);
        currentRewardCount++;
        if (currentRewardCount == 2)
        {
            m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("opt_rew_2"));
            DispatchEventsInQuque();
        }
        else if (currentRewardCount == 5)
        {
            m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("opt_rew_5"));
            DispatchEventsInQuque();
        }
        else if (currentRewardCount == 10)
        {
            m_firebaseEventQueue.Enqueue(new HoopslyAnalicsEvent("opt_rew_10"));
            DispatchEventsInQuque();
        }
        PlayerPrefs.SetInt("rewardCount", currentRewardCount);
    }
    #endregion

    #region Banner
    public static void ShowBanner()
    {
        if (m_sdkInitializedFinished)
        {
            MaxSdk.ShowBanner(HoopslySettings.Instance.ApplovinSettings.BannerAdUnitID);
        }
        else
        { 
            HoopslyLogger.LogMessage("======[Initilizetion incomplete BANNER show delayed]======", HoopslyLogLevel.Debug, H_LogType.Warning);
            HoopslyCallbacks.OnHoopslyInitialized += LateBannerShow;
        }
    }

    private static void LateBannerShow()
    {
        HoopslyLogger.LogMessage("======[Initilizetion finished showing BANNER]======", HoopslyLogLevel.Debug, H_LogType.Warning);
        MaxSdk.ShowBanner(HoopslySettings.Instance.ApplovinSettings.BannerAdUnitID);
        HoopslyCallbacks.OnHoopslyInitialized -= LateBannerShow;
    }

    public static void HideBanner()
    {
        MaxSdk.HideBanner(HoopslySettings.Instance.ApplovinSettings.BannerAdUnitID);
    }
    #endregion

    #region Mrec
    public static void ShowMREC()
    {
        if(m_sdkInitializedFinished)
        {
            MaxSdk.ShowMRec(HoopslySettings.Instance.ApplovinSettings.MRECAdUnitID);        
        }
        else
        {
            HoopslyLogger.LogMessage("======[Initilizetion incomplete MREC show delayed]======", HoopslyLogLevel.Debug, H_LogType.Warning);
            HoopslyCallbacks.OnHoopslyInitialized += LateMREcShow;
        }
    }

    private static void LateMREcShow()
    {
        HoopslyLogger.LogMessage("======[Initilizetion finished showing MREC]======", HoopslyLogLevel.Debug, H_LogType.Warning);
        MaxSdk.ShowMRec(HoopslySettings.Instance.ApplovinSettings.MRECAdUnitID);
        HoopslyCallbacks.OnHoopslyInitialized -= LateMREcShow;
    }

    public static void HideMREC()
    {
        MaxSdk.HideMRec(HoopslySettings.Instance.ApplovinSettings.MRECAdUnitID);
    }
    #endregion

    #endregion



    private void Awake()
    {
        //INTER
        HoopslyCallbacks.Interstitial.OnAdHiddenEvent += InterWatchCountEvent;
        HoopslyCallbacks.Interstitial.OnAdHiddenEvent += OnAdClosed;
        HoopslyCallbacks.Interstitial.OnAdDisplayFailedEvent += OnAdClosed;

        //REWARDED
        HoopslyCallbacks.Rewarded.OnAdHiddenEvent += RewardWatchCountEvent;
        HoopslyCallbacks.Rewarded.OnAdReceivedRewardEvent += RaiseAdWatchedEvent;
        HoopslyCallbacks.Rewarded.OnAdHiddenEvent += OnAdClosed;
        HoopslyCallbacks.Rewarded.OnAdDisplayFailedEvent += OnAdClosed;


        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += RaiseAdRevenueEvent;
        MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += RaiseAdRevenueEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += RaiseAdRevenueEvent;
        MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent += RaiseAdRevenueEvent;

        HoopslyCallbacks.OnHoopslyInitialized += SdkInitilizeComplete;
        HoopslyCallbacks.OnRemoteConfigRecived += OnRecivedRemoteConfig;
    }
    private void OnDestroy()
    {
        HoopslyCallbacks.Interstitial.OnAdHiddenEvent -= InterWatchCountEvent;
        HoopslyCallbacks.Interstitial.OnAdHiddenEvent -= OnAdClosed;
        HoopslyCallbacks.Interstitial.OnAdDisplayFailedEvent -= OnAdClosed;
        HoopslyCallbacks.Rewarded.OnAdHiddenEvent -= RewardWatchCountEvent;
        HoopslyCallbacks.Rewarded.OnAdReceivedRewardEvent -= RaiseAdWatchedEvent;
        HoopslyCallbacks.Rewarded.OnAdHiddenEvent -= OnAdClosed;
        HoopslyCallbacks.Rewarded.OnAdDisplayFailedEvent -= OnAdClosed;

        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent -= RaiseAdRevenueEvent;
        MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent -= RaiseAdRevenueEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent -= RaiseAdRevenueEvent;
        MaxSdkCallbacks.MRec.OnAdRevenuePaidEvent -= RaiseAdRevenueEvent;

        HoopslyCallbacks.OnRemoteConfigRecived -= OnRecivedRemoteConfig;

        if (FPS_MeasureTool.MeasurmentInProcess) { FPS_MeasureTool.StopMeasurement(); }
        Destroy(this.gameObject);
    }

    private void SdkInitilizeComplete()
    {
        m_sdkInitializedFinished = true;
        //===EXECUTE-AT-INITILIZE-COMPLETE===
        RaiseInitEvent();
        SetScreenUserProperty("menu");
        BackGroundTimer.ResetTimer();
        AdInterrupterTimer.ResetTimer();
        BeginSessionTimer();
        DispatchEventsInQuque();
        HoopslyCallbacks.OnHoopslyInitialized -= SdkInitilizeComplete;
    }

    private void OnRecivedRemoteConfig(string ab_group, int ab_test)
    {
        if (String.IsNullOrEmpty(ab_group))
            FirebaseAnalytics.SetUserProperty("ab_group", null);
        else
            FirebaseAnalytics.SetUserProperty("ab_group", ab_group);

        if(ab_test == 0)
            FirebaseAnalytics.SetUserProperty("ab_test", null);
        else
            FirebaseAnalytics.SetUserProperty("ab_test", ab_test.ToString());
    }

    private void OnAdClosed(string adUnitID, MaxSdkBase.AdInfo adInfo)
    {
        m_OnAdClosed.Invoke();
    }
    private void OnAdClosed(string adUnitID, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        m_OnAdClosed.Invoke();
    }


    private void OnApplicationPause(bool pause)
    {
        UpdateTimersStateOnPause(pause);
    }

    public static void CreateInstance()
    {
        if (m_instance != null) { return; }
        m_instance = new GameObject("HoopslyMethods").AddComponent<HoopslyIntegration>();
        DontDestroyOnLoad(m_instance.gameObject);
    }
}