using System;
using DG.Tweening;
using Core.Character.Behavior;
using UnityEngine;

namespace Core.Player
{
    public class PlayerMovement : MovementController
    {
        [SerializeField] private Joystick _joystick;
        public override bool IsMove => GetMoveVector() != Vector3.zero;
        
        public override void Move()
        {
            Rotation(GetMoveVector());
            _navMeshAgent.Move(GetMoveVector() * _speed * Time.deltaTime);
        }
        private void Rotation(Vector3 moveVector)
        {
            if (!(Vector3.Angle(Vector3.forward, moveVector) > 1f) && Vector3.Angle(Vector3.forward, moveVector) != 0)
            {
                return;
            }
            
            var direct = Vector3.RotateTowards(transform.forward, moveVector, _speed, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }
        private Vector3 GetMoveVector()
        {
            Vector3 moveVector = Vector3.zero;
            moveVector.x = _joystick.Horizontal + Input.GetAxis("Horizontal");
            moveVector.z = _joystick.Vertical + Input.GetAxis("Vertical");

            return moveVector;
        }
    }
}
