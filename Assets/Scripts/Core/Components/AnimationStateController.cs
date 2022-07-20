using UnityEngine;

namespace Core.Character
{
    public class AnimationStateController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private readonly int _runningNameId = Animator.StringToHash("IsRunning");
        private readonly int _attackNameId = Animator.StringToHash("IsAttack");
        private readonly int _deathNameId = Animator.StringToHash("Death");

        private bool _isRunning;
        private bool _isFighting;
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                if (value != _isRunning)
                {
                    _isRunning = value;
                    _animator.SetBool(_runningNameId, value);
                }
            }
        }
        public bool IsFighting
        {
            get => _isFighting;
            set
            {
                if (value != _isFighting)
                {
                    _isFighting = value;
                    _animator.SetBool(_attackNameId, value);
                }
            }
        }
        
        public void Death()
        {
            _animator.SetTrigger(_deathNameId);
        }
    }
}