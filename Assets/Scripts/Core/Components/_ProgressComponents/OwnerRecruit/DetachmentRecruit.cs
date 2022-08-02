using System.Collections.Generic;
using Core.Characters.Recruit;
using NaughtyAttributes;
using UnityEngine;

namespace Core.Components._ProgressComponents.OwnerRecruit
{
    [RequireComponent(typeof(Characters.Base.Character))]
    public class DetachmentRecruit : ProgressComponent
    {
        [SerializeField] private List<MovementRecruit> _recruits = new List<MovementRecruit>();
        [SerializeField] private MovementRecruit _prefab;
        private Characters.Base.Character _owner;
        public bool HasCanAdd => _recruits.Count + 1 <= _maxCount;
        private void Start()
        {
            Load();
            _owner = GetComponent<Characters.Base.Character>();
        }
        [Button]
        public void Add()
        {
            var recruit = Instantiate(_prefab, transform.position,Quaternion.identity);
                //NightPool.Spawn(_prefab,Vector3.zero);
            recruit.SetOwner(_owner);
            _recruits.Add(recruit);
        }
    }
}