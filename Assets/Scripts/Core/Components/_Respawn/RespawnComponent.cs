using System;
using Core.Character.Behavior;
using Core.Components._ProgressComponents.Health;
using Core.Components.Loot;
using DG.Tweening;
using NTC.Global.Pool;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Components
{
    public class RespawnComponent : MonoBehaviour
    {
        [SerializeField] private float _timeRespawn;
        [SerializeField] private MovementController _movementController;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private GrayscaleModel _grayscaleModel;
        [SerializeField] private LootSpawner _lootSpawner;
        public bool IsRespawn;
        private Vector3 _startPos;
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
            _startPos = transform.position;
        }

        private void HideBody()
        {
            _grayscaleModel.FadeGray(_timeRespawn * 0.75f).OnComplete(() =>
            {
                _lootSpawner.DespawnLoot();
                transform.DOScale(0,_timeRespawn * 0.25f).OnComplete(() =>
                {
                    if (IsRespawn)
                    {
                        Respawn();
                    }
                    else
                    {
                        transform.DOScale(1, 0);
                        NightPool.Despawn(this);
                    }
                });
                
            });
        }
        private void Respawn()
        {
            transform.DOScale(1, 0);
            _grayscaleModel.FadeToDefault(0);
            _movementController.SetStartPos(_startPos); 
            _healthComponent.Respawn();
        }
    }
}