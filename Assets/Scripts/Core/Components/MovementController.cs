using UnityEngine;
using UnityEngine.AI;

namespace Core.Character.Behavior
{
    public abstract class MovementController : MonoBehaviour
    {
        [SerializeField] private protected float _speed;
        [SerializeField] private protected NavMeshAgent _navMeshAgent;
        public virtual bool IsMove => false;
        public abstract void Move();
    }
}