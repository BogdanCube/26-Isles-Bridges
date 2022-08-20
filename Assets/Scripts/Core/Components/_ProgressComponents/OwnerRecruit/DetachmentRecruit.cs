using System;
using System.Collections.Generic;
using Core.Character.Behavior;
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
        [SerializeField] private float _radius = 3;
        [SerializeField] private MovementController _movementController;
        [SerializeField] private List<MovementRecruit> _recruits = new List<MovementRecruit>();
        [SerializeField] private MovementRecruit _prefab;
        [SerializeField] private HealthComponent _healthComponent;
        private Characters.Base.Character _owner;
        public event Action<int> OnUpdateCount;
        public Action OnMax;
        public bool HasCanAdd => _recruits.Count + 1 <= _maxCount;
        private bool IsRecruits => _recruits.Count > 0;
        public int MaxCount => _maxCount;

        #region Enable/Disable
        private void OnEnable()
        {
            _healthComponent.OnDeath += OverAll;
            _healthComponent.OnOver += OverAll;
            _movementController.OnChangePosition += GroupMovement;
        }
        private void OnDisable()
        {
            _healthComponent.OnDeath -= OverAll;
            _healthComponent.OnOver -= OverAll;
            _movementController.OnChangePosition -= GroupMovement;
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
            recruit.Init(_owner,this);
            _recruits.Add(recruit);
            UpdateCount();
        }

        public void Remove(MovementRecruit recruit)
        {
            _recruits.Remove(recruit);
            UpdateCount();
        }
        
        [Button]
        private void OverAll()
        {
            if (!IsRecruits) return;
            foreach (var recruit in _recruits)
            {
                NightPool.Despawn(recruit);
            }
            _recruits = new List<MovementRecruit>();
        }

        private void GroupMovement(Vector3 center)
        {
            if (!IsRecruits) return;
                
            float step = (Mathf.Deg2Rad * 360) / _recruits.Count;
            List<Vector3> result = new List<Vector3>();
            for (int i = 0; i < _recruits.Count; i++)
            {
                result.Add(new Vector3(Mathf.Cos(i * step),0,Mathf.Sin(i * step)));
            }

            for (int i = 0; i < _recruits.Count; i++)
            {
                _recruits[i].MoveToTarget(center + result[i] * _radius);
            }
        }
        
        protected override void UpdateCount()
        {
            OnUpdateCount?.Invoke(_recruits.Count);
        }
    }
}