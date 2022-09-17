using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using Hoopsly.GRDP;
using Hoopsly.Editor.Resources;

namespace Hoopsly.Editor
{
    public class GDPR_editorWindow : EditorWindow
    {
        //private string m_prefabPath = "Assets/Hoopsly_SDK/Resources/GDPR_Screen.prefab";
        private GameObject m_sceneInstantiatedPrefab;
        //private GameObject m_openedPrefabRoot;
        private GDPR_screen_handler m_screen_Handler;
        private GameObject m_lastOpenedObject;
        private bool m_wasIn2dBeforeEdit = false;
        private Scene m_prevScene;
        private Scene m_cur_scene;

        bool m_showMesageTextField = false;

        int toolbarInt = 0;
        string[] toolbarStrings = { "Title", "Message", "Links", "Buttons", "Message panel" };
        private static string m_editingpresetAssetPath;
        //[MenuItem("Hoopsly/GDPR screen editor", priority = 1)]
        public static void Init(string presetAssetPath)
        {
            m_editingpresetAssetPath = presetAssetPath;
            var window = (GDPR_editorWindow)EditorWindow.GetWindow(typeof(GDPR_editorWindow), false, "GDPR PRESET EDITOR");
            window.minSize = new Vector2(500, 200);
            window.maxSize = new Vector2(500, 4000);
            window.Show();
        }
        #region Unity_methods
        private void Awake()
        {
            Object prefab = AssetDatabase.LoadAssetAtPath<Object>(m_editingpresetAssetPath); //Resources.Load("GDPR_Screen", typeof(GameObject)) as GameObject;
            if (prefab == null)
            {
                Debug.Log("prefab is NULL");
                return;
            }
            CreateTempEditingScene();
            m_sceneInstantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab, m_prevScene) as GameObject;
            //m_openedPrefabRoot = PrefabUtility.LoadPrefabContents(m_prefabPath);
            m_screen_Handler = m_sceneInstantiatedPrefab.GetComponent<GDPR_screen_handler>();
            SceneVisibilityManager.instance.DisablePicking(m_sceneInstantiatedPrefab, true);
            if (m_sceneInstantiatedPrefab != null)
            {
                var sceneView = SceneView.lastActiveSceneView;
                m_wasIn2dBeforeEdit = sceneView.in2DMode;

                sceneView.in2DMode = true;
                if (sceneView.in2DMode == true && m_sceneInstantiatedPrefab != null)
                {
                    m_lastOpenedObject = Selection.activeGameObject;
                    GameObject focusTarget = m_sceneInstantiatedPrefab.transform.GetChild(1).gameObject;
                    Selection.activeObject = null;
                    Selection.activeObject = focusedWindow;
                    Rect rect = focusTarget.GetComponent<RectTransform>().rect;
                    Bounds newBounds = new Bounds(focusTarget.transform.position, new Vector3(rect.width, rect.height + 150, 100));
                    sceneView.Frame(newBounds, true);
                    Selection.activeObject = null;
                }
            }
            EditorSceneManager.sceneOpening += ChangedActiveScene;
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        private void OnDestroy()
        {
            //Debug.Log("GDPR SETTINGS WINDOW DESTROYED!");
            if (m_sceneInstantiatedPrefab != null)
            {
                WindowClosingActions();
            }
            EditorApplication.playModeStateChanged -= OnPlayModeChanged;
            EditorSceneManager.sceneOpening -= ChangedActiveScene;
        }
        #endregion

        #region UnityEventsSubscribers
        private void ChangedActiveScene(string path, OpenSceneMode mode)
        {
            Debug.Log($"Scene change detected to: {path} with mode: {mode}");
            CloseWindow();
        }

        private void OnPlayModeChanged(PlayModeStateChange playModeState)
        {
            Debug.Log($"Playmode state: {playModeState}");
            if (playModeState == PlayModeStateChange.ExitingEditMode)
            {
                CloseWindow();
            }
        }
        #endregion

        void OnInspectorUpdate()
        {
            // Call Repaint on OnInspectorUpdate as it repaints the windows
            // less times as if it was OnGUI/Update
            SceneView.lastActiveSceneView.camera.Render();
            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
            //SceneView.RepaintAll();
        }

        private void CreateTempEditingScene()
        {
            m_cur_scene = EditorSceneManager.GetActiveScene();
            m_prevScene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
            EditorSceneManager.SetActiveScene(m_prevScene);
            SceneVisibilityManager.instance.Hide(m_cur_scene);
            SceneVisibilityManager.instance.DisablePicking(m_cur_scene);
        }

        private void WindowClosingActions()
        {
            var sceneView = SceneView.lastActiveSceneView;
            if (!m_wasIn2dBeforeEdit)
                sceneView.in2DMode = false;

            if (m_lastOpenedObject != null)
            {
                if (m_lastOpenedObject.TryGetComponent<Renderer>(out Renderer rend))
                {
                    sceneView.Frame(rend.bounds);
                }
                else
                {
                    sceneView.FrameSelected(m_lastOpenedObject, true);
                }
            }

            PrefabUtility.SaveAsPrefabAsset(m_sceneInstantiatedPrefab, m_editingpresetAssetPath); //m_prefabPath);
                                                                                                  //PrefabUtility.UnloadPrefabContents(m_openedPrefabRoot);

            EditorSceneManager.CloseScene(m_prevScene, true);
            if (m_sceneInstantiatedPrefab != null)
            {
                DestroyImmediate(m_sceneInstantiatedPrefab);
            }


            SceneVisibilityManager.instance.Show(m_cur_scene);
            SceneVisibilityManager.instance.EnablePicking(m_cur_scene);
            EditorSceneManager.SetActiveScene(m_cur_scene);
        }

        private void CloseWindow()
        {
            WindowClosingActions();
            this.Close();
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            if (GUILayout.Button("SAVE AND CLOSE", GUILayout.Height(50)))
            {
                CloseWindow();
            }
            GUILayout.EndVertical();
            GUILayout.Space(10);
            toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);
            //SceneView.lastActiveSceneView.camera.Render();

            DrawSettingsGUI();
        }

        private void DrawSettingsGUI()
        {
            if (m_screen_Handler == null) { return; }

            GUILayout.BeginVertical();
            switch (toolbarInt)
            {
                case 0:
                    DrawTitleSettings();
                    GUILayout.Space(15);
                    break;
                case 1:
                    DrawMessageSettings();
                    GUILayout.Space(15);
                    break;
                case 2:
                    DrawLinksSettings();
                    GUILayout.Space(15);
                    break;
                case 3:
                    DrawButtonsSettings();
                    GUILayout.Space(15);
                    break;
                case 4:
                    DrawMessagePanelSettings();
                    break;
                default:
                    break;
            }
            GUILayout.EndVertical();
        }

        private void DrawTitleSettings()
        {
            GUILayout.Label("Screen title settings", EditorResources.Styles.TitleLableStyle);
            using (var v = new EditorGUILayout.VerticalScope("box"))
            {
                GUILayout.Space(5);

                m_screen_Handler.TitleText.text = EditorGUILayout.DelayedTextField(m_screen_Handler.TitleText.text);
                GUILayout.Space(5);
                m_screen_Handler.TitleText.fontSize = EditorGUILayout.IntSlider("Title Font size", m_screen_Handler.TitleText.fontSize, 8, 128);
                GUILayout.Space(5);
                m_screen_Handler.TitleText.font = (Font)EditorGUILayout.ObjectField("Title font", m_screen_Handler.TitleText.font, typeof(Font), false);
                GUILayout.Space(5);
                m_screen_Handler.TitleText.color = EditorGUILayout.ColorField("Title text color", m_screen_Handler.TitleText.color);
                GUILayout.Space(5);

                m_screen_Handler.TitleTextShadow.enabled = EditorGUILayout.ToggleLeft("Enable titleShadow SDK", m_screen_Handler.TitleTextShadow.enabled);
                if (m_screen_Handler.TitleTextShadow.enabled)
                {
                    m_screen_Handler.TitleTextShadow.effectColor = EditorGUILayout.ColorField("Shadow color", m_screen_Handler.TitleTextShadow.effectColor);
                    Vector2 shadowOffset = m_screen_Handler.TitleTextShadow.effectDistance;
                    shadowOffset.x = EditorGUILayout.Slider("Horizontal", shadowOffset.x, -10, 10);
                    shadowOffset.y = EditorGUILayout.Slider("Vertical", shadowOffset.y, -10, 10);
                    m_screen_Handler.TitleTextShadow.effectDistance = shadowOffset;
                }

                GUILayout.Space(5);
            }

        }

        private void DrawMessageSettings()
        {
            GUILayout.Label("Screen Message settings", EditorResources.Styles.TitleLableStyle);
            using (var v = new EditorGUILayout.VerticalScope("box"))
            {
                GUILayout.Space(5);
                m_screen_Handler.MessageText.fontSize = EditorGUILayout.IntSlider("Message Font size", m_screen_Handler.MessageText.fontSize, 8, 128);
                GUILayout.Space(5);
                m_screen_Handler.MessageText.font = (Font)EditorGUILayout.ObjectField("Message font", m_screen_Handler.MessageText.font, typeof(Font), false);
                GUILayout.Space(5);
                m_showMesageTextField = EditorGUILayout.BeginFoldoutHeaderGroup(m_showMesageTextField, "GDPR Message text");
                if (m_showMesageTextField)
                {
                    using (var inter = new EditorGUILayout.VerticalScope(EditorResources.Styles.BoxStyle))
                    {
                        m_screen_Handler.MessageText.text = EditorGUILayout.TextArea(m_screen_Handler.MessageText.text, GUILayout.Height(new GUIStyle().CalcSize(new GUIContent(m_screen_Handler.MessageText.text)).y + 10));
                    }
                }
                GUILayout.Space(5);
                m_screen_Handler.MessageText.color = EditorGUILayout.ColorField("Message text color", m_screen_Handler.MessageText.color);
                EditorGUILayout.EndFoldoutHeaderGroup();
                GUILayout.Space(5);
                m_screen_Handler.MessageTextShadow.enabled = EditorGUILayout.ToggleLeft("Enable message text shadow", m_screen_Handler.MessageTextShadow.enabled);
                if (m_screen_Handler.MessageTextShadow.enabled)
                {
                    m_screen_Handler.MessageTextShadow.effectColor = EditorGUILayout.ColorField("Shadow color", m_screen_Handler.MessageTextShadow.effectColor);
                    Vector2 shadowOffset = m_screen_Handler.MessageTextShadow.effectDistance;
                    shadowOffset.x = EditorGUILayout.Slider("Horizontal", shadowOffset.x, -10, 10);
                    shadowOffset.y = EditorGUILayout.Slider("Vertical", shadowOffset.y, -10, 10);
                    m_screen_Handler.MessageTextShadow.effectDistance = shadowOffset;
                }

                GUILayout.Space(5);
            }
        }

        private void DrawLinksSettings()
        {
            GUILayout.Label("Links settings", EditorResources.Styles.TitleLableStyle);
            using (var v = new EditorGUILayout.VerticalScope("box"))
            {
                GUILayout.Space(5);
                GUILayout.BeginHorizontal();
                GUILayout.Label("Terms of service URL link", EditorResources.Styles.LinkLableStyle, GUILayout.Width(175));
                m_screen_Handler.TermsOfService_URL = EditorGUILayout.DelayedTextField(m_screen_Handler.TermsOfService_URL);
                GUILayout.EndHorizontal();
                GUILayout.Space(5);
                GUILayout.BeginHorizontal();
                GUILayout.Label("Privacy policy URL link", EditorResources.Styles.LinkLableStyle, GUILayout.Width(175));
                m_screen_Handler.PrivacyPolicy_URL = EditorGUILayout.DelayedTextField(m_screen_Handler.PrivacyPolicy_URL);
                GUILayout.EndHorizontal();
                GUILayout.Space(5);
            }
        }

        private bool m_showButtonTextSettings;
        private void DrawButtonsSettings()
        {
            GUILayout.Label("Buttons settings", EditorResources.Styles.TitleLableStyle);
            using (var v = new EditorGUILayout.VerticalScope("box"))
            {
                GUILayout.Space(5);
                m_showButtonTextSettings = EditorGUILayout.BeginFoldoutHeaderGroup(m_showButtonTextSettings, "Button text settings");
                if (m_showButtonTextSettings)
                {
                    using (var inter = new EditorGUILayout.VerticalScope(EditorResources.Styles.BoxStyle))
                    {
                        m_screen_Handler.AceptButtonText.text = EditorGUILayout.DelayedTextField("Button text", m_screen_Handler.AceptButtonText.text);
                        GUILayout.Space(5);
                        m_screen_Handler.AceptButtonText.font = (Font)EditorGUILayout.ObjectField("Button text font", m_screen_Handler.AceptButtonText.font, typeof(Font), false);
                        GUILayout.Space(5);
                        m_screen_Handler.AceptButtonText.fontSize = EditorGUILayout.IntSlider("Button font size", m_screen_Handler.AceptButtonText.fontSize, 8, 128);
                        GUILayout.Space(5);
                        m_screen_Handler.AceptButtonText.color = EditorGUILayout.ColorField("Button text color", m_screen_Handler.AceptButtonText.color);
                    }
                }
                GUILayout.Space(5);
                m_screen_Handler.AceptButton.image.color = EditorGUILayout.ColorField("Button base color", m_screen_Handler.AceptButton.image.color);
                GUILayout.Space(5);
                m_screen_Handler.AcepetButtonShadow.enabled = EditorGUILayout.ToggleLeft("Enable button shadow", m_screen_Handler.AcepetButtonShadow.enabled);
                if (m_screen_Handler.AcepetButtonShadow.enabled)
                {
                    m_screen_Handler.AcepetButtonShadow.effectColor = EditorGUILayout.ColorField("Shadow color", m_screen_Handler.AcepetButtonShadow.effectColor);
                    Vector2 shadowOffset = m_screen_Handler.AcepetButtonShadow.effectDistance;
                    shadowOffset.x = EditorGUILayout.Slider("Horizontal offset", shadowOffset.x, -10, 10);
                    shadowOffset.y = EditorGUILayout.Slider("Vertical offset", shadowOffset.y, -10, 10);
                    m_screen_Handler.AcepetButtonShadow.effectDistance = shadowOffset;
                }
                GUILayout.Space(5);
            }
        }

        private void DrawMessagePanelSettings()
        {
            m_screen_Handler.PanelImage.color = EditorGUILayout.ColorField("Message panel color", m_screen_Handler.PanelImage.color);
            GUILayout.Space(5);
            m_screen_Handler.PanelImage.sprite = (Sprite)EditorGUILayout.ObjectField("Panel sprite", m_screen_Handler.PanelImage.sprite, typeof(Sprite), false);
            GUILayout.Space(5);
            m_screen_Handler.PanelShadow.enabled = EditorGUILayout.ToggleLeft("Enable message panel shadow", m_screen_Handler.PanelShadow.enabled);
            if (m_screen_Handler.PanelShadow.enabled)
            {
                m_screen_Handler.PanelShadow.effectColor = EditorGUILayout.ColorField("Shadow color", m_screen_Handler.PanelShadow.effectColor);
                Vector2 shadowOffset = m_screen_Handler.PanelShadow.effectDistance;
                shadowOffset.x = EditorGUILayout.Slider("Horizontal", shadowOffset.x, -10, 10);
                shadowOffset.y = EditorGUILayout.Slider("Vertical", shadowOffset.y, -10, 10);
                m_screen_Handler.PanelShadow.effectDistance = shadowOffset;
            }
        }
    }
}