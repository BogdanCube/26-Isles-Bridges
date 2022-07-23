using Core.Components.Wallet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.DisplayParametrs
{
    public class WalletDisplay : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private TextMeshProUGUI _text;
        #region Enable/Disable
        private void OnEnable()
        {
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