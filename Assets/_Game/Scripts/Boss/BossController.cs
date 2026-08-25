using UnityEngine;

namespace _Game.Scripts.Boss
{
public class BossController: MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private MovementController _movement;

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

        var horizontalInput = _movement.HorizontalInput;
        
        TryFlip(horizontalInput);
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