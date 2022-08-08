using Core.Components;
using Core.Components.Weapon.Bow;
using Core.Environment.Tower._Base;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Environment.Tower.Def_Tower
{
    public class LoaderDefTower : LoaderTower
    {
        [Expandable] [SerializeField] private SettingDefTower _setting;
        [SerializeField] private Bow _bow;
        [SerializeField] private DetectorFighting _detector;
        [SerializeField] private float _coefficient = 0.4f;
        private float Radius => _tower.Island.Radius * _coefficient;
        public override int MaxLevel => _setting.Templates.Count;
        public override int PriceNextLevel(int level) => _setting.Templates[level].Price;
        protected override Transform TowerModel(int index) => _setting.Templates[index].Model;

        public override void Load(int index)
        {
            base.Load(index);
            _bow.Load(_setting.Templates[index]);
            _detector.SetRadius(Radius);
        }
    }
}