using Base.Level;
using Core.Components._Spawners;
using Managers.Level;
using NaughtyAttributes;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class LoaderTower : MonoBehaviour
    {
        [Expandable] [SerializeField] private SettingLevelTower _setting;
        [SerializeField] private Transform _currentTower;
        [SerializeField] private Transform _modelParent;
        [SerializeField] private SpawnerTower _spawner;
        public int MaxLevel => _setting.Templates.Count;
        public int PriceNextLevel(int level) => _setting.Templates[level].Price;
        public void Load(int index)
        {
            Destroy(_currentTower.gameObject);
            var currentTemplate = _setting.Templates[index];
            var tower = currentTemplate.Model;
            _currentTower = Instantiate(tower, _modelParent);
            LoaderLevel.Instance.UpdateBake();
            
            _spawner.StartSpawn(currentTemplate.TimeSpawn);
        }
    }
}