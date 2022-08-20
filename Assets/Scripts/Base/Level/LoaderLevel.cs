using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Core.Character.Player;
using Core.Characters.Player;
using Core.Components._ProgressComponents.Health;
using Core.Environment.Tower;
using MoreMountains.NiceVibrations;
using NaughtyAttributes;
using Rhodos.Toolkit.Extensions;
using TMPro;
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
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        public static LoaderLevel Instance;
        private Level CurrentLevel => _levels[_numberLevel];
        public Player CurrentPlayer => CurrentLevel.Player;
        public IHealthComponent PlayerTower => CurrentLevel.PlayerTower.HealthComponent;
        public IHealthComponent EnemyTower => CurrentLevel.EnemyTower.HealthComponent;
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
            if (PlayerPrefs.GetInt("level") > _levels.Count - 1)
            {
                PlayerPrefs.DeleteAll();
            }
            else
            {
                _numberLevel = PlayerPrefs.GetInt("level");
                _text.text = $"Level {_numberLevel}";
                _levels.ForEach(level => level.transform.Deactivate());
                CurrentLevel.transform.Activate();
                _virtualCamera.Follow = CurrentLevel.Player.transform;
                CurrentLevel.Load();
            }
        }
        public void StartLevel()
        {
            MMVibrationManager.Haptic (HapticTypes.Selection);
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

        public void LevelCompleted()
        {
            _numberLevel++;
            PlayerPrefs.SetInt("level",_numberLevel);
        }

        [Button]
        private void DeleteKey()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}