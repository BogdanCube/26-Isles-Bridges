using System.Collections.Generic;
using Core.Characters.Recruit;
using Core.Components._ProgressComponents;
using UnityEngine;

namespace Core.Components
{
    public class DetachmentRecruit : ProgressComponent
    {
        [SerializeField] private List<FollowOwner> _recruits = new List<FollowOwner>();
        public bool HasCanAdd => _recruits.Count + 1 <= _maxCount;
        
        private void Start()
        {
            Load();
        }
        public void Add(FollowOwner recruit)
        {
            _recruits.Add(recruit);
        }
    }
}