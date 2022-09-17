using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


public class GDPR_presetObject : ScriptableObject
{
    [SerializeField] private GameObject m_prefab;
    public GameObject RelativePrefab
    {
        get
        {
            return m_prefab;
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Debug asset path")]
    public void DebugAssetPath()
    {
        string path = AssetDatabase.GetAssetPath(this);
        string folder = Path.GetDirectoryName(path);
        Debug.Log(path);
        Debug.Log(folder);
    }
    
    public void GenerateRelativePrefab()
    {
        GameObject sourcePrefab = PrefabUtility.LoadPrefabContents("Assets/Hoopsly/Resources/GDPR/GDPR_ScreenSource/GDPR_Screen.prefab");
        string path = AssetDatabase.GetAssetPath(this);
        string folder = Path.GetDirectoryName(path);
        string fileName = Path.GetFileNameWithoutExtension(path);
        m_prefab = PrefabUtility.SaveAsPrefabAsset(sourcePrefab, $"{folder}/{fileName}.prefab");
        EditorUtility.SetDirty(this);
    }

    public void DeletePreset()
    {
        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(m_prefab));
        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(this));
    }
    [ContextMenu("Find prefab")]
    private void FindPrefabAsset()
    {
        string fullPath = AssetDatabase.GetAssetPath(this);
        string presetDirectory = Path.GetDirectoryName(fullPath);
        m_prefab = AssetDatabase.LoadAssetAtPath<GameObject>($"{presetDirectory}/{Path.GetFileNameWithoutExtension(fullPath)}.prefab");
    }
#endif

}
