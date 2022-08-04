using System;
using Components;
using NaughtyAttributes;
using Rhodos.Toolkit.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Screen
{
    public class UIScreen : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private readonly int _showNameId = Animator.StringToHash("Show");
        private readonly int _hideNameId = Animator.StringToHash("Hide");
        
        private Action OnHideScreen;
        public UnityEvent OnHideEvent;
        [Button]
        public void Show()
        {
            transform.Activate();
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
            OnHideEvent?.Invoke();
            transform.Deactivate();
        }
    }
}