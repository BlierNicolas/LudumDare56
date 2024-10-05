using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainState : IState
{
    protected const float MAX_MOVEMENTSPEED = 100f;
    protected Rigidbody2D _rigidbody;
    protected float _speed;
    protected CharacterStateMachine _stateMachine;
    protected float _jumpPower;
    
    public MainState(CharacterStateMachine sm)
    {
        _stateMachine = sm;
        _rigidbody = sm.GetRB();
        _speed = sm.GetSpeed();
        _jumpPower = sm.GetJumpPower();
    }
    public virtual void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public virtual bool CanEnter()
    {
        throw new System.NotImplementedException();
    }

    public virtual bool CanExit()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnUpdate()
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnFixedUpdate()
    {
        throw new System.NotImplementedException();
    }
}
