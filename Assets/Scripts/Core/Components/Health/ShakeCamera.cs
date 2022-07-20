using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Components.Health
{
    public class ShakeCamera : MonoBehaviour
    {
        [SerializeField] private float _timeShake;
        [SerializeField] private float _intensityShake;
        
        [SerializeField] private CinemachineVirtualCamera _camera;
        private CinemachineBasicMultiChannelPerlin _noise;

        private void Start()
        {
            _noise = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin> ();
        }

        [ContextMenu("StartShake")]
        public void StartShake()
        {
            StartCoroutine(Noise());
        }
 
        private IEnumerator Noise()
        {            
            _noise.m_AmplitudeGain = 1;
            _noise.m_FrequencyGain = _intensityShake;
            
            yield return new WaitForSeconds(_timeShake);
            
            _noise.m_AmplitudeGain = 0;
            _noise.m_FrequencyGain = 0;
        }
    }
}