using _Game.Scripts.Interactive;
using UnityEngine;

namespace _Game.Scripts.Boss
{
public class BossController : MonoBehaviour
{
    private PlayerInput _playerInput;

    private InteractiveObject _interactable;
    private bool _interacted;

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

    private void ReadInput()
    {
        _interactable?.Interact();
        if (_interactable != null)
        {
            _interacted = !_interacted;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out InteractiveObject interactable))
        {
            _interactable = interactable;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out InteractiveObject interactable))
        {
            if (_interacted)
            {
                _interactable.Interact();
                _interacted = false;
            }
            _interactable = null;
        }
    }
}
}