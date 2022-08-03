using Core.Components._ProgressComponents.Health;
using Core.Environment.Tower;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class GamePresent : MonoBehaviour
    {
        [SerializeField] private HealthTower _playerTower;
        [SerializeField] private HealthTower _enemyTower;

        public UnityEvent OnStartGame;
        public UnityEvent OnWin;
        public UnityEvent OnLose;
        public bool IsGameOver => _playerTower.IsDeath;

        #region Enable/Disable
        private void OnEnable()
        {
            _playerTower.OnDeath += Lose;
            _enemyTower.OnDeath += Win;
        }

        private void OnDisable()
        {
            _playerTower.OnDeath -= Lose;
            _enemyTower.OnDeath -= Win;
        }
        #endregion
        
        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            OnStartGame.Invoke();
        }
        // Pause
        private void Restart()
        {
            
        }
        private void Win()
        {
            OnWin.Invoke();
        }
        private void Lose()
        {
            OnLose.Invoke();
        }
    }
}