using UnityEngine;

public class CharacterController3D : CharacterControllerBase
{
    private Rigidbody _rigidbody;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody>();
    }
    protected override void Move() =>
         _rigidbody.velocity = new Vector3(MoveDirection.x * States.speed, _rigidbody.velocity.y, MoveDirection.y * States.speed);

    protected override void Dash() =>
        _rigidbody.velocity = new Vector3(DashDirection.x * States.dashSpeed, _rigidbody.velocity.y, DashDirection.y * States.dashSpeed);
}