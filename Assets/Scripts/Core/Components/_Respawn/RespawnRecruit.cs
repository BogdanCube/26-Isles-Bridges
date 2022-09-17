using System;
using Core.Components._ProgressComponents.Bag;
using Core.Components._ProgressComponents.Health;
using Core.Components.Loot;
using DG.Tweening;
using JetBrains.Annotations;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.Components
{
    public class RespawnRecruit : MonoBehaviour
    {
        private float _timeRespawn = 3;
        [SerializeField] private GrayscaleModel _grayscaleModel;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private LootSpawner _lootSpawner;
        #region Enable/Disable
        private void OnEnable()
        {
            _healthComponent.OnDeath += HideBody;
        }

        private void OnDisable()
        {
            _healthComponent.OnDeath -= HideBody;
        }
        #endregion

        private void Start()
        {
            _grayscaleModel.FadeToDefault(0);
        }

        private void HideBody()
        {
            var sequence = DOTween.Sequence()
                .Append(_grayscaleModel.FadeGray(_timeRespawn * 0.7f)).OnComplete(_lootSpawner.DespawnLoot)
                .Append(transform.DOScale(0,_timeRespawn * 0.3f))
                .OnComplete(() =>
                {
                    if (_healthComponent.IsOver == false)
                    {
                        Respawn();
                        _healthComponent.Respawn();
                        _grayscaleModel.FadeToDefault(0);
                    }
                });
        }
        protected virtual void Respawn()
        {
            transform.DOScale(1, 0);
            NightPool.Despawn(this);
        }
        
    }
}