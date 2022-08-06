using Base.Level;
using Core.Components.Wallet;
using Managers.Level;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.DisplayParameters
{
    public class DisplayWallet : MonoBehaviour
    {
        [SerializeField] private LoaderLevel _loaderLevel;
        [SerializeField] private TextMeshProUGUI _text;
        private Wallet _wallet;
        #region Enable/Disable
        private void OnEnable()
        {
            _wallet = _loaderLevel.CurrentPlayer.Wallet;
            _wallet.OnUpdateCount += UpdateText;
        }
        private void OnDisable()
        {
            _wallet.OnUpdateCount -= UpdateText;

        }
        #endregion
        
        private void UpdateText(int count)
        {
            _text.text = count.ToString();
        }
    }
}