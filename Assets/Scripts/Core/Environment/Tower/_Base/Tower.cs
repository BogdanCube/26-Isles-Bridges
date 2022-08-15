using System;
using System.Collections.Generic;
using Base.Level;
using Core.Components._ProgressComponents.Health;
using DG.Tweening;
using UnityEngine;

namespace Core.Environment.Tower
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private Characters.Base.Character _owner;
        [SerializeField] private Island.Island _island;
        [SerializeField] private HealthTower _healthTower;
        [SerializeField] private TowerLevel _towerLevel;
        
        private NoBuilding.NoBuilding _noBuilding;
        public Characters.Base.Character Owner => _owner;
        public IHealthComponent HealthComponent => _healthTower;
        public Island.Island Island => _island;
        public TowerLevel TowerLevel => _towerLevel;
        
        public void Initialization(Characters.Base.Character owner, NoBuilding.NoBuilding noBuilding, Island.Island island)
        {
            transform.localScale = Vector3.zero;
            _owner = owner;
            _noBuilding = noBuilding;
            _island = island;
            Owner.HealthComponent.OnOver += _healthTower.Over;

            transform.DOScale(1, 1f).OnComplete(() =>
            {
                LoaderLevel.Instance.UpdateBake();
                TowerLevel.LoadTower();
            });
        }

        public void ReturnNoBuilding()
        {
            if (_noBuilding)
            {
                _noBuilding.Enable();
            }
        }
    }
}