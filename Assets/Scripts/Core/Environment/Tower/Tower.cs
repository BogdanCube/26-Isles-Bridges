using System.Collections.Generic;
using Core.Components._ProgressComponents.Health;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private Characters.Base.Character _owner;
        
        [SerializeReference]
        public HealthTower _HealthTower;

        public Characters.Base.Character Owner => _owner;
        public IHealthComponent HealthComponent => _HealthTower;

        public void SetOwner(Characters.Base.Character owner)
        {
            _owner = owner;
        }
    }
}