using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HoopslyMenuItems
{
    [MenuItem("Hoopsly/Editor/Reset Consent PlayerPrefs")]
    private static void ClearConsentPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("cStatus"))
        {
            PlayerPrefs.DeleteKey("cStatus");
        }
        Debug.Log("Consent status cleared");
    }

    private static readonly string[] m_prefKeys =  { "rewardCount", "interCount", "levelCount" };
    [MenuItem("Hoopsly/Editor/Clear PlayerPrefs")]
    private static void ClearPlayerPrefs()
    {
        foreach (string prefKey in m_prefKeys)
        {
            if (PlayerPrefs.HasKey(prefKey))
            {
                PlayerPrefs.DeleteKey(prefKey);
            }
        }
        Debug.Log("Player prefs cleared");
    }
}
