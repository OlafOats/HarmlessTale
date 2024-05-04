using System;
using UnityEngine;
using UnityEngine.InputSystem;
public abstract class CharacterControllerBase : MonoBehaviour
{
    public bool InDash { get; protected set; } = false;
    public Vector2 MoveDirection { get; private set; } = new();
    public Vector2 DashDirection { get; private set; } = new();
    public Vector2 LookDirection { get; private set; } = new();
    protected bool _canDash = true;
    protected Controls _controls;
    public PlayerCharacteristics States { get; private set; }
    public Action OnStartDash;
    public Action OnStopDash;

    protected virtual void Awake()
    {
        _controls = new Controls();
        _controls.Main.Move.performed += context => MoveDirection = context.ReadValue<Vector2>(); ;
        _controls.Main.Move.canceled += context => MoveDirection = Vector2.zero;
        _controls.Main.Look.performed += context => {
            LookDirection = context.ReadValue<Vector2>();
            if (Gamepad.current == null)
                LookDirection -= new Vector2(Screen.width / 2, Screen.height / 2);
        };
        _controls.Main.Look.canceled += _ => LookDirection = Vector2.zero;
        _controls.Main.Dash.performed += _ => StartDash();
        States = GetComponentInParent<PlayerCharacteristics>();
    }
    protected virtual void FixedUpdate()
    {
        if (!InDash)
        {
            Move();
        }
    }
    protected virtual void StartDash()
    {
        if (_canDash && MoveDirection != Vector2.zero)
        {            
            InDash = true; 
            DashDirection = MoveDirection;
            OnStartDash?.Invoke();
            Dash();
            Invoke(nameof(StopDash), States.dashTime);
        }
    }
    protected virtual void StopDash()
    {        
        InDash = false;
        OnStopDash?.Invoke();
        _canDash = false;
        Invoke(nameof(ReloadDash), States.timeBtwDash);
    }

    protected virtual void ReloadDash() => _canDash = true;
    protected abstract void Move();
    protected abstract void Dash();    
    protected virtual void OnEnable() => _controls.Enable();
    protected virtual void OnDisable() => _controls.Disable();
}