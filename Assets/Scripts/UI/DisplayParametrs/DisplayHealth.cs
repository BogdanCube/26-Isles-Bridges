using Core.Components._ProgressComponents.Health;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.DisplayParametrs
{
    public class DisplayHealth : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _sliderHp;
        
        [Space(5)][SerializeField] private HealthComponent _healthComponent;

        private void OnEnable()
        {
            _healthComponent.OnUpdateHealth += UpdateHealthBar;
        }
        
        private void OnDisable()
        { 
            _healthComponent.OnUpdateHealth -= UpdateHealthBar;
        }

        
        private void LateUpdate()
        {
            transform.LookAt(_camera.transform);
        }
       
        
        private void UpdateHealthBar(int newHp)
        {
            _text.text = newHp.ToString();
            _sliderHp.DOFillAmount((float) newHp / _healthComponent.MaxCount, 0.3f);
        }
        
    }
}