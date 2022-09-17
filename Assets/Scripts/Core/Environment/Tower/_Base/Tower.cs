using Base.Level;
using Core.Characters._Base;
using Core.Components._ProgressComponents.Health;
using Core.Environment.Tower.DetectorBag;
using DG.Tweening;
using UnityEngine;

namespace Core.Environment.Tower._Base
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private Character _owner;
        [SerializeField] private Island.Island _island;
        [SerializeField] private HealthTower _healthTower;
        [SerializeField] private TowerLevel _level;
        private BaseDetectorBag _baseDetectorBag;
        
        private NoBuilding.NoBuilding _noBuilding;
        public Character Owner => _owner;
        public HealthTower HealthComponent => _healthTower;
        public Island.Island Island => _island;
        public TowerLevel Level => _level;
        public Transform BaseDetectorBag => _baseDetectorBag.transform;

        private void Start()
        {
            _baseDetectorBag = GetComponentInChildren<BaseDetectorBag>();
        }

        public void Initialization(Character owner, NoBuilding.NoBuilding noBuilding, Island.Island island)
        {
            transform.localScale = Vector3.zero;
            _owner = owner;
            _noBuilding = noBuilding;
            _island = island;
            _level.LoadTower();

            transform.DOScale(1, 1f).OnComplete(() =>
            {
                LoaderLevel.Instance.UpdateBake();
            });
        }

        public void ReturnNoBuilding()
        {
            if (_noBuilding)
            {
                _noBuilding.Return();
                _noBuilding.FreeIsland.SetColor(Color.gray, 1);
            }
            else
            {
                _owner.HealthComponent.Over();
            }
        }
    }
}