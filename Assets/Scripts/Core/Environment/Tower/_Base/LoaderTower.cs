using Base.Level;
using Core.Components._Spawners;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Environment.Tower._Base
{
    public abstract class LoaderTower : MonoBehaviour
    {
        [SerializeField] private protected Tower _tower;
        [SerializeField] private protected Transform _currentTower;
        [SerializeField] private protected Transform _modelParent;
        [SerializeField] private HealthTower _healthTower;
        public abstract int MaxLevel { get; }
        protected abstract Transform TowerModel(int index);
        public abstract int PriceNextLevel(int level);
        public abstract int HealthLevel(int level);

        public virtual void Load(int index)
        {
            Destroy(_currentTower.gameObject);
            _currentTower = Instantiate(TowerModel(index), _modelParent);
            _healthTower.Load(HealthLevel(index));
            LoaderLevel.Instance.UpdateBake();
        }

        public virtual void ResetTower() { }
    }
}