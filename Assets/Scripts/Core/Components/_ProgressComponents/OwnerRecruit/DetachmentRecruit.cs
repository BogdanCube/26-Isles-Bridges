using System.Collections.Generic;
using Core.Characters.Recruit;
using Core.Components._ProgressComponents;
using Core.Environment._ItemSpawn;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.Components
{
    public class DetachmentRecruit : ProgressComponent
    {
        [SerializeField] private List<MovementRecruit> _recruits = new List<MovementRecruit>();
        [SerializeField] private MovementRecruit _recruit;
        public bool HasCanAdd => _recruits.Count + 1 <= _maxCount;
        
        private void Start()
        {
            Load();
        }
        public void Add(Transform spawnPos)
        {
            var recruit = NightPool.Spawn(_recruit, spawnPos.transform.position);
            recruit.SetTarget(transform);
            _recruits.Add(recruit);
        }
    }
}