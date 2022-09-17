using Core.Characters._Base;
using Core.Characters.Enemy.Finder;
using Core.Components.DataTowers;
using UnityEngine;

namespace Core.Characters.Enemy
{
    public class Enemy : Character
    {
        [SerializeField] private MovementEnemy _movementController;
        [SerializeField] private FinderOutside _finderOutside;
        [SerializeField] private FinderInside _finderInside;
        [SerializeField] private FinderPlayer _finderPlayer;
        [SerializeField] private FinderTower _finderTower;
        [SerializeField] private DataTowers _dataTowers;
        [SerializeField] private DataProgressComponent _dataProgress;
        public MovementEnemy MovementController => _movementController;
        public FinderOutside FinderOutside => _finderOutside;
        public FinderInside FinderInside => _finderInside;
        public FinderPlayer FinderPlayer => _finderPlayer;
        public FinderTower FinderTower => _finderTower;
        public DataTowers DataTowers => _dataTowers;
        public DataProgressComponent DataProgress => _dataProgress;
    }
}