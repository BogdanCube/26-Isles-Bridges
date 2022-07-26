using System.Collections.Generic;
using Core.Characters.Recruit;
using Core.Components._ProgressComponents;
using UnityEngine;

namespace Core.Components
{
    public class DetachmentRecruit : ProgressComponent
    {
        [SerializeField] private List<MovementRecruit> _recruits = new List<MovementRecruit>();
        public bool HasCanAdd => _recruits.Count + 1 <= _maxCount;
        
        private void Start()
        {
            Load();
        }
        public void Add(MovementRecruit recruit)
        {
            _recruits.Add(recruit);
        }
    }
}