using UnityEngine;

namespace _Game.Scripts.Boss
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class BossVisuals : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private MovementController _movement;

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            TryFlip(_movement.HorizontalInput);
            UpdateAnimator();
        }

        private void TryFlip(float input)
        {
            _spriteRenderer.flipX = input switch
            {
                > 0f => false,
                < 0f => true,
                _ => _spriteRenderer.flipX
            };
        }

        private void UpdateAnimator()
        {
            _animator.SetFloat("Horizontal", _playerInput.MoveInput.x);
            _animator.SetFloat("Vertical", _playerInput.MoveInput.y);
        }
    }
}