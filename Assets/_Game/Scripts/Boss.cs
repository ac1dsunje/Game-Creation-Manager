using UnityEngine;

namespace _Game.Scripts
{
[RequireComponent(typeof(Rigidbody2D))]
public class Boss: MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private SpriteRenderer _renderer;
    private Rigidbody2D _rb;
    
    private Vector2 _moveDirection;

    private Computer _currentComputer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

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
        _rb.linearVelocity = _moveDirection * _moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Computer computer))
        {
            _currentComputer = computer;
        }
    }
}
}