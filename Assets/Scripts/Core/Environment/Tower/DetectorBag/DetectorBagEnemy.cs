using System;
using System.Collections;
using Core.Characters.Enemy;
using Core.Components._ProgressComponents.Bag;
using Core.Environment.Tower._Base;
using DG.Tweening;
using UnityEngine;

namespace Core.Environment.Tower.DetectorBag
{
    public class DetectorBagEnemy : BaseDetectorBag
    {
        [SerializeField] private float _currenPumping = 0.1f;
      
        private IEnumerator _coroutineAdd;
        private IEnumerator _coroutineSpend;
        private BagCharacter _currentBag;
        private float _startPumping;
        private BoxCollider _collider;
        private Tween _tween;
        
        private void Start()
        {
            _startPumping = _currenPumping;
            _collider = GetComponent<BoxCollider>();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (_towerLevel.IsMaxLevel == false && other.TryGetComponent(out Enemy enemy) && other.TryGetComponent(out BagCharacter bag))
            {
                _currentBag = bag;
                if (_currentBag.IsZero == false)
                {
                    _currentBag.IsBlockAdd = false;
                    if (_coroutineSpend != null)
                    {
                        StopCoroutine(_coroutineSpend);
                    }

                    _tween = DOTween.To(() => _currenPumping, v => _currenPumping = v, 0, 0.5f);
                    _coroutineAdd = _tempBag.MovedCount(_currentBag, _currenPumping, () =>
                    {
                        enemy.MovementController.UpdatePos();
                        _collider.size = new Vector3(_collider.size.x, _collider.size.y, 0.35f);
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
            if (_towerLevel.IsMaxLevel == false && other.TryGetComponent(out Enemy enemy) && other.TryGetComponent(out BagCharacter bag))
            {
                _currentBag.IsBlockAdd = true;
                _currentBag = null;
                _tween.Kill();
                _currenPumping = _startPumping;
                _collider.size = new Vector3(_collider.size.x, _collider.size.y, 2.35f);
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