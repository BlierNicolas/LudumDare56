using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainState : IState
{
    protected const float MAX_MOVEMENTSPEED = 100f;
    protected Rigidbody2D m_rigidbody;
    protected float m_speed;
    protected CharacterStateMachine m_stateMachine;
    protected float m_jumpPower;
    
    public MainState(CharacterStateMachine sm)
    {
        m_stateMachine = sm;
        m_rigidbody = sm.GetRB();
        m_speed = sm.GetSpeed();
        m_jumpPower = sm.GetJumpPower();
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
