using System;
using Core.Characters.Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Base.Tutorial
{
    public class TextTrigger : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        [SerializeField] private string _text;
        public UnityEvent OnTrigger;
        private void Start()
        {
            _textMeshPro.gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _textMeshPro.text = _text;
                OnTrigger.Invoke();
                Destroy(gameObject);
            }
        }
    }
}