using System.Collections.Generic;
using Core.Components._ProgressComponents.Health;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private Characters.Base.Character _owner;
        [SerializeField] private Island.Island _island;
        [SerializeField] private HealthTower _healthTower;
        [SerializeField] private ShopTower _shopTower;

        private NoBuilding.NoBuilding _noBuilding;
        public Characters.Base.Character Owner => _owner;
        public IHealthComponent HealthComponent => _healthTower;
        public ShopTower ShopTower => _shopTower;
        public Island.Island Island => _island;
        
        public void SetOwner(Characters.Base.Character owner, NoBuilding.NoBuilding noBuilding, Island.Island island)
        {
            _owner = owner;
            _noBuilding = noBuilding;
            _island = island;
        }

        public void ReturnNoBuilding()
        {
            if (_noBuilding)
            {
                _noBuilding.gameObject.SetActive(true);
            }
        }
    }
}