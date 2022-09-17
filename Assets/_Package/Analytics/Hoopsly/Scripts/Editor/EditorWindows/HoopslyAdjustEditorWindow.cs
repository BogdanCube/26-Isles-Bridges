using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Hoopsly.Settings;
using Hoopsly.Editor.Resources;
using com.adjust.sdk;

namespace Hoopsly.Editor
{
    public class HoopslyAdjustEditorWindow
    {
        public void DrawEditor()
        {
            GUILayout.Label("Adjust settings", EditorResources.Styles.TitleLableStyle);
            using (var v = new EditorGUILayout.VerticalScope("box"))
            {
                GUILayout.Space(15);
                HoopslySettings.Instance.AdjustSettings.UseAdjust = EditorGUILayout.ToggleLeft("Enable Adjust SDK", HoopslySettings.Instance.AdjustSettings.UseAdjust);
                if (HoopslySettings.Instance.AdjustSettings.UseAdjust)
                {
                    GUILayout.Space(15);
                    HoopslySettings.Instance.AdjustSettings.AdjustAppToken = EditorGUILayout.DelayedTextField("Adjust App token", HoopslySettings.Instance.AdjustSettings.AdjustAppToken);
                    GUILayout.Space(5);
                    HoopslySettings.Instance.AdjustSettings.AdjustLogLevel = (AdjustLogLevel)EditorGUILayout.EnumPopup(new GUIContent("Log level", m_adjustLogLevelToolTip), HoopslySettings.Instance.AdjustSettings.AdjustLogLevel);
                    GUILayout.Space(5);
                    HoopslySettings.Instance.AdjustSettings.AdjustEnviroment = (AdjustEnvironment)EditorGUILayout.EnumPopup("Enviroment", HoopslySettings.Instance.AdjustSettings.AdjustEnviroment);
                    GUILayout.Space(5);
                    HoopslySettings.Instance.AdjustSettings.AdjustSendInBackground = EditorGUILayout.ToggleLeft(new GUIContent("Send in background", m_adjustBackgroundTracking), HoopslySettings.Instance.AdjustSettings.AdjustSendInBackground);
                    GUILayout.Space(5);
                    HoopslySettings.Instance.AdjustSettings.AdjustEventBufferingEnabled = EditorGUILayout.ToggleLeft(new GUIContent("Event buffering", m_adjustEventBufferingTooltip), HoopslySettings.Instance.AdjustSettings.AdjustEventBufferingEnabled);
                    GUILayout.Space(5);
                    HoopslySettings.Instance.AdjustSettings.AdjustLaunchDeferredDeeplink = EditorGUILayout.ToggleLeft("Launch deferred deeplink", HoopslySettings.Instance.AdjustSettings.AdjustLaunchDeferredDeeplink);

                }
                GUILayout.Space(15);
            }
        }

        #region tooltips
        const string m_adjustLogLevelToolTip = "Verbose - enable all logs \nDebug - disable verbose logs \nInfo - disable debug logs (default) \nWarn - disable info logs \nError - disable warning logs \nAssert - disable error logs \nSuppress - disable all logs";
        const string m_adjustBackgroundTracking = "The default behaviour of the Adjust SDK is to pause sending network requests while the app is in the background. You can change this by checking this box.";
        const string m_adjustEventBufferingTooltip = "If your app makes heavy use of event tracking, you might want to delay some network requests in order to send them in one batch every minute.";
        #endregion
    }
}
