using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Core.Character.Player;
using Core.Characters.Player;
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
        public Level CurrentLevel => _levels[_numberLevel];
        public Player CurrentPlayer => _levels[_numberLevel].Player;
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
            if (PlayerPrefs.GetInt("Level") > _levels.Count - 1)
            {
                PlayerPrefs.DeleteAll();
            }
            else
            {
                _numberLevel = PlayerPrefs.GetInt("Level");
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
            CurrentLevel.StartLevel();
            UpdateBake();
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
            PlayerPrefs.SetInt("Level",_numberLevel);
        }
    }
}