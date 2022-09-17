using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Hoopsly.Settings
{
    public class HoopslySettings : ScriptableObject
    {
        [ReadOnly] [SerializeField] private HoopslyApplovinSettings m_applovinSettings;
        public HoopslyApplovinSettings ApplovinSettings
        {
            get
            {
                return Instance.m_applovinSettings;
            }
            private set
            {
                Instance.m_applovinSettings = value;
            }
        }

        [ReadOnly] [SerializeField] private HoopslyGeneralSettings m_generalSettings;
        public HoopslyGeneralSettings GeneralSettings
        {
            get
            {
                return Instance.m_generalSettings;
            }
            private set
            {
                Instance.m_generalSettings = value;
            }
        }

        [ReadOnly] [SerializeField] private HoopslyAdjustSettings m_adjustSettings;
        public HoopslyAdjustSettings AdjustSettings
        {
            get
            {
                return Instance.m_adjustSettings;
            }
            private set
            {
                Instance.m_adjustSettings = value;
            }
        }

        private const string settingsAssetPath = "Assets/Hoopsly/Resources/HoopslySettings.asset";
        private static HoopslySettings instance;
        public static HoopslySettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load("HoopslySettings") as HoopslySettings;
                    if (instance != null)
                    {
                        return instance;
                    }
#if UNITY_EDITOR
                    else
                    {
                        instance = CreateInstance<HoopslySettings>();
                        AssetDatabase.CreateAsset(instance, settingsAssetPath);
                        instance.GenerateSettingsSubAssets();
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
#endif
                }
                return instance;
            }
        }

#if UNITY_EDITOR
        public void GenerateSettingsSubAssets()
        {
            if (GeneralSettings == null)
                GenerateGeneralSettings();
            if (ApplovinSettings == null)
                GenerateApplovinSettings();
            if (AdjustSettings == null)
                GenerateAdjustSettings();
        }

        private void GenerateAdjustSettings()
        {
            AdjustSettings = ScriptableObject.CreateInstance<HoopslyAdjustSettings>();
            AdjustSettings.name = "HoopslyAdjustSettings";
            AssetDatabase.AddObjectToAsset(AdjustSettings, this);
        }

        private void GenerateGeneralSettings()
        {
            GeneralSettings = ScriptableObject.CreateInstance<HoopslyGeneralSettings>();
            GeneralSettings.name = "HoopslyGeneralSettings";
            AssetDatabase.AddObjectToAsset(GeneralSettings, this);
        }

        private void GenerateApplovinSettings()
        {
            ApplovinSettings = ScriptableObject.CreateInstance<HoopslyApplovinSettings>();
            ApplovinSettings.name = "HoopslyApplovinSettings";
            AssetDatabase.AddObjectToAsset(ApplovinSettings, this);
        }

        public void SaveSettingsAsync()
        {
            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(m_applovinSettings);
            EditorUtility.SetDirty(m_adjustSettings);
            EditorUtility.SetDirty(m_generalSettings);
        }
#endif

    }
}
