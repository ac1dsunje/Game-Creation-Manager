using UnityEngine;

namespace _Game.Scripts
{
[RequireComponent(typeof(Rigidbody2D))]
public class Boss: MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Rigidbody2D _rb;
    
    private Vector2 _moveDirection;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveDirection * _moveSpeed;
    }
}
}