using UnityEngine;
using UnityEngine.AI;

namespace Core.Character.Behavior
{
    public abstract class MovementController : MonoBehaviour
    {
        [SerializeField] private protected float _speed;
        [SerializeField] private protected NavMeshAgent _navMeshAgent;
        public virtual bool IsMove => false;
        private bool _isStopped;
        public bool IsStopped
        {
            get => _isStopped;
            set
            {
                if (value != _isStopped)
                {
                    _isStopped = value;
                    _navMeshAgent.isStopped = value;
                }
            }
        }
        public abstract void Move();

    }
}