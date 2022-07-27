using UnityEngine;

namespace Core.Environment.Block
{
    public class Block : MonoBehaviour
    {
        public void SetPosition(Vector3 position)
        {
            transform.localPosition = position;
            transform.localScale = Vector3.one; 
        }
    }
}