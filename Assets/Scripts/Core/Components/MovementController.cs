using System;
using NTC.Global.Cache;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Components
{
    public abstract class MovementController : NightCache
    {
        [SerializeField] private protected float _speed;
        [SerializeField] private protected NavMeshAgent _navMeshAgent;
        private Vector3 _lastPose;
        public event Action<Vector3> OnChangePosition;

        public virtual bool IsMove { get; }
        private bool _isStopped;
        public bool IsStopped
        {
            get => _isStopped;
            set
            {
                if (value != _isStopped)
                {
                    _isStopped = value;
                    _navMeshAgent.enabled = !_isStopped;
                }
            }
        }

       
        private void Awake()
        {
            _lastPose = transform.position;
            _navMeshAgent.speed = _speed;

        }
        public virtual void Move()
        {
            if (IsStopped) return;
            
            if (transform.position != _lastPose)
            {
                OnChangePosition?.Invoke(transform.position);
            }

            _lastPose = transform.position;
        }

        public void SetStartPos(Vector3 target)
        {
            IsStopped = true;
            _navMeshAgent.Warp(target);
            IsStopped = false;
        }
    }
}