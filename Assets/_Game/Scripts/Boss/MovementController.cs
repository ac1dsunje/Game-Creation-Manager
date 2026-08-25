using UnityEngine;

namespace _Game.Scripts.Boss
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PlayerInput))]
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;

        private PlayerInput _playerInput;
        private Rigidbody2D _rb;

        public float HorizontalInput { private set; get; }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _playerInput = GetComponent<PlayerInput>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            if (_playerInput == null) return;

            Vector2 inputDirection = _playerInput.MoveInput;
            _rb.linearVelocity = inputDirection * _moveSpeed;
        }
    }
}