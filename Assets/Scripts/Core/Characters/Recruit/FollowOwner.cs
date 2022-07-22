using Core.Characters.Enemy;
using UnityEngine;

namespace Core.Characters.Recruit
{
    public class FollowOwner : FollowСharacter
    {
        [SerializeField] private float _stoppingDistance;
        public override bool IsMove => _target != null && DistanceToTarget > _stoppingDistance;
        private float DistanceToTarget => Vector3.Distance(transform.position, _target.position);

        public override void Start()
        {
            base.Start();
            _navMeshAgent.stoppingDistance = _stoppingDistance;
        }
    }
}