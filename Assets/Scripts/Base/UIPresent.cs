using NaughtyAttributes;
using UI.Screen;
using UnityEngine;

namespace Managers
{
    public class UIPresent : MonoBehaviour
    {
        [ShowNonSerializedField] private UIScreen _currentScreen;
        private void Awake()
        {
            DisableAllScreen();
        }

        private void DisableAllScreen()
        {
            var screens = FindObjectsOfType<UIScreen>();
            foreach (var screen in screens)
            {
                screen.gameObject.SetActive(false);
            }
        }

        public void SetScreen(UIScreen screen)
        {
            if (_currentScreen != null)
            {
                _currentScreen.Hide(screen.Show);
            }
            else
            {
                screen.Show();
            }
            _currentScreen = screen;
        }
    }
}