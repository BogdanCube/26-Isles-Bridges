using Core.Components;
using Core.Components._ProgressComponents.Bag;
using Core.Components._ProgressComponents.Health;
using Core.Components._ProgressComponents.OwnerRecruit;
using Core.Components.Wallet;
using Core.Components.Weapon;
using UnityEngine;

namespace Core.Characters._Base
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private BehaviourSystem _behaviourSystem;
        [SerializeField] private AnimationStateController _animationStateController;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private DetectorFighting _detectorFighting;
        [SerializeField] [InterfaceType(typeof(IWeapon))] private Object _weapon;
        [SerializeField] private Bag _bag;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private Material _material;
        [SerializeField] private ParticleSystem _winParticle;
        [SerializeField] private DetachmentRecruit _detachment;

        public BehaviourSystem BehaviourSystem => _behaviourSystem;
        public AnimationStateController AnimationStateController => _animationStateController;
        public IHealthComponent HealthComponent => _healthComponent;
        public DetectorFighting DetectorFighting => _detectorFighting;
        public IWeapon Weapon => (IWeapon)_weapon;
        public Bag Bag => _bag;
        public Wallet Wallet => _wallet;
        public Color Color => _material.color;
        public Transform WinParticle => _winParticle.transform;
        public DetachmentRecruit Detachment => _detachment;

    }
}