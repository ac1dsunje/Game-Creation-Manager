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

    private Computer _currentComputer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_currentComputer != null)
            {
                _currentComputer.Toggle();
            }
        }
        
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        _renderer.flipX = horizontalInput switch
        {
            > 0.1f => false,
            < -0.1f => true,
            _ => _renderer.flipX
        };

        _moveDirection = new Vector2(horizontalInput, verticalInput);
    }

    private void FixedUpdate()
    {
        _movement.Move(_moveDirection * _moveSpeed);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Computer computer))
        {
            _currentComputer = computer;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Computer computer))
        {
            _currentComputer = null;
        }
    }
}
}