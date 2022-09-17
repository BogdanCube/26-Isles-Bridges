using Core.Components._ProgressComponents.Bag;
using UnityEngine;
using UnityEngine.Events;

namespace Base.Tutorial
{
    public class TutorialBagEvent : MonoBehaviour
    {
        [SerializeField] private Bag _bag;
        [SerializeField] private int _count;
        public UnityEvent OnCollect;

        private void OnEnable()
        {
            _bag.OnUpdateBag += UpdateCount;
        }

        private void OnDisable()
        {
            _bag.OnUpdateBag -= UpdateCount;
        }

        private void UpdateCount(int count)
        {
            if (_count == count)
            {
                OnCollect?.Invoke();
            }
        }
    }
}