using Base.Level;
using Cinemachine;
using UnityEngine;

namespace Managers.Level
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private LoaderLevel _loaderLevel;

        private void Start()
        {
            _virtualCamera.Follow = _loaderLevel.CurrentPlayer.transform;
        }
    }
}