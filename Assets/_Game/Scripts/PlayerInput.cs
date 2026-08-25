using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class PlayerInput : MonoBehaviour
    {
        private InputSystem_Actions _inputSystemActions;

        public event Action Pressed;

        public Vector2 MoveInput { get; private set; }

        private void Awake()
        {
            _inputSystemActions = new InputSystem_Actions();

            _inputSystemActions.Player.Press.started += ctx => Pressed?.Invoke();

            _inputSystemActions.Player.Move.performed += ctx => MoveInput = ctx.ReadValue<Vector2>();
            _inputSystemActions.Player.Move.canceled += ctx => MoveInput = Vector2.zero;
        }

        private void OnEnable()
        {
            _inputSystemActions.Enable();
        }

        private void OnDisable()
        {
            _inputSystemActions.Disable();
        }
    }
}
