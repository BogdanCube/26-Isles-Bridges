using Core.Character.Behavior;
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
        [SerializeField] private float _timeRespawn;
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
        
        private void HideBody()
        {
            _grayscaleModel.FadeGray(_timeRespawn * 0.75f).OnComplete(() =>
            {
                _lootSpawner.DespawnLoot();
                transform.DOScale(0,_timeRespawn * 0.25f).OnComplete(() =>
                {
                    if (_healthComponent.IsOver == false)
                    {
                        Respawn();
                        _healthComponent.Respawn();
                        _grayscaleModel.FadeToDefault(0);
                    }
                });
                
            });
        }
        protected virtual void Respawn()
        {
            transform.DOScale(1, 0);
            NightPool.Despawn(this);
        }
        
    }
}