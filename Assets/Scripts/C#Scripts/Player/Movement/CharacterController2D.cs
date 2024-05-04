using UnityEngine;

public class CharacterController2D : CharacterControllerBase
{
    private Rigidbody2D _rigidbody2D;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    protected override void Move() =>
        _rigidbody2D.velocity = MoveDirection * States.speed;
    
    protected override void Dash() =>
        _rigidbody2D.velocity = DashDirection * States.dashSpeed;
}