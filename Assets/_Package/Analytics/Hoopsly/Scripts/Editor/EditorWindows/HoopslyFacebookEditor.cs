using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Facebook.Unity.Settings;
using Hoopsly.Settings;
using Hoopsly.Editor.Resources;

namespace Hoopsly.Editor
{
    public class HoopslyFacebookEditor
    {
        public HoopslyFacebookEditor()
        {
            m_facebookAppId = Facebook.Unity.Settings.FacebookSettings.AppId;
            m_facebookClientToken = Facebook.Unity.Settings.FacebookSettings.ClientToken;
            m_facebookAppName = Facebook.Unity.Settings.FacebookSettings.AppLabels[0];
            m_facebookAndroidKeystore = Facebook.Unity.Settings.FacebookSettings.AndroidKeystorePath;
            m_facebookIosURLSuffix = Facebook.Unity.Settings.FacebookSettings.IosURLSuffix;
        }

        public void DrawEditor()
        {
            DrawFacebookSettings();
        }

        private FacebookSettings m_facebookSettings;
        private FacebookSettings Facebook_Settings
        {
            get
            {
                if (m_facebookSettings == null)
                {
                    m_facebookSettings = (FacebookSettings)UnityEngine.Resources.Load("FacebookSettings");
                }
                return m_facebookSettings;
            }
        }

        private string m_facebookAppName;
        private string m_facebookAppId;
        private string m_facebookClientToken;
        private string m_facebookAndroidKeystore;
        private string m_facebookIosURLSuffix;

        private bool m_facebookAndroidSettings = false;
        private bool m_facebookIOSsettinghs = false;
        private void DrawFacebookSettings()
        {
            GUILayout.Label("Facebook settings", EditorResources.Styles.TitleLableStyle);
            using (var v = new EditorGUILayout.VerticalScope("box"))
            {
                GUILayout.Space(10);
                HoopslySettings.Instance.GeneralSettings.UseFacebook = EditorGUILayout.ToggleLeft("Enable Facebook SDK", HoopslySettings.Instance.GeneralSettings.UseFacebook);
                if (HoopslySettings.Instance.GeneralSettings.UseFacebook)
                {

                    EditorGUI.BeginChangeCheck();
                    m_facebookAppName = EditorGUILayout.TextField("App name (Optional)", m_facebookAppName);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Facebook.Unity.Settings.FacebookSettings.AppLabels = new List<string>() { m_facebookAppName };
                        EditorUtility.SetDirty(Facebook_Settings);
                    }
                    GUILayout.Space(5);
                    EditorGUI.BeginChangeCheck();
                    m_facebookAppId = EditorGUILayout.TextField("App ID", m_facebookAppId);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Facebook.Unity.Settings.FacebookSettings.AppIds = new List<string>() { m_facebookAppId };
                        EditorUtility.SetDirty(Facebook_Settings);
                    }
                    GUILayout.Space(5);
                    EditorGUI.BeginChangeCheck();
                    m_facebookClientToken = EditorGUILayout.TextField("Client Token (Optional)", m_facebookClientToken);
                    if (EditorGUI.EndChangeCheck())
                    {
                        Facebook.Unity.Settings.FacebookSettings.ClientTokens = new List<string>() { m_facebookClientToken };
                        EditorUtility.SetDirty(Facebook_Settings);
                    }

                    GUILayout.Space(5);
                    m_facebookAndroidSettings = EditorGUILayout.BeginFoldoutHeaderGroup(m_facebookAndroidSettings, "Android Settings");
                    if (m_facebookAndroidSettings)
                    {
                        EditorGUI.BeginChangeCheck();
                        m_facebookAndroidKeystore = EditorGUILayout.TextField("Android keystore path", m_facebookAndroidKeystore);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Facebook.Unity.Settings.FacebookSettings.AndroidKeystorePath = m_facebookAndroidKeystore;
                            EditorUtility.SetDirty(Facebook_Settings);
                        }
                    }
                    EditorGUILayout.EndFoldoutHeaderGroup();
                    GUILayout.Space(5);
                    m_facebookIOSsettinghs = EditorGUILayout.BeginFoldoutHeaderGroup(m_facebookIOSsettinghs, "iOS Settings");
                    if (m_facebookIOSsettinghs)
                    {
                        EditorGUI.BeginChangeCheck();
                        m_facebookIosURLSuffix = EditorGUILayout.TextField("iOS URL Scheme Suffix", m_facebookIosURLSuffix);
                        if (EditorGUI.EndChangeCheck())
                        {
                            Facebook.Unity.Settings.FacebookSettings.IosURLSuffix = m_facebookIosURLSuffix;
                            EditorUtility.SetDirty(Facebook_Settings);
                        }
                    }
                    EditorGUILayout.EndFoldoutHeaderGroup();
                    GUILayout.Space(5);

                    if (GUILayout.Button("Regenerate android manifest"))
                    {
                        Facebook.Unity.Editor.ManifestMod.GenerateManifest();
                        EditorUtility.SetDirty(Facebook_Settings);
                    }

                    GUILayout.Space(20);

                    if (GUILayout.Button("Open facebook settings"))
                    {
                        EditorGUIUtility.PingObject(Facebook_Settings);
                        Selection.activeObject = Facebook_Settings;
                    }
                }
                GUILayout.Space(10);
            }
        }
    }
}
