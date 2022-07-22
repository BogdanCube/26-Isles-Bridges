using System.Collections.Generic;
using Core.Character.Behavior;
using Core.Characters.Recruit;
using UnityEngine;

namespace Core.Components
{
    public class DetachmentRecruit : MonoBehaviour
    {
        [SerializeField] private List<FollowOwner> _recruits = new List<FollowOwner>();
        [SerializeField] private int _maxCount;
        public bool HasCanAdd => _recruits.Count + 1 <= _maxCount;
        public void Add(FollowOwner recruit)
        {
            _recruits.Add(recruit);
        }
    }
}