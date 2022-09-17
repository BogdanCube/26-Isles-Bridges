using System;
using Base.Level;
using UnityEngine;

namespace Base
{
    public class AnalyticEvent : MonoBehaviour
    {
        [SerializeField] private GamePresent _gamePresent;
        [SerializeField] private LoaderLevel _loaderLevel;
        private string levelId => $"level_{_loaderLevel.NumberLevel}";

        #region Enable / Disable
        private void OnEnable()
        {
            _gamePresent.OnStartGame.AddListener(StartLevel);
            _gamePresent.OnWin.AddListener(Win);
            _gamePresent.OnLose.AddListener(Lose);
        }

        private void OnDisable()
        {
            _gamePresent.OnStartGame.RemoveListener(StartLevel);
            _gamePresent.OnWin.RemoveListener(Win);
            _gamePresent.OnLose.RemoveListener(Lose);
        }
        #endregion
        private void StartLevel()
        {
            bool measureFPS = true; 
            string skin = ""; 
            string gameType = ""; 

            HoopslyIntegration.RaiseLevelStartEvent(levelId, measureFPS, skin, gameType);
        }
        private void Win()
        {
            FinishLevel(LevelFinishedResult.win);
        }
        private void Lose()
        {
            FinishLevel(LevelFinishedResult.lose);
        }

        private void FinishLevel(LevelFinishedResult result)
        {
            LevelFinishedResult finishedResult = result;
            string reason = ""; 
            string enemy = ""; 
            string gameType = ""; 

            HoopslyIntegration.RaiseLevelFinishedEvent(levelId, finishedResult, reason, enemy, gameType);
        }
    }
}