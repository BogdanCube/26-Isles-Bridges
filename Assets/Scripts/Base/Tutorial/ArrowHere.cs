using DG.Tweening;
using NTC.Global.Cache;
using UnityEngine;
using UnityEngine.UI;

namespace Base.Tutorial
{
    [RequireComponent(typeof(Image))]
    public class ArrowHere : NightCache,INightLateRun
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float posY;
        [SerializeField] private float _duration = 1;
        private float _startPosY;
        private Image _arrow;
        private void Start()
        {
            _arrow = GetComponent<Image>();
            _startPosY = _arrow.transform.position.y;

            var sequence = DOTween.Sequence()
                .Append(_arrow.transform.DOMoveY(posY, _duration))
                .Append(_arrow.transform.DOMoveY(_startPosY, _duration));
            sequence.SetLoops(-1, LoopType.Yoyo);

        }
        public void LateRun()
        {
            transform.LookAt(_camera.transform);
        }
    }
}