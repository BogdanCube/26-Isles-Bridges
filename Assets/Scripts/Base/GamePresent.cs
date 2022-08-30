using System.Linq;
using Base.Level;
using Core.Characters._Base;
using Core.Characters.Base.Behavior;
using Core.Characters.Player;
using Managers.Level;
using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.Events;

namespace Base
{
    public class GamePresent : MonoBehaviour
    {
        [SerializeField] private LoaderLevel _loaderLevel;
        [SerializeField] private PlayerVibration _vibration;
        public UnityEvent OnStartGame;
        public UnityEvent OnWin;
        public UnityEvent OnLose;

        private void OnDisable()
        {
            _loaderLevel.PlayerTower.OnOver -= Lose;
            _loaderLevel.EnemyTower.OnOver -= Win;
        }
        
        private void Awake()
        {
            LoadGame();
        }

        private void LoadGame()
        {
            OnStartGame.Invoke();
            _loaderLevel.Load();
            _loaderLevel.PlayerTower.OnOver += Lose;
            _loaderLevel.EnemyTower.OnOver += Win;
            _vibration.Load(_loaderLevel.CurrentPlayer);
        }
        private void Win()
        {
            var characters = FindObjectsOfType<BehaviourPlayer>().ToList();
            characters.ForEach(character => character.SetDanceState());
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