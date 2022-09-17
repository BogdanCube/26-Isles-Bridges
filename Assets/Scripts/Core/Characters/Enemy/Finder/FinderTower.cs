using System.Collections.Generic;
using System.Linq;
using Core.Environment.Tower._Base;
using Toolkit.Extensions;
using UnityEngine;

namespace Core.Characters.Enemy.Finder
{
    public class FinderTower : MonoBehaviour
    {
        private List<Tower> _towers = new List<Tower>();
        public bool IsTower => _towers.Count(tower => tower.Level.IsMaxLevel == false && tower.IsActive()) > 0;
        public bool IsMaxTower => IsTower && _towers[0].Level.IsMaxLevel;

        public bool IsFullMax => 
            _towers.Count(tower => tower.Level.IsMaxLevel && tower.IsActive()) == _towers.Count(tower => tower.IsActive());
        public Transform Tower => IsTower
            ? _towers.Where(tower => tower.Level.IsMaxLevel == false && tower.IsActive()).ToList()[0].BaseDetectorBag
            : _towers[0].BaseDetectorBag;
        
        
        private List<Tower> _playerTowers = new List<Tower>();
        public bool IsTowerPlayer => 
            _playerTowers.Count(tower => tower.Level.IsMaxLevel == false && tower.IsActive()) > 0;

        public Transform PlayerTower =>
            _playerTowers.Where(tower => tower.Level.IsMaxLevel == false && tower.IsActive()).ToList()[0].transform;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Tower tower))
            {
                if (tower.Owner is Player.Player)
                {
                    if (tower.HealthComponent.IsDeath == false)
                    {
                        if (_playerTowers.Contains(tower) == false)
                        {
                            if (tower.Level.IsMaxLevel == false)
                            {
                                _playerTowers.Add(tower);
                            }
                        }
                    }
                }
                else if (tower.Owner is Enemy && tower.HealthComponent.IsDeath == false)
                {
                    if (_towers.Contains(tower) == false)
                    {
                        if (tower.Level.IsMaxLevel == false)
                        {
                            _towers.Add(tower);
                        }
                    }
                }
            }
        }
    }
}