using System;
using Base.Level;
using Core.Components._ProgressComponents.OwnerRecruit;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.DisplayParameters
{
    public class DisplayDetachment : MonoBehaviour
    {
        [SerializeField] private LoaderLevel _loaderLevel;
        [SerializeField] private TextMeshProUGUI _text;
        private DetachmentRecruit _detachment;

        #region Enable/Disable

        private void OnEnable()
        {
            _detachment = _loaderLevel.CurrentPlayer.Detachment;
            _detachment.OnAddRecruit += UpdateText;
            _detachment.OnMax += FadeMaxText;
        }

        private void OnDisable()
        {
            _detachment.OnAddRecruit -= UpdateText;
            _detachment.OnMax -= FadeMaxText;
        }

        #endregion
        private void UpdateText(int count)
        {
            _text.text = $"{count} / {_detachment.MaxCount}";
        }

        private void FadeMaxText()
        {
            _text.color = Color.yellow;
            _text.DOColor(Color.white, 1);
        }
    }
}