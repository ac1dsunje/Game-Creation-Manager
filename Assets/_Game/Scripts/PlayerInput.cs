using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class PlayerInput : MonoBehaviour
    {
        private InputSystem_Actions _inputSystemActions;

        public event Action Pressed;

        private void Awake()
        {
            _inputSystemActions = new InputSystem_Actions();

            _inputSystemActions.Player.Press.started += ctx => Pressed?.Invoke();
        }

        private void OnEnable()
        {
            _inputSystemActions.Enable();

            Pressed += Call; // Example, this should be in other script
        }

        private void OnDisable()
        {
            _inputSystemActions.Disable();
        }

        private void Call() // This too
        {
            Debug.Log("Vizov Po Nazhatiyu");
        }
    }
}
