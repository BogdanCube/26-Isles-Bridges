using System;
using Core.Components._ProgressComponents.Health;
using Core.Environment.Tower;
using DG.Tweening;
using NaughtyAttributes;
using NTC.Global.Pool;
using Rhodos.Toolkit.Extensions;
using UnityEngine;

namespace Core.Components.Weapon.Bow
{
    public class Bow : MonoBehaviour, IWeapon
    {
        [SerializeField] private Arrow _arrow;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private AnimationStateController _animation;
        [ShowNonSerializedField] private int _damage;
        private Transform _transform;
        public event Action OnTakeDamage;

        [ShowNativeProperty] private float Speed => _animation.Speed;
        
        public void Load(TemplateDefTower template)
        {
            _damage = template.Damage;
            _animation.SetSpeed(template.Speed);
        }
        public void TakeDamage(Transform target, IHealthComponent health)
        {
            var arrow = NightPool.Spawn(_arrow,_spawnPoint.position);
            transform.LookAt(target.transform);
            arrow.Launch(target, () =>
            {
                health.Hit(_damage);
                NightPool.Despawn(arrow);
            });
        }
    }
}