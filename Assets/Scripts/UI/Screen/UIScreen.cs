using System;
using Components;
using NaughtyAttributes;
using UnityEngine;

namespace UI.Screen
{
    public class UIScreen : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private readonly int _showNameId = Animator.StringToHash("Show");
        private readonly int _hideNameId = Animator.StringToHash("Hide");
        
        private Action OnHideScreen;
        
        [Button]
        public void Show()
        {
            gameObject.SetActive(true);
            _animator.SetTrigger(_showNameId);
        }
        public void Hide(Action callback)
        {
            OnHideScreen = callback;
            HideWithoutCallback();
        }
        
        [Button("Hide")]
        public void HideWithoutCallback()
        {
            _animator.SetTrigger(_hideNameId);
        }

        private void EnableCallback()
        {
            OnHideScreen?.Invoke();
            gameObject.SetActive(false);
        }
    }
}