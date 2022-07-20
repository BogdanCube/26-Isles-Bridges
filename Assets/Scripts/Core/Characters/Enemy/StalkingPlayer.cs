using Core.Character.Behavior;
using DG.Tweening;
using UnityEngine;

namespace Core.Characters.Enemy
{
    public class StalkingPlayer : MovementController
    {
        [SerializeField] private Transform _target;
        public override bool IsMove => _target != null;
        
        private void Start()
        {
            _navMeshAgent.speed = _speed;
            _target = FindObjectOfType<Character.Player.Player>().transform;
        }
        
        public override void Move()
        {
            _navMeshAgent.SetDestination(_target.position);
            transform.DOLookAt(_target.position, 0.5f);
        }
    }
}

