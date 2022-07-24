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
        public int Price => _componentData.Template[_level].Price;
        public bool IsMaxLevel => _level + 1 >= _componentData.Template.Count;
        public string ProgressText => $"{CurrentMaxCount} -> {NextMaxCount}" ;
        private int CurrentMaxCount => _componentData.Template[_level].MaxCount;
        private int NextMaxCount => IsMaxLevel == false ? _componentData.Template[_level + 1].MaxCount : 0;
        public bool IsProgress => _componentData;
        protected void Load()
        {
            _maxCount = CurrentMaxCount;
        }

        public virtual void LevelUp()
        {
            _level++;
            _maxCount = CurrentMaxCount;
        }
    }
}