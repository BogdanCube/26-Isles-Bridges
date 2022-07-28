using System.Collections.Generic;
using Core.Environment.Tower;
using UnityEngine;

namespace Core.Components.DataTowers
{
    [CreateAssetMenu(fileName = "New Tower Data", menuName = "My Assets/Components/TowerData", order = 1)]
    public class TowerData : ScriptableObject
    {
        public List<TemplateTower> Templates;
    }
    [System.Serializable]
    public class TemplateTower
    {
        public Tower Tower;
        public Sprite Icon;
        public int Price;
    }
}