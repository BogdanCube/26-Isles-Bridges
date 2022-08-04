using System;
using System.Collections.Generic;
using System.Linq;
using Core.Character.Player;
using Core.Characters.Enemy;
using Core.Environment.Tower;
using Rhodos.Toolkit.Extensions;
using UnityEngine;

namespace Base.Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private HealthTower _playerTower;
        [SerializeField] private HealthTower _enemyTower;
        [SerializeField] private Player _player;
        [SerializeField] private Enemy _enemy;
        private List<Transform> _objects = new();
        public Action DeathPlayerTower
        {
            get => _playerTower.OnDeath;
            set => _playerTower.OnDeath = value;
        }
        public Action DeathEnemyTower
        {
            get => _enemyTower.OnDeath;
            set => _enemyTower.OnDeath = value;
        }
        public Player Player => _player;
        public Enemy Enemy => _enemy;

        public void Load()
        {
            _enemy.transform.Deactivate();
        }

        public void StartLevel()
        {
            _enemy.transform.Activate();
        }
    }
}