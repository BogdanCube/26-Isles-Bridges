using Base.Level;
using Core.Components._ProgressComponents.Bag;
using Managers.Level;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.DisplayParameters
{
    public class DisplayBag : MonoBehaviour
    {
        [SerializeField] private LoaderLevel _loaderLevel;
        [SerializeField] private TextMeshProUGUI _text;
        private Bag _bag;

        #region Enable / Disable
        private void OnEnable()
        {
            _bag = _loaderLevel.CurrentPlayer.Bag;
            _bag.OnUpdateBag += UpdateText;
        }
        private void OnDisable()
        { 
            _bag.OnUpdateBag -= UpdateText;
        }
        #endregion
        private void UpdateText(int count)
        {
            _text.text = $"{count} / {_bag.MaxCount}";
        }
    }
}