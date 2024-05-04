using UnityEngine;

public class AnimationController2D : MonoBehaviour
{
    [SerializeField] private float _animWalkSpeedMultipler, _animDashSpeedMultipler;
    private Animator _animator;
    private CharacterControllerBase _characterController;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterControllerBase>();
    }

    private void Update()
    {
        if (!_characterController.InDash)
        {
            if (_characterController.LookDirection != Vector2.zero)
                SetAnimDirection(_characterController.LookDirection);
            else if (_characterController.MoveDirection != Vector2.zero)
                SetAnimDirection(_characterController.MoveDirection);

            if (_characterController.MoveDirection != Vector2.zero)
                _animator.speed = _characterController.States.speed * _animWalkSpeedMultipler * _characterController.MoveDirection.magnitude;

            if (_characterController.MoveDirection.magnitude > 0.1)
            {
                _animator.SetBool("RunS", Mathf.Abs(_characterController.MoveDirection.x) > 0.5);
                _animator.SetBool("RunUD", !_animator.GetBool("RunS"));
            }
            else
            {
                _animator.SetBool("RunS", false);
                _animator.SetBool("RunUD", false);
            }
        }
    }
    private void SetAnimDirection(Vector2 direction)
    {
        float angle = Vector2.SignedAngle(Vector2.up, direction.normalized);

        _animator.SetBool("Up", angle >= -22.5f && angle < 22.5f);
        _animator.SetBool("Diagonal", (angle >= 22.5f && angle < 67.5f) || (angle >= -67.5f && angle < -22.5f));
        _animator.SetBool("Sideways", (angle >= 67.5f && angle < 157.5f) || (angle >= -157.5f && angle < -67.5f));
        _animator.SetBool("Down", angle < -157.5f || angle >= 157.5f);
        transform.localScale = new(angle < 0 ? 1 : -1, 1, 1);
    }

        

    private void StartDashAnim()
    {        
        SetAnimDirection(_characterController.DashDirection);
        _animator.SetBool("Dash", true);
        _animator.speed = _animDashSpeedMultipler / _characterController.States.dashTime;
    }
    private void StopDashAnim()
    {
        _animator.SetBool("Dash", false);
    }
    private void OnEnable()
    {
        _characterController.OnStartDash += StartDashAnim;
        _characterController.OnStopDash += StopDashAnim;
    }
    private void OnDisable()
    {
        _characterController.OnStartDash -= StartDashAnim;
        _characterController.OnStopDash -= StopDashAnim;
    }
}
