using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Hoopsly.Settings;

namespace Hoopsly.Editor
{
    public class VersionWarningWindow : EditorWindow
    {
        private GUIStyle m_versionHeaderStyle;
        private GUIStyle VersionHeaderLableStyle
        {
            get
            {
                if (m_versionHeaderStyle == null)
                {
                    m_versionHeaderStyle = new GUIStyle(EditorStyles.label)
                    {
                        fontSize = 14,
                        fontStyle = FontStyle.Normal,
                        stretchHeight = true,
                        alignment = TextAnchor.MiddleCenter,
                        richText = true
                    };
                }
                return m_versionHeaderStyle;
            }
        }

        private static string m_remoteVersion;
        private static bool m_showBetaWarning;

        public static void Init(string remoteReleseData, bool betaWarning)
        {
            VersionWarningWindow window = (VersionWarningWindow)VersionWarningWindow.GetWindow(typeof(VersionWarningWindow), true, "SDK VERSION IS OUTDATED");
            window.minSize = new Vector2(500, 200);
            window.maxSize = window.minSize;
            m_remoteVersion = remoteReleseData;
            m_showBetaWarning = betaWarning;
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.BeginVertical("box", GUILayout.Height(100));
            GUILayout.Label("YOUR SDK VERSION IS OUTDATED!", VersionHeaderLableStyle);
            Rect rect = EditorGUILayout.GetControlRect(false, 1);
            EditorGUI.DrawRect(rect, new Color(0.40f, 0.40f, 0.40f, 1));
            if (m_showBetaWarning)
            {
                GUILayout.Label("<b>WARNING! YOU ARE USING BETA VERSION!</b>", VersionHeaderLableStyle);
                GUILayout.Label("It may be unstable and cause bugs!", VersionHeaderLableStyle);
                Rect rect2 = EditorGUILayout.GetControlRect(false, 1);
                EditorGUI.DrawRect(rect2, new Color(0.40f, 0.40f, 0.40f, 1));
            }
            GUILayout.Label($"Current version is: <b>{HoopslySettings.Instance.GeneralSettings.SdkVersion}</b>. Latest version is: <b>{m_remoteVersion}</b>", VersionHeaderLableStyle);
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            GUILayout.Space(20);
            if (GUILayout.Button("OPEN DOCUMENTAION FOR A LATEST VERSION", GUILayout.Height(30)))
            {
                Application.OpenURL("https://docs.google.com/document/d/1OUFI8LWsckBgGsriPKd_OziHdR94IftRTTxVUQqWjoE/");
            }
            GUILayout.Space(20);
            GUILayout.EndVertical();
        }
    }
}