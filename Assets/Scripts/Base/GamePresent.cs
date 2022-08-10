using System.Linq;
using Base.Level;
using Core.Characters.Base.Behavior;
using Managers.Level;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.Events;

namespace Base
{
    public class GamePresent : MonoBehaviour
    {
        [SerializeField] private LoaderLevel _loaderLevel;
        public UnityEvent OnStartGame;
        public UnityEvent OnWin;
        public UnityEvent OnLose;

        private void OnDisable()
        {
            _loaderLevel.CurrentLevel.DeathPlayerTower -= Lose;
            _loaderLevel.CurrentLevel.DeathEnemyTower -= Win;
        }
        
        private void Start()
        {
            LoadGame();
        }

        private void LoadGame()
        {
            OnStartGame.Invoke();
            _loaderLevel.Load();
            _loaderLevel.CurrentLevel.DeathPlayerTower += Lose;
            _loaderLevel.CurrentLevel.DeathEnemyTower += Win;
        }
        // Pause
        private void Restart()
        {
        }
        private void Win()
        {
            var characters = FindObjectsOfType<BehaviourSystem>().ToList();
            characters.ForEach(character => character.SetDanceState());
            FindObjectOfType<BehaviourSystem>().SetDanceState();
            _loaderLevel.LevelCompleted();
            OnWin.Invoke();
            MMVibrationManager.Haptic (HapticTypes.Success);
        }
        private void Lose()
        {
            OnLose.Invoke();
            MMVibrationManager.Haptic (HapticTypes.Warning);
        }
    }
}