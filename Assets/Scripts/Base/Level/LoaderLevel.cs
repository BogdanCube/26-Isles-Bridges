using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Managers.Level
{
    public class LoaderLevel : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface _navMeshSurface;
        public static LoaderLevel Instance;

        #region Singleton
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance == this)
            {
                Destroy(gameObject);
            }
        }

        #endregion
        
        [Button]
        public void UpdateBake()
        {
            _navMeshSurface.BuildNavMesh();
        }

        public void Load()
        {
            
            UpdateBake();
        }
        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}