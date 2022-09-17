using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.Networking;
using Hoopsly.Settings;
namespace Hoopsly.Editor
{
    [InitializeOnLoad]
    public class VersionChecker
    {
        private static string m_remoteVer;
        private static string m_adress = "https://pastebin.com/raw/jq245Vq4";

        private static int[] currentVerInt;
        private static int[] remoteVerInt;

        static VersionChecker()
        {
            EditorApplication.update += VersionCehck;
        }

        private static void VersionCehck()
        {

            if (!SessionState.GetBool("versionChecked", false))
            {
                GetLatestVersion();
            }
            EditorApplication.update -= VersionCehck;
        }

        static async void GetLatestVersion()
        {
            using UnityWebRequest webRequest = UnityWebRequest.Get(m_adress);
            var operation = webRequest.SendWebRequest();
            while (!operation.isDone)
                await System.Threading.Tasks.Task.Yield();
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                m_remoteVer = webRequest.downloadHandler.text;
                CompareVersion();
            }
            else
            {
                Debug.LogWarning($"Failed to get SDK version: {webRequest.error}");
            }
            SessionState.SetBool("versionChecked", true);
        }

        private static void CompareVersion()
        {
            var localReleseData = ParseVersionString(HoopslySettings.Instance.GeneralSettings.SdkVersion);
            var remoteReleseData = ParseVersionString(m_remoteVer);
            int compareResult = localReleseData.m_ver.CompareTo(remoteReleseData.m_ver);
            if (compareResult < 0)
            {
                Debug.LogWarning("YOUR VERSION IS OUTDATED!");
                VersionWarningWindow.Init(m_remoteVer, localReleseData.m_releseType == ReleseType.Beta);
            }
            else if (compareResult > 0)
            {
                //Debug.LogWarning("YOUR VERSION IS HIGHER THEN REMOTE!");
                //Local version higher
            }
            else if (compareResult == 0)
            {
                //Debug.LogWarning("YOUR VERSION IS EQUAL TO REMOTE!");
                //Both equal
            }

        }

        private static ReleseData ParseVersionString(string ver)
        {
            ReleseType releseType;
            if (ver.Contains('f'))
                releseType = ReleseType.Final;
            else if (ver.Contains('b'))
                releseType = ReleseType.Beta;
            else
                releseType = ReleseType.Beta;

            List<string> verNumbers = ver.Split('.').ToList();
            string last = verNumbers[verNumbers.Count - 1];
            verNumbers.RemoveAt(verNumbers.Count - 1);
            switch (releseType)
            {
                case ReleseType.Final:
                    verNumbers.AddRange(last.Split('f'));
                    break;
                case ReleseType.Beta:
                    verNumbers.AddRange(last.Split('b'));
                    break;
            }
            return new ReleseData(releseType, verNumbers.ToArray(), ver);
        }


    }
    struct ReleseData
    {
        public ReleseType m_releseType { get; private set; }
        public Version m_ver { get; private set; }
        public string m_verSingleString { get; private set; }
        public ReleseData(ReleseType releseType, string[] verNumbers, string singleString)
        {
            m_releseType = releseType;
            m_ver = new Version(Convert.ToInt32(verNumbers[0]), Convert.ToInt32(verNumbers[1]), Convert.ToInt32(verNumbers[2]), Convert.ToInt32(verNumbers[3]));
            m_verSingleString = singleString;
        }
    }
    enum ReleseType { Final, Beta };
}
