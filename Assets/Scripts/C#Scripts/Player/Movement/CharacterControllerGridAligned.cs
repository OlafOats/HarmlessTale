using UnityEngine;

public class CharacterControllerGridAligned : CharacterControllerBase
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _cellSize;

    protected override void Awake()
    {
        base.Awake();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }    
    protected override void Move()
    {
        _rigidbody2D.velocity = MoveDirection * States.speed;
        if(MoveDirection == Vector2.zero)
        {
            RoundPosition();
        }
    }
    private void RoundPosition()
    {
        Vector3 position = transform.position; 

        position.x = Mathf.Round(position.x / _cellSize) * _cellSize;
        position.y = Mathf.Round(position.y / _cellSize) * _cellSize;

        transform.position = position;
    }
    protected override void Dash() =>
        _rigidbody2D.velocity = DashDirection * States.dashSpeed;
}
