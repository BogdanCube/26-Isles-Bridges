using Core.Components._ProgressComponents.Bag;
using Core.Environment.Tower._Base;
using UnityEngine;

namespace Core.Environment.Tower.DetectorBag
{
    public abstract class BaseDetectorBag : MonoBehaviour
    {
        [SerializeField] private protected TowerLevel _towerLevel;
        [SerializeField] private protected Bag _tempBag;
        #region Enable / Disable
        private void OnEnable()
        {
            _towerLevel.OnMaxUpgrade += Clean;
        }

        private void OnDisable()
        {
            _towerLevel.OnMaxUpgrade -= Clean;
        }
        #endregion

        private void Clean()
        {
            _tempBag.Reset();
        }
    }
}