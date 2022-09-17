using System.Collections.Generic;
using Core.Components._ProgressComponents;
using Core.Components.Wallet;
using Core.Environment.Tower.ShopProgressItem;
using NTC.Global.Pool;
using Toolkit.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace UI.DisplayParameters
{
    public class DisplayProgressTower : MonoBehaviour
    {
        [SerializeField] private ShopProgressComponent _prefab;
        [SerializeField] private Transform _parent;
        private List<ShopProgressComponent> _shopDataTower = new List<ShopProgressComponent>();
        public List<ShopProgressComponent> ShopDataTowers => _shopDataTower;
        public UnityEvent OnBought;

        public void Load(List<ProgressComponent> components,Wallet wallet)
        {
            foreach (var component in components)
            {
                var shop = NightPool.Spawn(_prefab, _parent);
                _shopDataTower.Add(shop);
                shop.Load(component,wallet,OnBought);
            }
        }

        public void Deload()
        {
            for (int i = 0; i < _parent.childCount; i++)
            {
                _parent.GetChild(i).Deactivate();
            }
        }
    }
}