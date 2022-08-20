using System;
using System.Collections.Generic;
using System.Linq;
using Core.Character.Player;
using Core.Characters.Enemy;
using Core.Characters.Player;
using Core.Components._ProgressComponents.Health;
using Core.Environment.Tower;
using JetBrains.Annotations;
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
        
        [Header("Debug")] 
        [SerializeField] private bool _isAutoLoad;
        public Player Player => _player;
        public Enemy Enemy => _enemy;
        public Tower PlayerTower => _playerTower;
        public Tower EnemyTower => _enemyTower;

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
            _playerTower?.Level.LoadTower();
            _enemyTower.Level.LoadTower();
        }
    }
}