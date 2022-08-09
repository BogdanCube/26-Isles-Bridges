using System;
using Core.Environment.Tower;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.DisplayParameters
{
    public class DisplaySpawner : MonoBehaviour
    {
        [SerializeField] private LoaderSpawnerTower _loaderSpawner;
        [SerializeField] private Image _image;
        #region Disable / Enable

        private void OnEnable()
        {
            _loaderSpawner.OnSpawn += StartSlider;
        }

        private void OnDisable()
        {
            _loaderSpawner.OnSpawn -= StartSlider;
        }

        private void StartSlider(int time)
        {
            _image.fillAmount = 0;
            _image.DOFillAmount(1, time).SetEase(Ease.InCubic).OnComplete(() =>
            {
                _loaderSpawner.Shake();
            });
        }

        #endregion
    }
}