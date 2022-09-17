using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using Hoopsly.Settings;
public class PreBuildChecks : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0;} }
    public void OnPreprocessBuild(BuildReport report)
    {
        if(EditorUserBuildSettings.buildAppBundle)
        {
            if (HoopslySettings.Instance.GeneralSettings.HoopslyEventsLogLevel != HoopslyLogLevel.Suppress)
                HoopslySettings.Instance.GeneralSettings.HoopslyEventsLogLevel = HoopslyLogLevel.Suppress;

            if (HoopslySettings.Instance.AdjustSettings.AdjustLogLevel != com.adjust.sdk.AdjustLogLevel.Suppress)
                HoopslySettings.Instance.AdjustSettings.AdjustLogLevel = com.adjust.sdk.AdjustLogLevel.Suppress;

            if (HoopslySettings.Instance.GeneralSettings.EnableEngameConsole == true)
                HoopslySettings.Instance.GeneralSettings.EnableEngameConsole = false;

            if (HoopslySettings.Instance.AdjustSettings.AdjustEnviroment == com.adjust.sdk.AdjustEnvironment.Sandbox)
            {
                HoopslySettings.Instance.AdjustSettings.AdjustEnviroment = com.adjust.sdk.AdjustEnvironment.Production;
                PrebuildWarningWindow.Init();
                //throw new BuildFailedException("Andjust in sndbox!");
            }
        }

    }
}
