using System;
using Core.Components._ProgressComponents.Health;
using Core.Environment.Tower;
using DG.Tweening;
using NTC.Global.Pool;
using Rhodos.Toolkit.Extensions;
using UnityEngine;

namespace Core.Components.Weapon.Bow
{
    public class Bow : MonoBehaviour, IWeapon
    {
        [SerializeField] private int _damage;
        [SerializeField] [Range(1, 2)] private float _speed = 1;
        [SerializeField] private Arrow _arrow;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Animator _animator;
        public Action OnTakeDamage { get; set; }

        public void Load(TemplateDefTower template)
        {
            _damage = template.Damage;
            _speed = template.Speed;
            _animator.speed = _speed;
        }
        public void TakeDamage(Transform target, IHealthComponent health)
        {
            var arrow = NightPool.Spawn(_arrow,_spawnPoint.position);
            transform.DOLookAt(target.transform.position, 0.5f);
            arrow.Launch(target, () =>
            {
                health.Hit(_damage);
                NightPool.Despawn(arrow);
            });


            // створення префаба
            // вистріл + кут
            // якщо стріла столкнулася -хп
            //speed
        }

        public void Load()
        {
            throw new NotImplementedException();
        }
    }
}