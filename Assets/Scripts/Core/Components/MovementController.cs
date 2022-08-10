using System;
using Base.Level;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Character.Behavior
{
    public abstract class MovementController : MonoBehaviour
    {
        [SerializeField] private protected float _speed;
        [SerializeField] private protected NavMeshAgent _navMeshAgent;
        private Transform _transform;
        private Vector3 _lastPose;
        public event Action<Vector3> OnChangePosition;

        public abstract bool IsMove { get; }
        private bool _isStopped;
        public bool IsStopped
        {
            get => _isStopped;
            set
            {
                if (value != _isStopped)
                {
                    _isStopped = value;
                    _navMeshAgent.isStopped = _isStopped;
                }
            }
        }
        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _lastPose = _transform.position;

        }
        public virtual void Move()
        {
            if (_transform.position != _lastPose)
            {
                OnChangePosition?.Invoke(_transform.position);
            }

            _lastPose = _transform.position;
        }

        public void SetStartPos(Vector3 position)
        {
            IsStopped = false;
            _navMeshAgent.Warp(position);
        }
    }
}