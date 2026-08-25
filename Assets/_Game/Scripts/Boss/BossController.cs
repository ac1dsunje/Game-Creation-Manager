using UnityEngine;

namespace _Game.Scripts.Boss
{
    public class BossController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private MovementController _movement;

        private PlayerInput _playerInput;

        private IInteractable _interactable;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();    
        }

        private void OnEnable()
        {
            _playerInput.Pressed += ReadInput;
        }

        private void OnDisable()
        {
            _playerInput.Pressed -= ReadInput;
        }

        private void Update()
        {
            TryFlip(_movement.HorizontalInput);
        }

        private void ReadInput()
        {
            _interactable?.Interact();
        }

        public void TryFlip(float input)
        {
            _renderer.flipX = input switch
            {
                > 0.1f => false,
                < -0.1f => true,
                _ => _renderer.flipX
            };
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IInteractable interactable))
            {
                _interactable = interactable;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IInteractable interactable))
            {
                _interactable = null;
            }
        }
    }
}