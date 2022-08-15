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
        private Tween _tween;
        #region Disable / Enable

        private void OnEnable()
        {
            _loaderSpawner.OnSpawn += StartSlider;
            _loaderSpawner.OnReset += ResetSlider;
        }

       
        private void OnDisable()
        {
            _loaderSpawner.OnSpawn -= StartSlider;
            _loaderSpawner.OnReset -= ResetSlider;

        }

        #endregion
        
        private void StartSlider(int time)
        {
            _image.fillAmount = 0;
            _tween = _image.DOFillAmount(1, time).SetEase(Ease.InCubic).OnComplete(() =>
            {
                _loaderSpawner.Shake();
            });
        }

        private void ResetSlider(Action callback)
        {
            _tween.Kill();
            _image.DOFillAmount(0, 1).OnComplete(callback.Invoke);
        }

    }
}