using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PrebuildWarningWindow : EditorWindow
{
    private static PrebuildWarningWindow m_window;
    public static void Init()
    {
        m_window = (PrebuildWarningWindow)PrebuildWarningWindow.GetWindow(typeof(PrebuildWarningWindow), false, "aab build warning");
        m_window.position = new Rect(new Vector2((Screen.width / 2) + (450 / 2), (Screen.height / 2) + (150 / 2)), new Vector2(450, 150));
        m_window.minSize = new Vector2(450, 150);
        m_window.maxSize = m_window.minSize;
        m_window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label($"<b><color=red>WARNING!</color></b>", VersionHeaderLableStyle);
        GUILayout.Space(5);
        GUILayout.Label("You was trying to create aab release build \nwith <b>Adjust environment</b> setted in <b>Sandbox</b> mode!", IssuelineLableStyle);
        //GUILayout.Space(5);
        GUILayout.Label("<b>Adjust environment</b> was automaticly switched to <b>Production</b>!", IssuelineLableStyle);

        GUILayout.Space(20);
        if(GUILayout.Button("OK", GUILayout.Height(50)))
        {
            m_window.Close();
        }
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
                    fontSize = 16,
                    fontStyle = FontStyle.Normal,
                    alignment = TextAnchor.MiddleCenter,
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
                    alignment = TextAnchor.MiddleCenter,
                    richText = true
                };
            }
            return m_issuelineLableStyle;
        }
    }
}
