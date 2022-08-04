using Core.Environment.Tower;
using UnityEngine;

namespace Core.Components._ProgressComponents.Health
{
    public class AutoDeath : MonoBehaviour
    {
        [SerializeField] private HealthTower _healthTower;
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private RespawnComponent _respawnComponent;
        #region Enable / Disable
        private void OnEnable()
        {
            _healthTower.OnDeath += Death;
        }
        
        private void OnDisable()
        {
            _healthTower.OnDeath -= Death;
        }
        #endregion
        private void Death()
        {
            _healthComponent.Death();
            _respawnComponent.IsRespawn = false;
        }
    }
}