using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TimeBooster : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private void Start()
        {
            Time.timeScale = 1;
        }

        public void SetTime()
        {
            Time.timeScale = _slider.value;
        }
    }
}