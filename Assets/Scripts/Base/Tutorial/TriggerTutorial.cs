using System;
using Core.Characters.Player;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Base.Tutorial
{
    public class TriggerTutorial : MonoBehaviour
    {
        public UnityEvent OnTrigger;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                OnTrigger.Invoke();
                Destroy(gameObject);
            }
        }
    }
}