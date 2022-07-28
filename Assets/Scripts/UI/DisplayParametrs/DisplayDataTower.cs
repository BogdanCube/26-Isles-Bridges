using Core.Components._ProgressComponents.Bag;
using Core.Components.DataTowers;
using Core.Environment.Tower.NoBuilding;
using Core.Environment.Tower.ShopDataTower;
using NTC.Global.Pool;
using UnityEngine;

namespace UI.DisplayParametrs
{
    public class DisplayDataTower : MonoBehaviour
    {
        [SerializeField] private ShopDataTower _prefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private NoBuilding _noBuilding;
        public void Load(TowerData towerData,BagCharacter bag)
        {
            Deload();
            foreach (var template in towerData.Templates)
            {
                var shop = NightPool.Spawn(_prefab, _parent);
                shop.Load(template,_noBuilding,bag);
            }
        }

        public void Deload()
        {
            for (int i = 0; i < _parent.childCount; i++)
            {
                NightPool.Despawn(_parent.GetChild(i).transform);
            }
        }
    }
}