using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.DisplayParametrs
{
    public class WaveDisplay : MonoBehaviour
    {
        [SerializeField] private Image _slider;

        /*public Tween LaunchSlider(Color color,float duration)
        {
            _slider.color = color;
            _slider.fillAmount = 1;
            return _slider.DOFillAmount(0, duration);
        }*/
    }
}