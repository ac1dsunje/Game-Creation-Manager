using System;
using UnityEngine;

namespace _Game.Scripts
{
    public class PlayerInput : MonoBehaviour
    {
        private InputSystem_Actions inputSystemActions;

        public event Action Pressed;

        private void Awake()
        {
            inputSystemActions = new InputSystem_Actions();

            inputSystemActions.Player.Press.started += ctx => Pressed?.Invoke();
        }

        private void OnEnable()
        {
            inputSystemActions.Enable();

            Pressed += Call; // Example, this should be in other script
        }

        private void OnDisable()
        {
            inputSystemActions.Disable();
        }

        private void Call() // This too
        {
            Debug.Log("Vizov Po Nazhatiyu");
        }
    }
}
