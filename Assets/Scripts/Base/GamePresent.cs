using System.Linq;
using Base.Level;
using Core.Characters._Base;
using Core.Characters.Enemy;
using Core.Characters.Player;
using Core.Characters.Recruit;
using Managers.Level;
using MoreMountains.NiceVibrations;
using NTC.Global.Pool;
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
        }
        private void Win()
        {
            _loaderLevel.WinCompleted();
            OnWin.Invoke();
        }
        private void Lose()
        {
            _loaderLevel.LoseCompleted();
            OnLose.Invoke();
        }
    }
}