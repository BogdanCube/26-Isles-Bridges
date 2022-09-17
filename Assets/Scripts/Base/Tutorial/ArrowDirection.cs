using NTC.Global.Cache;
using Toolkit.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Base.Tutorial
{
    public class ArrowDirection : NightCache,INightLateRun
    {
        [SerializeField] private Image _image;
        private Transform _target;
        
        public void LateRun()
        {
            if (transform.DistanceToTarget(_target) > 3)
            {
                transform.LookAt(_target);
                _image.fillAmount = 1;
            }
            else
            {
                _image.fillAmount = 0;
            }
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}