using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Components._ProgressComponents
{
    public class ProgressComponent : MonoBehaviour
    {
        [Expandable] [SerializeField] private ComponentData _componentData;
        [SerializeField] private protected int _maxCount;

        private int _level;
        public event Action OnLevelUp;
        private int CurrentMaxCount => _componentData.Template[_level].MaxCount;
        private int NextMaxCount => IsMaxLevel == false ? _componentData.Template[_level + 1].MaxCount : 0;
        protected bool IsProgress => _componentData;
        public int Price => _componentData.Template[_level].Price;
        public bool IsMaxLevel => _level + 1 >= _componentData.Template.Count;
        public string ProgressText => $"{CurrentMaxCount} -> {NextMaxCount}";
        public Sprite Icon => _componentData.Icon;
        protected void Load()
        {
            _maxCount = CurrentMaxCount;
        }

        public void LevelUp()
        {
            _level++;
            _maxCount = CurrentMaxCount;
            OnLevelUp?.Invoke();
            UpdateCount();
        }

        protected virtual void UpdateCount(){}
    }
}