using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingState : MainState
{
    private Vector2 _direction;
    private float _dragForce;

    public MovingState(CharacterStateMachine sm) : base(sm)
    {
        _dragForce = sm.GetDragForce();
    }

    public override void OnEnter()
    {
        _speed = _stateMachine.GetSpeed();
        Debug.Log("Entering MovingState");
        
    }
    
    public override void OnExit()
    {
        
    }
    
    public override bool CanEnter()
    {
        return !_stateMachine.CheckIfSticked() || !_stateMachine.CheckIfInAir();
    }
    
    public override bool CanExit()
    {
        return _stateMachine.CheckIfSticked() || _stateMachine.CheckIfInAir();
    }
    
    public override void OnUpdate()
    {
        Jump();
        _direction = GetInPutDirection();
    }
    
    public override void OnFixedUpdate()
    {
        _rigidbody.AddForce(_direction * (_speed* Time.fixedDeltaTime), ForceMode2D.Force);
        _rigidbody.AddForce(-_direction * (_dragForce * Time.fixedDeltaTime), ForceMode2D.Force);
        ClampVelocity();
        Debug.Log("Must move : " + (_direction * (_speed* Time.fixedDeltaTime)));


    }
    
    protected Vector2 GetInPutDirection()
    {
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            direction += Vector2.right;
        }
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            direction += Vector2.left;
        }
        
        return direction.normalized;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }

    private void ClampVelocity()
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -MAX_MOVEMENTSPEED, MAX_MOVEMENTSPEED);
        _rigidbody.velocity = velocity;
    }
    
}
