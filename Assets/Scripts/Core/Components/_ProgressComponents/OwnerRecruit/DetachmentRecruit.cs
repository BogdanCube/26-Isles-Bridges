using System;
using System.Collections.Generic;
using Core.Characters.Recruit;
using Core.Components._ProgressComponents.Health;
using NaughtyAttributes;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.Components._ProgressComponents.OwnerRecruit
{
    [RequireComponent(typeof(Characters.Base.Character))]
    public class DetachmentRecruit : ProgressComponent
    {
        [SerializeField] private List<MovementRecruit> _recruits = new List<MovementRecruit>();
        [SerializeField] private MovementRecruit _prefab;
        [SerializeField] private HealthComponent _healthComponent;
        private Characters.Base.Character _owner;
        public event Action<int> OnAddRecruit;
        public Action OnMax;
        public bool HasCanAdd => _recruits.Count + 1 <= _maxCount;
        public int MaxCount => _maxCount;

        #region Enable/Disable
        private void OnEnable()
        {
            _healthComponent.OnDeath += RemoveAll;
        }

        private void OnDisable()
        {
            _healthComponent.OnDeath -= RemoveAll;

        }
        #endregion
        private void Start()
        {
            Load();
            _owner = GetComponent<Characters.Base.Character>();
        }
        
        [Button]
        public void Add()
        {
            var recruit = Instantiate(_prefab, transform.position, Quaternion.identity);
            recruit.SetOwner(_owner);
            _recruits.Add(recruit);
            OnAddRecruit.Invoke(_recruits.Count);
        }

        [Button]
        private void RemoveAll()
        {
            _recruits.ForEach(recruit => recruit.HealthComponent.Death());
            _recruits = new List<MovementRecruit>();
        }
    }
}