using Core.Components._ProgressComponents.Health;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class GamePresent : MonoBehaviour
    {
        [SerializeField] private HealthComponent _healthComponent;

        public UnityEvent OnStartGame;
        public UnityEvent OnLose;
        public bool IsGameOver => _healthComponent.IsDeath;

        /*#region Enable/Disable
        private void OnEnable()
        {
            _healthComponent.OnDeath += Lose;
        }

        private void OnDisable()
        {
            _healthComponent.OnDeath -= Lose;

        }
        #endregion*/
        
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
        private void Lose()
        {
            OnLose.Invoke();
        }
    }
}