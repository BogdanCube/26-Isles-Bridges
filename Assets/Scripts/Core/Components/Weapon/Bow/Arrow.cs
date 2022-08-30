using System;
using System.Collections;
using Toolkit.Extensions;
using UnityEngine;

namespace Core.Components.Weapon.Bow
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _force;
        private Transform _target;
        private Action OnHitTarget;
        
        public void Launch(Transform target,Action callback)
        {
            _target = target;
            OnHitTarget = callback;
            _rigidbody.velocity = Vector3.zero;
            transform.LookAt(_target);
            
            _rigidbody.AddForce(transform.forward * _force,ForceMode.Impulse);
            StartCoroutine(Deactive(5, transform.Deactivate));
        }

        private IEnumerator Deactive(int time, Action callback)
        {
            yield return new WaitForSeconds(time);
            callback.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform == _target)
            {
                _target = null;
                OnHitTarget.Invoke();
            }
        }
    }
}