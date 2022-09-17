using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Hoopsly.Internal;
using Hoopsly.Settings;
using Hoopsly.Editor.Resources;

namespace Hoopsly.Editor
{
    public class HoopslyGeneralEditorWindow
    {
        public void DrawEditor()
        {
            GUILayout.Label("Hoopsly settings", EditorResources.Styles.TitleLableStyle);
            using (var v = new EditorGUILayout.VerticalScope("box"))
            {
                GUILayout.Space(15);
                if (GUILayout.Button("Spawn integration prefab in current scene"))
                {
                    SpawnIntegraionsPrefab();
                }
                GUILayout.Space(5);
                HoopslySettings.Instance.GeneralSettings.StartSdkManually = EditorGUILayout.ToggleLeft(" Start SDK manually", HoopslySettings.Instance.GeneralSettings.StartSdkManually);
                GUILayout.Space(5);
                if(HoopslySettings.Instance.GeneralSettings.StartSdkManually== true)
                {
                    EditorGUILayout.HelpBox("WARNING! With this option on, you must start sdk manually by calling method... \n\nHoopslyManualStart.StartSDK();", MessageType.Warning);
                }
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                GUILayout.Label("Debug settings", EditorResources.Styles.LinkLableStyle);
                using (var inter = new EditorGUILayout.VerticalScope(EditorResources.Styles.BoxStyle))
                {
                    GUILayout.Space(5);
                    GUILayout.BeginHorizontal();
                    GUILayout.Label("Hoopsly SDK log level", GUILayout.Width(250));
                    HoopslySettings.Instance.GeneralSettings.HoopslyEventsLogLevel = (HoopslyLogLevel)EditorGUILayout.EnumPopup(HoopslySettings.Instance.GeneralSettings.HoopslyEventsLogLevel);
                    GUILayout.EndHorizontal();
                    GUILayout.Space(10);
                    HoopslySettings.Instance.GeneralSettings.FPSmeasueIntervals = EditorGUILayout.Slider("FPS measurment intervals (in sec)", HoopslySettings.Instance.GeneralSettings.FPSmeasueIntervals, .1f, 2f);
                    GUILayout.Space(5);
                    HoopslySettings.Instance.GeneralSettings.DebugFpsMeasurment = EditorGUILayout.ToggleLeft("Enable FPS measure debug logs", HoopslySettings.Instance.GeneralSettings.DebugFpsMeasurment);
                }

                DrawConsoleSettings();

                DrawEnumEditor();

                GDPR_Settings_editor();

            }
        }

        private void SpawnIntegraionsPrefab()
        {
            HoopslyLauncher integrations = GameObject.FindObjectOfType<HoopslyLauncher>();
            if (integrations == null)
            {
                GameObject instance = UnityEngine.Resources.Load("Hoopsly_Launcher", typeof(GameObject)) as GameObject;
                var prefab = PrefabUtility.InstantiatePrefab(instance);
                EditorUtility.SetDirty(prefab);
            }
            else
            {
                Debug.Log("Integrations pprfab already in scene");
            }
        }

        private static bool m_gdprCreatorFoldout = false;
        private static bool m_presetListFoldout = false;
        private static bool m_presetListFoldoutLastState = false;
        private static string m_newPresetName = "";
        private static string[] m_presetsArray;
        private const string m_gdprPresetsPath = "Assets/Hoopsly/Resources/GDPR/GDPR_ScreenPresets/";

        private void DrawConsoleSettings()
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("Ingame console", EditorResources.Styles.LinkLableStyle);
            using (var gdprSettings = new EditorGUILayout.VerticalScope(EditorResources.Styles.BoxStyle))
            {
                HoopslySettings.Instance.GeneralSettings.EnableEngameConsole = EditorGUILayout.ToggleLeft(" Enable ingame console", HoopslySettings.Instance.GeneralSettings.EnableEngameConsole);
            }
            //GUILayout.Space(5);
        }

        private void GDPR_Settings_editor()
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("GDPR screen settings", EditorResources.Styles.LinkLableStyle);
            using (var gdprSettings = new EditorGUILayout.VerticalScope(EditorResources.Styles.BoxStyle))
            {
                HoopslySettings.Instance.GeneralSettings.EnableGDPR_Screen = EditorGUILayout.BeginToggleGroup(" Enable GDPR screen", HoopslySettings.Instance.GeneralSettings.EnableGDPR_Screen);
                GUILayout.Space(5);
                HoopslySettings.Instance.GeneralSettings.OverrigeGDPR_Region = EditorGUILayout.ToggleLeft(" Override GDPR region", HoopslySettings.Instance.GeneralSettings.OverrigeGDPR_Region);
                GUILayout.Space(5);
                HoopslySettings.Instance.GeneralSettings.PauseGameOnGDPR = EditorGUILayout.ToggleLeft(" Pause game when GDPR opened", HoopslySettings.Instance.GeneralSettings.PauseGameOnGDPR);
                GUILayout.Space(10);
                m_gdprCreatorFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(m_gdprCreatorFoldout, "Create preset");
                if (m_gdprCreatorFoldout)
                {
                    using (var presetCreate = new EditorGUILayout.VerticalScope(EditorResources.Styles.BoxStyle))
                    {
                        GUILayout.Space(5);
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Label("New preset name", GUILayout.Width(105));
                        GUILayout.Space(5);
                        m_newPresetName = EditorGUILayout.TextField(m_newPresetName);
                        EditorGUI.BeginDisabledGroup(m_newPresetName == "");
                        GUILayout.Space(10);
                        if (GUILayout.Button("Create preset", GUILayout.Width(200)))
                        {
                            GDPR_presetObject preset = ScriptableObject.CreateInstance<GDPR_presetObject>();
                            preset.name = m_newPresetName;
                            AssetDatabase.CreateAsset(preset, $"{m_gdprPresetsPath}{m_newPresetName}.asset");
                            preset.GenerateRelativePrefab();
                            AssetDatabase.SaveAssets();
                            if (m_presetListFoldout == true)
                                m_presetsArray = GetAllAssets();
                        }
                        EditorGUI.EndDisabledGroup();
                        EditorGUILayout.EndHorizontal();
                        GUILayout.Space(5);
                    }
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
                GUILayout.Space(10);
                m_presetListFoldout = EditorGUILayout.BeginFoldoutHeaderGroup(m_presetListFoldout, "Preset list");
                if (m_presetListFoldout != m_presetListFoldoutLastState)
                {
                    if (m_presetListFoldout)
                    {
                        m_presetsArray = GetAllAssets();
                    }
                    m_presetListFoldoutLastState = m_presetListFoldout;
                }
                if (m_presetListFoldout)
                {
                    using (var presetList = new EditorGUILayout.VerticalScope(EditorResources.Styles.BoxStyle))
                    {
                        string defaultPresetPath = Directory.GetFiles(Directory.GetDirectories(m_gdprPresetsPath).First(), "*.asset").First();
                        DrawPresetGUI(defaultPresetPath);
                        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                        foreach (string preset in m_presetsArray)
                        {
                            DrawPresetGUI(preset);
                        }
                    }
                }
                EditorGUILayout.EndToggleGroup();
            }
        }

        private static void DrawPresetGUI(string presetPath)
        {
            bool isSelected = HoopslySettings.Instance.GeneralSettings.ActiveGDPR_Preset.name == Path.GetFileNameWithoutExtension(presetPath) ? true : false;
            bool isDefauld = Path.GetFileNameWithoutExtension(presetPath) == "Default Preset" ? true : false;

            EditorGUILayout.BeginHorizontal(GUILayout.Height(20));
            EditorGUI.BeginDisabledGroup(isSelected);
            if (GUILayout.Button("Activate", GUILayout.Width(60)))
            {
                HoopslySettings.Instance.GeneralSettings.ActiveGDPR_Preset = AssetDatabase.LoadAssetAtPath<GDPR_presetObject>(presetPath);
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.Label(Path.GetFileNameWithoutExtension(presetPath), EditorStyles.centeredGreyMiniLabel, GUILayout.Width(200));
            EditorGUI.BeginDisabledGroup(isDefauld);
            if (GUILayout.Button("Edit preset"))
            {
                string prefabPath = AssetDatabase.GetAssetPath(AssetDatabase.LoadAssetAtPath<GDPR_presetObject>(presetPath).RelativePrefab);
                GDPR_editorWindow.Init(prefabPath);
                //CLOSE WINDOW!!!
            }
            GUILayout.Space(5);
            if (GUILayout.Button(EditorResources.Resources.TrashIcon, GUILayout.Width(30), GUILayout.Height(20)))
            {
                AssetDatabase.LoadAssetAtPath<GDPR_presetObject>(presetPath).DeletePreset();
                m_presetsArray = GetAllAssets();
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
        }

        private bool m_AdRewardTypeUnfoldedState = false;
        private bool m_LastAdRewardTypeUnfoldedState = false;
        private bool m_canRebuildEnum = true;
        private List<AdRewardTypeEditrorCell> m_currentAdRewardTypes = new List<AdRewardTypeEditrorCell>();

        private void DrawEnumEditor()
        {
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            GUILayout.Label("SDK enum settings", EditorResources.Styles.LinkLableStyle);
            using (var SdkEnumSettings = new EditorGUILayout.VerticalScope(EditorResources.Styles.BoxStyle))
            {
                m_AdRewardTypeUnfoldedState = EditorGUILayout.BeginFoldoutHeaderGroup(m_AdRewardTypeUnfoldedState, "Ad Reward Types");
                if(m_LastAdRewardTypeUnfoldedState==false && m_AdRewardTypeUnfoldedState == true)
                {
                    m_LastAdRewardTypeUnfoldedState = true;
                    LoadAdRewardTypeData();
                }
                if(m_LastAdRewardTypeUnfoldedState == true && m_AdRewardTypeUnfoldedState == false)
                {
                    m_LastAdRewardTypeUnfoldedState = false;
                }

                if (m_AdRewardTypeUnfoldedState)
                {
                    using (var AdRewardTypeSettingsBox = new EditorGUILayout.VerticalScope(EditorResources.Styles.BoxStyle))
                    {
                        GUILayout.Space(5);

                        foreach (AdRewardTypeEditrorCell cell in m_currentAdRewardTypes.ToList())
                        {
                            DrawCellGui(cell);
                        }
                        GUILayout.Space(5);
                        if (GUILayout.Button(EditorResources.Resources.PlusIcon, GUILayout.Height(20)))
                        {
                            m_currentAdRewardTypes.Add(new AdRewardTypeEditrorCell());
                        }
                        GUILayout.Space(7.5f);
                        m_canRebuildEnum = ValidateAdList();
                        EditorGUI.BeginDisabledGroup(!m_canRebuildEnum);
                        if (GUILayout.Button("REBUILD ENUM", GUILayout.Height(30)))
                        {
                            ArRewardEnumGenerator.BuildEnum(m_currentAdRewardTypes.ToArray());
                            m_AdRewardTypeUnfoldedState = false;
                            AssetDatabase.Refresh();

                        }
                        EditorGUI.EndDisabledGroup();
                        if (!m_canRebuildEnum)
                            EditorGUILayout.HelpBox("New enum field can't be EMPTY or start with digit!", MessageType.Error);
                    }
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
            }
        }

        private bool ValidateAdList()
        {
            bool invalidDetected = false;
            foreach (AdRewardTypeEditrorCell cell in m_currentAdRewardTypes)
            {
                if(!cell.m_isDefault)
                {
                    if (!IsStringValid(cell.m_value))
                    {
                        invalidDetected = true;
                    }
                }
            }
            if (invalidDetected == false)
                return true;
            else
                return false;
        }

        private void DrawCellGui(AdRewardTypeEditrorCell editrorCell)
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Height(20));
            EditorGUI.BeginDisabledGroup(editrorCell.m_isDefault);

            if(editrorCell.m_isDefault)
            {
                GUILayout.Label(editrorCell.m_value, EditorStyles.whiteLabel);
                GUILayout.Label("(default)", EditorStyles.whiteLabel, GUILayout.Width(50));
            }
            else
            {

                editrorCell.m_value = GUILayout.TextField(editrorCell.m_value);
                
            }
            //m_canRebuildEnum = IsStringValid(editrorCell.m_value);
            if (GUILayout.Button(EditorResources.Resources.TrashIcon, GUILayout.Width(30), GUILayout.Height(20)))
            {
                m_currentAdRewardTypes.Remove(editrorCell);
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndHorizontal();
            Rect rect = EditorGUILayout.GetControlRect(false, 1);
            EditorGUI.DrawRect(rect, new Color(0.40f, 0.40f, 0.40f, 1));
        }

        private bool IsStringValid(string inputString)
        {
            bool returVal = true;

            if (String.IsNullOrEmpty(inputString))
            {
                returVal = false;
            }
            else if (char.IsDigit(inputString.First()))
            {
                returVal = false;
            }
            else if (inputString.All(char.IsDigit))
            {
                returVal = false;
            }
            else if(HoopslySettings.Instance.GeneralSettings.DefaultAdRewardTypes.Contains(inputString))
            {
                returVal = false;
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(inputString, "^[a-zA-Z0-9. -_?]*$"))
            {
                returVal = false;
            }
            return returVal;
        }

        private void LoadAdRewardTypeData()
        {
            string[] dataInCrrent = ArRewardEnumGenerator.GetCurrentAdRewardTypesFromEnum();
            m_currentAdRewardTypes.Clear();
            for (int i = 0; i < dataInCrrent.Length; i++)
            {
                if (HoopslySettings.Instance.GeneralSettings.DefaultAdRewardTypes.Contains(dataInCrrent[i]))
                {
                    m_currentAdRewardTypes.Add(new AdRewardTypeEditrorCell() { m_isDefault = true, m_value = dataInCrrent[i] });
                }
                else
                {
                    m_currentAdRewardTypes.Add(new AdRewardTypeEditrorCell() { m_isDefault = false, m_value = dataInCrrent[i] });
                }
            }
        }

        private static string[] GetAllAssets()
        {
            return Directory.GetFiles(m_gdprPresetsPath, "*.asset");
        }
    }
    public class AdRewardTypeEditrorCell
    {
        public string m_value;
        public bool m_isDefault = false;
    }
}
