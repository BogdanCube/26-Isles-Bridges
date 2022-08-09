using System;
using Core.Components._ProgressComponents.Health;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.Components.Weapon.Bow
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _force;
        private Transform _currentTarget;
        private Action OnHitTarget;
        
        public void Launch(Transform target,Action callback)
        {
            _currentTarget = target;
            OnHitTarget = callback;
            
            transform.LookAt(_currentTarget);
            _rigidbody.AddForce(transform.forward * _force,ForceMode.Impulse);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform == _currentTarget)
            {
                OnHitTarget.Invoke();
            }
            else
            {
                NightPool.Despawn(this);
            }
        }
    }
}