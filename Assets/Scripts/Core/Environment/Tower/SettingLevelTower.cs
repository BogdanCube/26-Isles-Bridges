using System.Collections.Generic;
using UnityEngine;

namespace Core.Environment.Tower
{
    [CreateAssetMenu(fileName = "SettingLevelTower", menuName = "My Assets/Components/Tower", order = 0)]
    public class SettingLevelTower : ScriptableObject
    {
        public List<TemplateTower> Templates;
    }
    [System.Serializable]
    public class TemplateTower
    {
        public Transform Model;
        public int TimeSpawn;
        public int Price;
    }
}