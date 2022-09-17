using System.Collections.Generic;
using Core.Characters.Enemy;
using Core.Characters.Player;
using Core.Components._ProgressComponents.Health;
using MoreMountains.NiceVibrations;
using NaughtyAttributes;
using TMPro;
using Toolkit.Extensions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Base.Level
{
    public class LoaderLevel : MonoBehaviour
    {
        [SerializeField] private List<Level> _levels;
        [SerializeField] private int _numberLevel;
        [SerializeField] private NavMeshSurface _navMeshSurface;
        [SerializeField] private TextMeshProUGUI _text;
        public static LoaderLevel Instance;
        private Level CurrentLevel => _levels[_numberLevel];
        public Player CurrentPlayer => CurrentLevel.Player;
        public Enemy CurrentEnemy => CurrentLevel.Enemy;
        public IHealthComponent PlayerTower => CurrentLevel.PlayerTower.HealthComponent;
        public IHealthComponent EnemyTower => CurrentLevel.EnemyTower.HealthComponent;
        private string _key = "Level";
        public int NumberLevel => _numberLevel;

        #region Singleton
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance == this)
            {
                Destroy(gameObject);
            }
        }
        #endregion
        
        public void Load()
        {
            _numberLevel = GetMaxInt(_key, _levels.Count - 1);
            _text.text = $"Level {_numberLevel}";
            
            _levels.ForEach(level => level.Deactivate());
            CurrentLevel.Init();
        }
        public void StartLevel()
        {
            UpdateBake();
            CurrentLevel.StartLevel();
        }

        #region Debug
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        [Button]
        public void UpdateBake()
        {
            _navMeshSurface.BuildNavMesh();
        }

        #endregion

        public void WinCompleted()
        {
            CurrentPlayer.BehaviourSystem.SetDanceState();
            if (CurrentEnemy)
            {
                CurrentEnemy.BehaviourSystem.SetCryingState();
            }

            
            Completed();
        }

        public void LoseCompleted()
        {
            CurrentPlayer.BehaviourSystem.SetCryingState();
            CurrentEnemy.BehaviourSystem.SetDanceState();
            
            Completed();
        }
        private void Completed()
        {
            CurrentPlayer.Detachment.OverAll();
            CurrentEnemy.Detachment.OverAll();

            _numberLevel++;
            PlayerPrefs.SetInt(_key,_numberLevel);
        }

        [Button]
        private void DeleteKey()
        {
            PlayerPrefs.DeleteAll();
        }
        private int GetMaxInt(string key, int maxCount)
        {
            if (PlayerPrefs.HasKey(key) == false)
            {
                return 0;
            }

            return PlayerPrefs.GetInt(key) > maxCount ? 0 : PlayerPrefs.GetInt(_key);
        }
    }
}