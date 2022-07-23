using System.Collections.Generic;
using UnityEngine;

namespace Core.Components._ProgressComponents
{
    [CreateAssetMenu(fileName = "New Progress Data", menuName = "My Assets/Components/ProgressData", order = 1)]
    public class ComponentData : ScriptableObject
    {
        public List<TemplateData> Template;
    }

    [System.Serializable]
    public class TemplateData
    {
        public int MaxCount;
        public int Price;
    }
}