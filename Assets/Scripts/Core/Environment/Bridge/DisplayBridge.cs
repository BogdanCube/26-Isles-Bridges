using System.Collections.Generic;
using Managers.Level;
using NTC.Global.Pool;
using UnityEngine;

namespace Core.Environment.Bridge
{
    public class DisplayBridge : MonoBehaviour
    {
        [SerializeField] private Bridge _bridge;
        [SerializeField] private Brick.Brick _prefab;
        private List<Brick.Brick> _bricks = new List<Brick.Brick>();
        
        #region Enable/Disable
        private void OnEnable()
        {
            _bridge.OnBuild += AddBrick;
        }

        private void OnDisable()
        {
            _bridge.OnBuild -= AddBrick;
        }
        #endregion

        private void AddBrick(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var brick =  NightPool.Spawn(_prefab, transform);
                brick.SetPosition(GetNextDistance());
                _bricks.Add(brick);
            }
        }
        
        private Vector3 GetNextDistance()
        {
            var distance = _bricks.Count * _bridge.Offset + _bridge.Offset;
            return new Vector3(0,0,distance);
        }
    }
}