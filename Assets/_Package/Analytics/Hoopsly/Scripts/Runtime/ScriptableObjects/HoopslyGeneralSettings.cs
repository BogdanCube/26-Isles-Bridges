using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hoopsly.Settings
{
    public class HoopslyGeneralSettings : ScriptableObject, ISerializationCallbackReceiver
    {
        [ReadOnly] private static string m_sdkVersion = "1.1.0f1";
        public string SdkVersion
        {
            get { return m_sdkVersion; }
        }


        [SerializeField] private bool m_startSdkManually = false;
        [SerializeField] private float m_fpsMeasureIntervals = 1;
        [SerializeField] private bool m_debugFpsMeasurment = false;
        [SerializeField] private HoopslyLogLevel m_hoopslyEventsLogLevel = HoopslyLogLevel.Debug;

        [SerializeField] private bool m_enableGDPR_screen = true;
        [SerializeField] private bool m_overrideGDPR_region = false;

        [SerializeField] private bool m_pauseGameOnGDPR = true;
        [SerializeField] private GDPR_presetObject m_activeGDPR_Preset;
        [SerializeField] private GDPR_presetObject m_defaultGDPR_Preset;

        [SerializeField] private bool m_useFacebook = false;

        [HideInInspector] private string[] m_defaultArRewardTypes = { "revive", "multiply", "skin", "skin_1", "skin_2", "skin_3", "coins_1", "coins_2", "coins_3", "bonus_level", "upgrade_attack", "upgrade_def", "upgrade_speed", "upgrade_spawn", "upgrade_hp", "upgrade_income", "other", "win_screen_coinsx2", "lab_upgrade", "win_screen_getSkin", "daily_claimx3", "daily_restore", "daily", "milestone" };
        public string[] DefaultAdRewardTypes { get { return m_defaultArRewardTypes; } }

        private string m_AB_group = "";
        private int m_AB_test = 0;

        public void UpdateRemoteConfigVariables(Hoopsly.Internal.RemoteConfigInitResult remoteConfinInitResult)
        {
            m_AB_group = remoteConfinInitResult.AB_group;
            m_AB_test = remoteConfinInitResult.AB_test;
        }

        public string AB_Group { get { return m_AB_group; } }
        public int AB_Test { get { return m_AB_test; } }

        [SerializeField] private bool m_enableIngameConsole = false;
        public bool EnableEngameConsole
        {
            get { return this.m_enableIngameConsole; }
            set { this.m_enableIngameConsole = value; SaveChanges(); }
        }

        public bool StartSdkManually
        {
            get { return this.m_startSdkManually; }
            set { this.m_startSdkManually = value; SaveChanges(); }
        }

        public bool UseFacebook
        {
            get { return this.m_useFacebook; }
            set { this.m_useFacebook = value; SaveChanges(); }
        }

        public bool DebugFpsMeasurment
        {
            get { return this.m_debugFpsMeasurment; }
            set { this.m_debugFpsMeasurment = value; SaveChanges(); }
        }
        public float FPSmeasueIntervals
        {
            get { return this.m_fpsMeasureIntervals; }
            set { this.m_fpsMeasureIntervals = value; SaveChanges(); }
        }
        public HoopslyLogLevel HoopslyEventsLogLevel
        {
            get { return this.m_hoopslyEventsLogLevel; }
            set { this.m_hoopslyEventsLogLevel = value; SaveChanges(); }
        }

        public bool EnableGDPR_Screen
        {
            get { return this.m_enableGDPR_screen; }
            set { this.m_enableGDPR_screen = value; SaveChanges(); }
        }
        public bool OverrigeGDPR_Region
        {
            get { return this.m_overrideGDPR_region; }
            set 
            { 
                this.m_overrideGDPR_region = value;
                SaveChanges();
            }
        }

        public bool PauseGameOnGDPR
        {
            get { return this.m_pauseGameOnGDPR; }
            set { this.m_pauseGameOnGDPR = value; SaveChanges(); }
        }

        public GDPR_presetObject DefaultGDPR_Preset
        {
            get
            {
#if UNITY_EDITOR
                if (this.m_defaultGDPR_Preset == null)
                    this.m_defaultGDPR_Preset = (GDPR_presetObject)this.GetGDPR_DefaultPresetFile();
#endif
                return this.m_defaultGDPR_Preset;
            }
        }

        public GDPR_presetObject ActiveGDPR_Preset
        {
            get
            {
                if (this.m_activeGDPR_Preset == null)
                    this.m_activeGDPR_Preset = DefaultGDPR_Preset;
                return this.m_activeGDPR_Preset;
            }
            set { this.m_activeGDPR_Preset = value; }
        }

        private void SaveChanges()
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            //UnityEditor.AssetDatabase.SaveAssetIfDirty(this);
            //UnityEditor.AssetDatabase.Refresh();
#endif
        }

#if UNITY_EDITOR
        private Object GetGDPR_DefaultPresetFile()
        {
            return UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Hoopsly/Resources/GDPR/GDPR_ScreenPresets/Default/Default Preset.asset", typeof(Object));
        }
#endif

        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (this.m_defaultGDPR_Preset == null)
            {
                m_defaultGDPR_Preset = (GDPR_presetObject)this.GetGDPR_DefaultPresetFile();
            }
            if (m_activeGDPR_Preset == null)
            {
                m_activeGDPR_Preset = m_defaultGDPR_Preset;
            }
#endif
        }

        public void OnAfterDeserialize()
        {
        }

    }

    public enum HoopslyLogLevel { Suppress, Debug, Verbose, };
}

