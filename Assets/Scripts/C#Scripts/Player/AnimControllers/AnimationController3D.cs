using UnityEngine;

public class AnimationController3D : MonoBehaviour
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
        if(!_characterController.InDash)
        {
            if(_characterController.LookDirection != Vector2.zero)
            {
                transform.LookAt(new Vector3(_characterController.LookDirection.x, transform.position.y, _characterController.LookDirection.y));
                Vector3 direction = transform.rotation * new Vector3(_characterController.MoveDirection.x, 0, _characterController.MoveDirection.y);
                _animator.SetFloat("MoveX", direction.x);
                _animator.SetFloat("MoveY", direction.z);
            }
            else if (_characterController.MoveDirection != Vector2.zero)
            {
                transform.LookAt(new Vector3(_characterController.MoveDirection.x, transform.position.y, _characterController.MoveDirection.y));
                _animator.SetFloat("MoveX", 0);
                _animator.SetFloat("MoveY", _characterController.MoveDirection.magnitude);
            }
            if (_characterController.MoveDirection != Vector2.zero)
                _animator.speed = _characterController.States.speed * _animWalkSpeedMultipler * _characterController.MoveDirection.magnitude;
            else
                _animator.speed = 1;
        }

    }
    private void StartDashAnim()
    {
        transform.LookAt(transform.position + new Vector3(_characterController.DashDirection.x, 0, _characterController.DashDirection.y));
        _animator.SetBool("Dash", true);
        _animator.speed =_animDashSpeedMultipler * _characterController.DashDirection.magnitude / _characterController.States.dashTime;
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
