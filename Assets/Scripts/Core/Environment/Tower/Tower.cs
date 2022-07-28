using System.Collections.Generic;
using Core.Components._ProgressComponents.Health;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private Characters.Base.Character _owner;
        [SerializeField] private HealthTower _healthTower;
        private NoBuilding.NoBuilding _noBuilding;
        public Characters.Base.Character Owner => _owner;
        public IHealthComponent HealthComponent => _healthTower;

        #region Enable/Disable
        private void OnEnable()
        {
            _healthTower.OnDeath += ReturnNoBuilding;
        }

        private void OnDisable()
        {
            _healthTower.OnDeath -= ReturnNoBuilding;
        }
        #endregion

        public void SetOwner(Characters.Base.Character owner, NoBuilding.NoBuilding noBuilding)
        {
            _owner = owner;
            _noBuilding = noBuilding;
        }

        private void ReturnNoBuilding()
        {
            if (_noBuilding)
            {
                _noBuilding.gameObject.SetActive(true);
            }
        }
    }
}