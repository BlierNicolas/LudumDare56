using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InAirState : MainState
{
    private Vector2 m_direction;

    public InAirState(CharacterStateMachine sm) : base(sm)
    {
    }

    public override void OnEnter()
    {
        m_speed = m_stateMachine.GetSpeed() / 2;
        Debug.Log("Entering AirState");
        
    }
    
    public override void OnExit()
    {
        Debug.Log("Exiting AirState");
    }
    
    public override bool CanEnter()
    {
        return !m_stateMachine.CheckIfSticked() || m_stateMachine.CheckIfInAir();
    }
    
    public override bool CanExit()
    {
        return m_stateMachine.CheckIfSticked() || !m_stateMachine.CheckIfInAir();
    }
    
    public override void OnUpdate()
    {
        RotateCharacter();
        m_direction = GetInPutDirection();
    }
    
    public override void OnFixedUpdate()
    {
        Debug.Log("OnFixedUpdate");
        m_rigidbody.AddForce(m_direction * (m_speed* Time.fixedDeltaTime), ForceMode2D.Force);
        ClampVelocity();

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
        
        return direction;
    }

    private void RotateCharacter()
    {
        Vector3 angle = Vector3.zero;
        
        if (Input.GetKeyDown(KeyCode.E) && !Input.GetKeyDown(KeyCode.Q))
        {
            angle.z += 45;
        }
        if (Input.GetKeyDown(KeyCode.Q) && !Input.GetKeyDown(KeyCode.E))
        {
            angle.z -= 45;

        }

        m_rigidbody.transform.Rotate(angle);

    }
    
    private void ClampVelocity()
    {
        Vector2 velocity = m_rigidbody.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -MAX_MOVEMENTSPEED, MAX_MOVEMENTSPEED);
        m_rigidbody.velocity = velocity;
    }
    
}