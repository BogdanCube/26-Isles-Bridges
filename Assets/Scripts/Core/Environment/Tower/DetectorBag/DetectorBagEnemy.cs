using System.Collections;
using Core.Characters.Enemy;
using Core.Components._ProgressComponents.Bag;
using DG.Tweening;
using UnityEngine;

namespace Core.Environment.Tower.DetectorBag
{
    public class DetectorBagEnemy : BaseDetectorBag
    {
        [SerializeField] private float _currenPumping = 0.1f;
        [SerializeField] private TowerLevel _towerLevel;
        [SerializeField] private Bag _tempBag;
        private IEnumerator _coroutineAdd;
        private IEnumerator _coroutineSpend;
        private BagCharacter _currentBag;
        private float _startPumping;
        private Tween _tween;
        private void Start()
        {
            _startPumping = _currenPumping;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy) && other.TryGetComponent(out BagCharacter bag))
            {
                _currentBag = bag;
                if (_currentBag.IsZero == false)
                {
                    _currentBag.IsBlockAdd = false;
                    enemy.MovementController.IsStopped = true;
                    if (_coroutineSpend != null)
                    {
                        StopCoroutine(_coroutineSpend);
                    }

                    _tween = DOTween.To(() => _currenPumping, v => _currenPumping = v, 0, 0.5f);
                    _coroutineAdd = _tempBag.MovedCount(_currentBag, _currenPumping, () =>
                    {
                        enemy.MovementController.IsStopped = false;
                        StopCoroutine(_coroutineAdd);
                        _coroutineSpend = _towerLevel.ReplenishmentBrick(_tempBag, () => { StopCoroutine(_coroutineAdd); });
                        StartCoroutine(_coroutineSpend);
                    });
                    StartCoroutine(_coroutineAdd);
                }
            }        
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy) && other.TryGetComponent(out BagCharacter bag))
            {
                _currentBag.IsBlockAdd = true;
                enemy.MovementController.IsStopped = false;
                _currentBag = null;
                _tween.Kill();
                _currenPumping = _startPumping;
                if (_coroutineAdd != null)
                {
                    StopCoroutine(_coroutineAdd);
                }
                _coroutineSpend = _towerLevel.ReplenishmentBrick(_tempBag);
                StartCoroutine(_coroutineSpend);
            }
        }
    }
}