using DG.Tweening;
using UnityEngine;

namespace Toolkit.Extensions
{
    public static class DOTweenExtensions 
    {
        public static Tween DOLocalMoveArc(this Transform transform, Vector3 position,float timeIpUp,float timeGravity)
        {
            transform.DOLocalMoveX(position.x, timeIpUp).SetEase(Ease.OutQuad);
            transform.DOLocalMoveZ(position.z, timeGravity).SetEase(Ease.OutQuad);
            return transform.DOLocalMoveY(position.y, timeGravity).SetEase(Ease.InQuart);
        }
    }
}