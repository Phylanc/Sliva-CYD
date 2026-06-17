using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SlivaCYD1
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerInputHandler : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }
        public bool JumpPressed { get; private set; }
        public bool AttackPressed { get; private set; }

        public void OnAnimatorMove(InputAction.CallbackContext context)
        {
            if (context.performed)
                JumpPressed = true;
            if (context.canceled)
                JumpPressed = false;
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
                AttackPressed = true;
            if(context.canceled)
                AttackPressed = false;
        }
    }
}
