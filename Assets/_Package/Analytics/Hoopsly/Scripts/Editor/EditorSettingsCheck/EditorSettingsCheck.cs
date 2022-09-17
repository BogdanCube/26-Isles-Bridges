using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace Hoopsly.Editor
{
    [InitializeOnLoad]
    public class EditorSettingsCheck
    {
        private static bool m_androidSettingsRequierUpdate = false;
        private static bool m_iosSettingsRequerUpdate = false;

        static EditorSettingsCheck()
        {
            EditorApplication.update += SettingsCheck;
        }

        private static void SettingsCheck()
        {
            if (!SessionState.GetBool("settingsChecked", false))
            {
                CheckEditorSettings();
            }
            EditorApplication.update -= SettingsCheck;
        }

        private static void CheckEditorSettings()
        {
            var currentBuildTarget = EditorUserBuildSettings.activeBuildTarget;
            bool iosBuildAvalibale = BuildPipeline.IsBuildTargetSupported(BuildTargetGroup.iOS, BuildTarget.iOS);
            if(iosBuildAvalibale)
            {
                m_iosSettingsRequerUpdate = IsTargetGroupSettingsRequerFix(BuildTargetGroup.iOS);
            }
            bool androidBuildAvalible = BuildPipeline.IsBuildTargetSupported(BuildTargetGroup.Android, BuildTarget.Android);
            if (androidBuildAvalible)
            {
                m_androidSettingsRequierUpdate = IsTargetGroupSettingsRequerFix(BuildTargetGroup.Android);
            }
            if(m_androidSettingsRequierUpdate||m_iosSettingsRequerUpdate)
            {
                EditorApplication.Beep();
                EditorSettingsWindow.Init(m_androidSettingsRequierUpdate, m_iosSettingsRequerUpdate);
            }
            SessionState.SetBool("settingsChecked", true);
        }

        private static bool IsTargetGroupSettingsRequerFix(BuildTargetGroup targetGroup)
        {
            bool apiLevelRequerUpdate = PlayerSettings.GetApiCompatibilityLevel(targetGroup) != ApiCompatibilityLevel.NET_4_6;
            bool il2cppRequerUpdate = PlayerSettings.GetScriptingBackend(targetGroup) != ScriptingImplementation.IL2CPP;
            if (apiLevelRequerUpdate || il2cppRequerUpdate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
