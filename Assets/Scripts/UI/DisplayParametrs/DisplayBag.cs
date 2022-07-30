using Core.Components._ProgressComponents.Bag;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.DisplayParametrs
{
    public class DisplayBag : MonoBehaviour
    {
        [SerializeField] private Bag _bag;
        [SerializeField] private TextMeshProUGUI _text;
        
        #region Enable / Disable
        private void OnEnable()
        {
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