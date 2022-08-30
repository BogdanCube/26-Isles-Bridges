using System;
using Core.Characters.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Base.Level
{
    public class AdminMode : MonoBehaviour
    {
        [SerializeField] private LoaderLevel _loaderLevel;

        [SerializeField] private Button _buttonBrick;
        [SerializeField] private Button _buttonRecruit;
        [SerializeField] private Button _buttonCoin;
        private Player Player => _loaderLevel.CurrentPlayer;

        #region Enable / Disable
        private void OnEnable()
        {
            _buttonBrick.onClick.AddListener(AddBlock);
            _buttonRecruit.onClick.AddListener(AddRecruit);
            _buttonCoin.onClick.AddListener(AddCoin);
        }

        private void OnDisable()
        {
            _buttonBrick.onClick.RemoveListener(AddBlock);
            _buttonRecruit.onClick.RemoveListener(AddRecruit);
            _buttonCoin.onClick.RemoveListener(AddCoin);
        }

        #endregion

        private void AddBlock()
        {
            Player.Bag.Add(20);
        }

        private void AddRecruit()
        {
            Player.Detachment.Add();
        }
        private void AddCoin()
        {
            Player.Wallet.Add(20);
        }
    }
}