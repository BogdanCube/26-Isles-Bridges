using System.Collections.Generic;
using Core.Components._ProgressComponents;
using Core.Components._ProgressComponents.Bag;
using Core.Components.DataTowers;
using Core.Components.Wallet;
using Core.Environment.Tower.NoBuilding;
using Core.Environment.Tower.ShopDataTower;
using Core.Environment.Tower.ShopProgressItem;
using NTC.Global.Pool;
using Rhodos.Toolkit.Extensions;
using UnityEngine;

namespace UI.DisplayParameters
{
    public class DisplayProgressTower : MonoBehaviour
    {
        [SerializeField] private ShopProgressComponent _prefab;
        [SerializeField] private Transform _parent;
        private List<ShopProgressComponent> _shopDataTower = new List<ShopProgressComponent>();
        public List<ShopProgressComponent> ShopDataTowers => _shopDataTower;
        
        public void Load(List<ProgressComponent> components,Wallet wallet)
        {
            foreach (var component in components)
            {
                var shop = NightPool.Spawn(_prefab, _parent);
                _shopDataTower.Add(shop);
                shop.Load(component,wallet);
            }
        }

        public void Deload()
        {
            for (int i = 0; i < _parent.childCount; i++)
            {
                _parent.GetChild(i).transform.Deactivate();
            }
        }
    }
}