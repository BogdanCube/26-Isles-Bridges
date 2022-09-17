using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.adjust.sdk;

namespace Hoopsly.Settings
{
    public class HoopslyAdjustSettings : ScriptableObject
    {
        [SerializeField] private bool m_useAdjust = false;
        [SerializeField] private string m_adjustAppToken = "{Your App Token}";
        [SerializeField] private AdjustLogLevel m_adjustLogLevel = AdjustLogLevel.Info;
        [SerializeField] private AdjustEnvironment m_adjustEnviroment = AdjustEnvironment.Production;
        [SerializeField] private bool m_adjustSendInBackground = false;
        [SerializeField] private bool m_adjustEvenBufferingEnabled = false;
        [SerializeField] private bool m_adjustLaunchDeferredDeeplink = true;

        #region Adjust
        public bool UseAdjust
        {
            get { return this.m_useAdjust; }
            set { this.m_useAdjust = value; }
        }

        public string AdjustAppToken
        {
            get { return this.m_adjustAppToken; }
            set { this.m_adjustAppToken = value; }
        }

        public AdjustLogLevel AdjustLogLevel
        {
            get { return this.m_adjustLogLevel; }
            set { this.m_adjustLogLevel = value; }
        }

        public AdjustEnvironment AdjustEnviroment
        {
            get { return this.m_adjustEnviroment; }
            set { this.m_adjustEnviroment = value; }
        }

        public bool AdjustSendInBackground
        {
            get { return this.m_adjustSendInBackground; }
            set { this.m_adjustSendInBackground = value; }
        }

        public bool AdjustEventBufferingEnabled
        {
            get { return this.m_adjustEvenBufferingEnabled; }
            set { this.m_adjustEvenBufferingEnabled = value; }
        }

        public bool AdjustLaunchDeferredDeeplink
        {
            get { return this.m_adjustLaunchDeferredDeeplink; }
            set { this.m_adjustLaunchDeferredDeeplink = value; }
        }
        #endregion

        private void SaveChanges()
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssetIfDirty(this);
            UnityEditor.AssetDatabase.Refresh();
#endif
        }
    }
}
