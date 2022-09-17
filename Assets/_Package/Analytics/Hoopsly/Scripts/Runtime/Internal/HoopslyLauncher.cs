using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

using Hoopsly.GRDP;
using Hoopsly.Settings;
using Hoopsly.Internal.Adjust;
using Hoopsly.Internal.FirebaseSdk;
using Hoopsly.Internal.Applovin;
using IngameDebugConsole;

namespace Hoopsly.Internal
{
    public class HoopslyLauncher : MonoBehaviour
    {
        private static HoopslyLauncher instance;
        public static HoopslyLauncher Instance
        {
            get { return instance; }
        }
        private FirebaseInitializer firebaseInit;
        private FirebaseInitializer FirebaseInitializer
        {
            get
            {
                if (firebaseInit == null)
                    firebaseInit = new FirebaseInitializer();
                return firebaseInit;
            }
        }
        private FirebaseRConfigInitializer firebaseRConfigInit;
        private FirebaseRConfigInitializer FirebaseRConfigInit
        {
            get
            {
                if (firebaseRConfigInit == null)
                    firebaseRConfigInit = new FirebaseRConfigInitializer();
                return firebaseRConfigInit;
            }
        }

        private ApplovinInitializer applovinIntegration;
        private ApplovinInitializer ApplovinIntegration
        {
            get
            {
                if (applovinIntegration == null)
                    applovinIntegration = new ApplovinInitializer();
                return applovinIntegration;
            }
        }
        private AdjustInitializer adjustInitializer;
        private AdjustInitializer AdjustInitializer
        {
            get
            {
                if (adjustInitializer == null)
                    adjustInitializer = new AdjustInitializer();
                return adjustInitializer;
            }
        }
        private string m_uuid;
        private bool m_initSequenceComplete = false;
        [ReadOnly][SerializeField] private GameObject m_consolePrefab;
        private static bool m_ConsentRecived;

        #region Callbacks
        public static event Action m_OnHoopslyInitialized = delegate { };
        public static event Action<string, int> m_OnRemoteConfigRecived = delegate { };
        #endregion

        private void Awake()
        {
            CreateInstance();
            HoopslyCallbacks.CreateInstance();
            HoopslyIntegration.CreateInstance();
            firebaseInit = new FirebaseInitializer();
            applovinIntegration = new ApplovinInitializer();
            adjustInitializer = new AdjustInitializer();
            InitiDebugConsole();

            if (!HoopslySettings.Instance.GeneralSettings.StartSdkManually)
            {
                AutoSDKStart();
            }
        }

        private async void AutoSDKStart()
        {
            m_uuid = GetOrGenerateUUID();
            await InitSequnce(m_uuid);
        }

        public void StartSDK()
        {
            m_uuid = GetOrGenerateUUID();
            ManualSDKStart(m_uuid);
        }

        public void StartSDK(string uuid)
        {
            m_uuid = uuid;
            ManualSDKStart(m_uuid);
        }

        private async void ManualSDKStart(string uuid)
        {
            await InitSequnce(m_uuid);
        }

        private void InitiDebugConsole()
        {
            if (HoopslySettings.Instance.GeneralSettings.EnableEngameConsole == true && m_consolePrefab != null)
            {
                Instantiate(m_consolePrefab, transform);
            }
        }

        private void OnApplicationPause(bool pause)
        {
            if (!pause)
            {
                if (HoopslySettings.Instance.GeneralSettings.UseFacebook && m_initSequenceComplete)
                {
                    InitFacebookSDK();
                }
            }
        }

        private void CreateInstance()
        {
            DontDestroyOnLoad(this.gameObject);
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private async Task InitSequnce(string uuid)
        {
            HoopslyLogger.LogMessage($"==========[Hoopsly_sdk_ver: {HoopslySettings.Instance.GeneralSettings.SdkVersion}]==========", HoopslyLogLevel.Suppress);
            MaxSdkBase.SdkConfiguration applovinInitResult = await ApplovinIntegration.Initilize(uuid);
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
#if UNITY_IPHONE || UNITY_IOS
                if(applovinInitResult.AppTrackingStatus == MaxSdkBase.AppTrackingStatus.Authorized)
                {
                    MaxSdk.SetHasUserConsent(true);
                    await InitAnalitics(uuid);
                }
                else
                {
                    await AdjustInitializer.Initilize(uuid);
                }
                m_OnHoopslyInitialized.Invoke();
#endif
            }
            else
            {
#if UNITY_EDITOR || UNITY_ANDROID
                if (applovinInitResult.ConsentDialogState == MaxSdkBase.ConsentDialogState.Applies)
                {
                    if (HoopslySettings.Instance.GeneralSettings.EnableGDPR_Screen)
                    {
                        if (await GDRP_ConsentDialogue() == ConsentValue.accepted)
                        {
                            MaxSdk.SetHasUserConsent(true);
                        }
                    }
                    await InitAnalitics(uuid);
                }
                else
                {
                    await InitAnalitics(uuid);
                }
                m_OnHoopslyInitialized.Invoke();
#endif
            }
            m_initSequenceComplete = true;
        }

        private async Task InitAnalitics(string uuid)
        {
            await FirebaseInitializer.Initilize(uuid);

            RemoteConfigInitResult config = await FirebaseRConfigInit.Initilize(uuid);
            if(config!=null)
            {
                HoopslySettings.Instance.GeneralSettings.UpdateRemoteConfigVariables(config);
                m_OnRemoteConfigRecived.Invoke(HoopslySettings.Instance.GeneralSettings.AB_Group, HoopslySettings.Instance.GeneralSettings.AB_Test);
            }

            await AdjustInitializer.Initilize(uuid);
            InitFacebookSDK();
            InitAudienceNetwork();
        }

        private async Task<ConsentValue> GDRP_ConsentDialogue()
        {
            HoopslyLogger.LogMessage("======[GDPR_DIALOGUIE_CALLED]======", HoopslyLogLevel.Verbose);


            int cStatus = PlayerPrefs.GetInt("cStatus", 0);
            if (cStatus == 0)
            {
                if (HoopslySettings.Instance.GeneralSettings.PauseGameOnGDPR)
                    UnityEngine.Time.timeScale = 0;

                GDPR_screen_handler gDRP_Screen = Instantiate(HoopslySettings.Instance.GeneralSettings.ActiveGDPR_Preset.RelativePrefab, this.transform).GetComponent<GDPR_screen_handler>();
                while (gDRP_Screen.GetConsentValue == ConsentValue.none)
                {
                    await Task.Yield();
                }

                if (gDRP_Screen.GetConsentValue == ConsentValue.accepted)
                {
                    HoopslyLogger.LogMessage("======[CONSENT_RECIVED!_ACTIVATING_ANALITIC_SDK!]======", HoopslyLogLevel.Verbose);
                    cStatus = 1;
                    PlayerPrefs.SetInt("cStatus", 1);
                }

                if (gDRP_Screen.GetConsentValue == ConsentValue.declined)
                {
                    HoopslyLogger.LogMessage("======[CONSENT_REJECTED!_SKIPPING_ANALITIC_SDK!]======", HoopslyLogLevel.Verbose);
                    cStatus = 2;
                    PlayerPrefs.SetInt("cStatus", 2);
                }

                if (HoopslySettings.Instance.GeneralSettings.PauseGameOnGDPR)
                    UnityEngine.Time.timeScale = 1;

                Destroy(gDRP_Screen.gameObject);

                return (ConsentValue)cStatus;
            }
            else
            {
                return (ConsentValue)PlayerPrefs.GetInt("cStatus");
            }
        }

        private void InitFacebookSDK()
        {
            HoopslyLogger.LogMessage("==========[FACEBOOK!]==========", HoopslyLogLevel.Suppress);
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                FB.Init(() =>
                {
                    FB.ActivateApp();
                });
            }
        }

        private void InitAudienceNetwork()
        {
            AudienceNetwork.AudienceNetworkAds.Initialize();
#if UNITY_IOS

            AudienceNetwork.AdSettings.SetAdvertiserTrackingEnabled(true);
#endif
        }

#region UUID get or generate
        private string GetOrGenerateUUID()
        {
            string uuid = "";
            if (!PlayerPrefs.HasKey("UUID"))
            {
                uuid = Guid.NewGuid().ToString();
                PlayerPrefs.SetString("UUID", uuid);
            }
            else
            {
                uuid = PlayerPrefs.GetString("UUID");
            }
            return uuid;
        }
#endregion

    }
    public class RemoteConfigInitResult
    {
        public RemoteConfigInitResult(string ab_group, int ab_test)
        {
            m_ab_group = ab_group;
            m_ab_test = ab_test;
        }
        private string m_ab_group;
        private int m_ab_test;
        public string AB_group { get { return m_ab_group; } }
        public int AB_test { get { return m_ab_test; } }
    }
}
