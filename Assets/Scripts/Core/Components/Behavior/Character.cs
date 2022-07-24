using Components.Health;
using Components.Weapon;
using Core.Character;
using Core.Character.Behavior;
using Core.Components.Health;
using UnityEngine;

namespace Core.Components.Behavior
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private MovementController _movementController;
        [SerializeField] private AnimationStateController _animationStateController;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private DetectorFighting _detectorFighting;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Color _color;

        public MovementController MovementController => _movementController;
        public AnimationStateController AnimationStateController => _animationStateController;
        public HealthComponent HealthComponent => _healthComponent;
        public DetectorFighting DetectorFighting => _detectorFighting;
        public Weapon Weapon => _weapon;
        public Color Color => _color;
    }
}