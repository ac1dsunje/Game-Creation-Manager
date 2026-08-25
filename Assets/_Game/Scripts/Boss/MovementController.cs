using UnityEngine;

namespace _Game.Scripts.Boss
{
[RequireComponent(typeof(Rigidbody2D))]
public class MovementController: MonoBehaviour
{
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {
        _rb.linearVelocity = direction;
    }
}
}