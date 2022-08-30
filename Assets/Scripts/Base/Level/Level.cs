using Core.Characters.Enemy;
using Core.Characters.Player;
using Core.Environment.Tower;
using Toolkit.Extensions;
using UnityEngine;

namespace Base.Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Tower _playerTower;
        [SerializeField] private Tower _enemyTower;
        [SerializeField] private Player _player;
        [SerializeField] private Enemy _enemy;
        private bool _isAutoLoad = true;
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
            _isAutoLoad = false;
            _enemy.Deactivate();
        }

        public void StartLevel()
        {
            _enemy.Activate();
            _playerTower?.Level.LoadTower();
            _enemyTower.Level.LoadTower();
        }
    }
}