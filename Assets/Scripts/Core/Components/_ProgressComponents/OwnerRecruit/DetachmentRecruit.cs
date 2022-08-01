using System.Collections.Generic;
using Core.Characters.Recruit;
using UnityEngine;

namespace Core.Components._ProgressComponents.OwnerRecruit
{
    public class DetachmentRecruit : ProgressComponent
    {
        [SerializeField] private List<MovementRecruit> _recruits = new List<MovementRecruit>();
        [SerializeField] private MovementRecruit _prefab;
        public bool HasCanAdd => _recruits.Count + 1 <= _maxCount;
        private void Start()
        {
            Load();
        }
        public void Add()
        {
            var recruit = Instantiate(_prefab, transform.position,Quaternion.identity);
                //NightPool.Spawn(_prefab,Vector3.zero);
            recruit.SetTarget(transform);
            _recruits.Add(recruit);
        }
    }
}