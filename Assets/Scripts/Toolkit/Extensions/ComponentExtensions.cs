using UnityEngine;

namespace Toolkit.Extensions
{
    public static class ComponentExtensions
    {
        public static bool IsActive(this Component component)
        {
            return component.gameObject.activeSelf;
        }
        
        public static void Activate(this Component component)
        {
            component.gameObject.SetActive(true);
        }
        public static void Deactivate(this Component component)
        {
            component.gameObject.SetActive(false);
        }
    }
}