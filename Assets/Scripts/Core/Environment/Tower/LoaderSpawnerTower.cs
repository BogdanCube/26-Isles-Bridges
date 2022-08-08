using Base.Level;
using Core.Components._Spawners;
using Core.Environment.Tower._Base;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class LoaderSpawnerTower : LoaderTower
    {
        [Expandable] [SerializeField] private SettingLevelTower _setting;
        [SerializeField] private SpawnerTower _spawner;
        protected override Transform TowerModel(int index) => _setting.Templates[index].Model;
        public override int MaxLevel => _setting.Templates.Count;
        public override int PriceNextLevel(int level) => _setting.Templates[level].Price;
        
        public override void Load(int index)
        {
            base.Load(index);
            _spawner.StartSpawn(_setting.Templates[index].TimeSpawn, _tower);
        }
    }
}