using System;
using System.Collections;
using System.Collections.Generic;
using Core.Characters.Enemy;
using Core.Components._ProgressComponents.Bag;
using DG.Tweening;
using UnityEngine;

namespace Core.Environment.Tower.DetectorBag
{
    public class DetectorBagPlayer : BaseDetectorBag
    {
        [SerializeField] private float _currenPumping = 0.1f;
        [SerializeField] private TowerLevel _towerLevel;
        [SerializeField] private Bag _tempBag;
        private IEnumerator _coroutineAdd;
        private IEnumerator _coroutineSpend;
        private BagCharacter _currentBag;
        private float _startPumping;
        private Tween _tween;
        private bool _isMoved;
        private void Start()
        {
            _startPumping = _currenPumping;
        }
        
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Characters.Player.Player player) && other.TryGetComponent(out BagCharacter bag)
                                                                           && _isMoved == false  
                                                                           && player.MovementController.IsMove == false) 
            {
                _currentBag = bag;
                _isMoved = true;
                if (_currentBag.IsZero == false)
                {
                    if (_coroutineSpend != null)
                    {
                        StopCoroutine(_coroutineSpend);
                    }

                    _tween = DOTween.To(() => _currenPumping, v => _currenPumping = v, 0, 0.5f);
                    _coroutineAdd = _tempBag.MovedCount(_currentBag, _currenPumping);
                    StartCoroutine(_coroutineAdd);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Characters.Player.Player player) && other.TryGetComponent(out BagCharacter bag))
            {
                _currentBag = null;
                _tween.Kill();
                _currenPumping = _startPumping;
                _isMoved = false;
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