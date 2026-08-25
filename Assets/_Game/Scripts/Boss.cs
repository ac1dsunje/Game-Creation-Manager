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

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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
}
}