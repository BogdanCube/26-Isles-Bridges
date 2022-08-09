using System;
using DG.Tweening;
using NTC.Global.Pool;
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
        public void MoveToTower(Transform tower, float speed, Action callback)
        {
            transform.DOMove(tower.position, speed).OnComplete(callback.Invoke);
        }
    }
}