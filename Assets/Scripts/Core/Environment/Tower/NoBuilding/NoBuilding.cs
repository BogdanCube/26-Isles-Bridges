using System;
using Core.Environment.Island;
using DG.Tweening;
using Toolkit.Extensions;
using UI.DisplayParameters;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Environment.Tower.NoBuilding
{
    public class NoBuilding : MonoBehaviour
    {
        [SerializeField] private FreeIsland _freeIsland;
        [SerializeField] private DisplayDataTower _displayData;
        [SerializeField] private Collider _collider;
        public UnityEvent OnBuild;
        public FreeIsland FreeIsland => _freeIsland;
        public DisplayDataTower DisplayData => _displayData;

        public void Return()
        {
            transform.Activate();
            DisplayData.Deload();
            _collider.enabled = false;
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, 2.5f).OnComplete(() =>
            {
                _collider.enabled = true;
            });
        }

        public void BuildTower(Color color)
        {
            OnBuild?.Invoke();
            FreeIsland.SetColor(color,1f);
            transform.Deactivate();
        }
    }
}