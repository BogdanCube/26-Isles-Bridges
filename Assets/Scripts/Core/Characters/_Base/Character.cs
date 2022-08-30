using Core.Components;
using Core.Components._ProgressComponents.Bag;
using Core.Components._ProgressComponents.Health;
using Core.Components.Wallet;
using Core.Components.Weapon;
using UnityEngine;

namespace Core.Characters.Base
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private AnimationStateController _animationStateController;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private DetectorFighting _detectorFighting;
        [SerializeField] [InterfaceType(typeof(IWeapon))] private Object _weapon;
        [SerializeField] private Bag _bag;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private Color _color;
        [SerializeField] private ParticleSystem _winParticle;
        public AnimationStateController AnimationStateController => _animationStateController;
        public IHealthComponent HealthComponent => _healthComponent;
        public DetectorFighting DetectorFighting => _detectorFighting;
        public IWeapon Weapon => (IWeapon)_weapon;
        public Bag Bag => _bag;
        public Wallet Wallet => _wallet;
        public Color Color => _color;
        public Transform WinParticle => _winParticle.transform;
    }
}