using Components.Weapon;
using Core.Character.Behavior;
using Core.Components;
using Core.Components._ProgressComponents.Health;
using Core.Components.Weapon;
using Core.Environment.Tower;
using UnityEngine;

namespace Core.Characters.Base
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private MovementController _movementController;
        [SerializeField] private AnimationStateController _animationStateController;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private DetectorFighting _detectorFighting;
        [SerializeField] [InterfaceType(typeof(IWeapon))] private Object _weapon;
        [SerializeField] private Color _color;
        public MovementController MovementController => _movementController;
        public AnimationStateController AnimationStateController => _animationStateController;
        public IHealthComponent HealthComponent => _healthComponent;
        public DetectorFighting DetectorFighting => _detectorFighting;
        public IWeapon Weapon => (IWeapon)_weapon;
        public Color Color => _color;
    }
}