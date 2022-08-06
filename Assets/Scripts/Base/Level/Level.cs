using System;
using System.Collections.Generic;
using System.Linq;
using Core.Character.Player;
using Core.Characters.Enemy;
using Core.Characters.Player;
using Core.Environment.Tower;
using Rhodos.Toolkit.Extensions;
using UnityEngine;

namespace Base.Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Tower _playerTower;
        [SerializeField] private Tower _enemyTower;
        [SerializeField] private Player _player;
        [SerializeField] private Enemy _enemy;
        private List<Transform> _objects = new();
        
        [Header("Debug")] 
        [SerializeField] private bool _isAutoLoad;
        public Action DeathPlayerTower
        {
            get => _playerTower.HealthComponent.OnDeath;
            set => _playerTower.HealthComponent.OnDeath = value;
        }
        public Action DeathEnemyTower
        {
            get => _enemyTower.HealthComponent.OnDeath;
            set => _enemyTower.HealthComponent.OnDeath = value;
        }
        public Player Player => _player;
        public Enemy Enemy => _enemy;

        private void Start()
        {
            if (_isAutoLoad)
            {
                StartLevel();
            }
        }

        public void Load()
        {
            _enemy.transform.Deactivate();
        }

        public void StartLevel()
        {
            _enemy.transform.Activate();
            _playerTower.TowerLevel.LoadTower();
            _enemyTower.TowerLevel.LoadTower();
        }
    }
}