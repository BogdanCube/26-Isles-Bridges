using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Hoopsly.Editor
{
    public class EditorSettingsWindow : EditorWindow
    {
        private static bool m_androidUpdateRequered;
        private static bool m_iosUpdateRequered;
        private static EditorSettingsWindow m_windowReference;
        public static void Init(bool androidUpdateRequered, bool iosUpdateRequered)
        {
            m_windowReference = (EditorSettingsWindow)EditorSettingsWindow.GetWindow(typeof(EditorSettingsWindow), true, "Editor settings");
            m_windowReference.minSize = new Vector2(350, 210);
            m_windowReference.maxSize = m_windowReference.minSize;
            m_androidUpdateRequered = androidUpdateRequered;
            m_iosUpdateRequered = iosUpdateRequered;
            m_windowReference.Show();
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            if(m_androidUpdateRequered)
                DrawSettings(BuildTargetGroup.Android);
            if(m_iosUpdateRequered)
                DrawSettings(BuildTargetGroup.iOS);

            Rect rect = EditorGUILayout.GetControlRect(false, 2);
            EditorGUI.DrawRect(rect, new Color(0.40f, 0.40f, 0.40f, 1));
            GUILayout.Space(5);
            if (GUILayout.Button("FIX ALL"))
            {
                FixAllSettings();
            }
        }

        private void DrawSettings(BuildTargetGroup targetGroup)
        {
            GUILayout.Label($"<b>Some {targetGroup} settings Requer update</b>", VersionHeaderLableStyle);
            Rect rect = EditorGUILayout.GetControlRect(false, 1);
            EditorGUI.DrawRect(rect, new Color(0.40f, 0.40f, 0.40f, 1));
            GUILayout.Space(5);
            if (PlayerSettings.GetApiCompatibilityLevel(targetGroup) != ApiCompatibilityLevel.NET_4_6)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("ApiCompatibilityLevel <b>.NET 4.x</b> requered", IssuelineLableStyle);
                if(GUILayout.Button("FIX", GUILayout.Width(60)))
                {
                    PlayerSettings.SetApiCompatibilityLevel(targetGroup, ApiCompatibilityLevel.NET_4_6);
                    CheckForWondowClose();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.Space(5);
            if(PlayerSettings.GetScriptingBackend(targetGroup) != ScriptingImplementation.IL2CPP)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Scripting Backend <b>IL2CPPx</b> requered", IssuelineLableStyle);
                if (GUILayout.Button("FIX", GUILayout.Width(60)))
                {
                    PlayerSettings.SetScriptingBackend(targetGroup, ScriptingImplementation.IL2CPP);
                    CheckForWondowClose();
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.Space(10);
        }

        private void CheckForWondowClose()
        {
            if(IsPlatforSettingsCorect(BuildTargetGroup.Android) && IsPlatforSettingsCorect(BuildTargetGroup.iOS))
            {
                m_windowReference.Close();
            }
        }

        private void FixAllSettings()
        {
            if (IsBuildTargetSupported(BuildTargetGroup.Android, BuildTarget.Android))
            {
                PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android, ApiCompatibilityLevel.NET_4_6);
                PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
            }
            if (IsBuildTargetSupported(BuildTargetGroup.iOS, BuildTarget.iOS))
            {
                PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.iOS, ApiCompatibilityLevel.NET_4_6);
                PlayerSettings.SetScriptingBackend(BuildTargetGroup.iOS, ScriptingImplementation.IL2CPP);
            }
            CheckForWondowClose();
        }

        private bool IsPlatforSettingsCorect(BuildTargetGroup buildTargetGroup)
        {
            if(PlayerSettings.GetApiCompatibilityLevel(buildTargetGroup) == ApiCompatibilityLevel.NET_4_6 &&
               PlayerSettings.GetScriptingBackend(buildTargetGroup) == ScriptingImplementation.IL2CPP)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsBuildTargetSupported(BuildTargetGroup buildTargetGroup, BuildTarget buildTarget)
        {
            return BuildPipeline.IsBuildTargetSupported(buildTargetGroup, buildTarget);
        }

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
                        richText = true
                    };
                }
                return m_versionHeaderStyle;
            }
        }

        private GUIStyle m_issuelineLableStyle;
        private GUIStyle IssuelineLableStyle
        {
            get
            {
                if (m_issuelineLableStyle == null)
                {
                    m_issuelineLableStyle = new GUIStyle(EditorStyles.label)
                    {
                        fontSize = 13,
                        fontStyle = FontStyle.Normal,
                        richText = true
                    };
                }
                return m_issuelineLableStyle;
            }
        }
    }
}
