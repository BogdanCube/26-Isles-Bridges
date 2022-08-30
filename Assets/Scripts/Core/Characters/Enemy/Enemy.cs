using Core.Characters.Enemy.Finder;
using Core.Components.DataTowers;
using UnityEngine;

namespace Core.Characters.Enemy
{
    public class Enemy : Base.Character
    {
        [SerializeField] private MovementEnemy _movementController;
        [SerializeField] private FinderOutside _finderOutside;
        [SerializeField] private FinderInside _finderInside;
        [SerializeField] private FinderPlayer _finderPlayer;
        [SerializeField] private DataTowers _dataTowers;
        [SerializeField] private DataProgressComponent _dataProgress;
        public MovementEnemy MovementController => _movementController;
        public FinderOutside FinderOutside => _finderOutside;
        public FinderInside FinderInside => _finderInside;
        public FinderPlayer FinderPlayer => _finderPlayer;
        public DataTowers DataTowers => _dataTowers;
        public DataProgressComponent DataProgress => _dataProgress;

        public bool IsAggressive => _finderOutside.PlayerTower || FinderPlayer.IsPlayer;
        public bool HasBrick => Bag.CheckCount(0.5f);
        public bool IsCollection => FinderOutside.IsBlockItem && Bag.HasCanAdd;
        public bool IsNonState => (HasBrick == false || IsCollection == false) && _movementController.IsAtStart == false;
        public bool IsBuildTower => HasBrick && FinderOutside.IsTower;
        public bool IsBuild => HasBrick && (FinderOutside.IsBrick || (FinderOutside.IsNoBuilding && DataTowers.CanBuySomething));
    }
}