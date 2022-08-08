using System.Collections.Generic;
using UnityEngine;

namespace Core.Environment.Tower
{
    [CreateAssetMenu(fileName = "SettingDefTower", menuName = "My Assets/Components/SettingDefTower", order = 0)]

    public class SettingDefTower : ScriptableObject
    {
        public List<TemplateDefTower> Templates;
    }
    [System.Serializable]
    public class TemplateDefTower
    {
        public Transform Model;
        public float Speed;
        public int Damage;
        public int Price;
    }
}