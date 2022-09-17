using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Hoopsly.Settings;
using Hoopsly.Editor.Resources;

namespace Hoopsly.Editor
{

    public class HoopslyEditorWindow : EditorWindow
    {
        private HoopslyGeneralEditorWindow m_hoopslyGeneralEditor;
        private HoopslyGeneralEditorWindow HoopslyGeneralEditorWindow
        {
            get
            {
                if (m_hoopslyGeneralEditor == null) { m_hoopslyGeneralEditor = new HoopslyGeneralEditorWindow(); }
                return m_hoopslyGeneralEditor;
            }
        }

        private HoopslyApplovinEditor m_hoopslyApplovinEditor;
        private HoopslyApplovinEditor HoopslyApplovinEditor
        {
            get
            {
                if (m_hoopslyApplovinEditor == null) { m_hoopslyApplovinEditor = new HoopslyApplovinEditor(); }
                return m_hoopslyApplovinEditor;
            }
        }
        private HoopslyAdjustEditorWindow m_hoopslyAdjustEditor;
        private HoopslyAdjustEditorWindow HoopslyAdjustEditorWindow
        {
            get
            {
                if (m_hoopslyAdjustEditor == null) { m_hoopslyAdjustEditor = new HoopslyAdjustEditorWindow(); }
                return m_hoopslyAdjustEditor;
            }
        }

        private HoopslyFacebookEditor m_hoopslyFacebookEditor;
        private HoopslyFacebookEditor HoopslyFacebookEditor
        {
            get
            {
                if (m_hoopslyFacebookEditor == null) { m_hoopslyFacebookEditor = new HoopslyFacebookEditor(); }
                return m_hoopslyFacebookEditor;
            }
        }

        Vector2 scrollPos;
        private int m_uiModeID = 0;
        private string[] m_uiModeNames = { "Hoopsly SDK", "Adjust", "Applovin", "Facebook" };

        [MenuItem("Hoopsly/Hoopsly settings")]
        public static void Init()
        {
            HoopslyEditorWindow window = (HoopslyEditorWindow)EditorWindow.GetWindow(typeof(HoopslyEditorWindow), false, "Hoopsly settings");
            window.minSize = new Vector2(600, 500);
            window.maxSize = new Vector2(600, 4000);
            window.Show();
        }

        private void Awake()
        {

        }

        private void OnDestroy()
        {
            //Save assets
        }

        private void OnGUI()
        {
            if (HoopslySettings.Instance != null)
            {
                scrollPos = GUILayout.BeginScrollView(scrollPos);
                EditorGUI.BeginChangeCheck();

                GUILayout.Space(10);
                DrawVersionInfo();
                GUILayout.Space(10);
                DrawSelectedTab();

                if (EditorGUI.EndChangeCheck())
                {
                    HoopslySettings.Instance.SaveSettingsAsync();
                }

                GUILayout.EndScrollView();
            }
        }

        private void DrawVersionInfo()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("HOOPSLY SDK", EditorResources.Styles.TitleLableStyle);
            GUILayout.Label(HoopslySettings.Instance.GeneralSettings.SdkVersion, EditorResources.Styles.VersionHeaderLableStyle);
            GUILayout.EndHorizontal();
            GUILayout.Space(15);
        }

        private void DrawSelectedTab()
        {
            m_uiModeID = GUILayout.Toolbar(m_uiModeID, m_uiModeNames, GUILayout.Height(30));
            GUILayout.Space(5);
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Space(5);
            switch (m_uiModeID)
            {
                case 0:
                    HoopslyGeneralEditorWindow.DrawEditor();
                    break;
                case 1:
                    HoopslyAdjustEditorWindow.DrawEditor();
                    break;
                case 2:
                    HoopslyApplovinEditor.DrawEditor();
                    break;
                case 3:
                    HoopslyFacebookEditor.DrawEditor();
                    break;
                default:
                    break;
            }
        }
    }
}
