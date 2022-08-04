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
        [SerializeField] private MovementRecruit _prefabRecruit;
        [SerializeField] private MovementRecruit _prefabArcher;
        [SerializeField] private HealthComponent _healthComponent;
        private Characters.Base.Character _owner;
        public bool HasCanAdd => _recruits.Count + 1 <= _maxCount;
        
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
        public void AddRecruit()
        {
            Add(Instantiate(_prefabRecruit, transform.position,Quaternion.identity));

        }
        [Button]
        public void AddArcher()
        {
            Add(Instantiate(_prefabArcher, transform.position,Quaternion.identity));
        }
        private void Add(MovementRecruit recruit)
        {
            recruit.SetOwner(_owner);
            _recruits.Add(recruit);
        }

        [Button]
        private void RemoveAll()
        {
            _recruits.ForEach(recruit => recruit.HealthComponent.Death());
            _recruits = new List<MovementRecruit>();
        }
    }
}