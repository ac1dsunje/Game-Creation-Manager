using UnityEngine;

namespace _Game.Scripts.Boss
{
public class BossController: MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private MovementController _movement;
    private Rigidbody2D _rb;
    
    private Vector2 _moveDirection;

    private IInteractable _interactable;

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _interactable?.Interact();
        }
        
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        
        TryFlip(horizontalInput);
        _moveDirection = new Vector2(horizontalInput, verticalInput);
    }

    private void TryFlip(float input)
    {
        _renderer.flipX = input switch
        {
            > 0.1f => false,
            < -0.1f => true,
            _ => _renderer.flipX
        };
    }

    private void FixedUpdate()
    {
        _movement.Move(_moveDirection * _moveSpeed);
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