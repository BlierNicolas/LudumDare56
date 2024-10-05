using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingState : MainState
{
    private Vector2 m_direction;
    private float m_dragForce;

    public MovingState(CharacterStateMachine sm) : base(sm)
    {
        m_dragForce = sm.GetDragForce();
    }

    public override void OnEnter()
    {
        m_speed = m_sm.GetSpeed();
        Debug.Log("Entering MovingState");
        
    }
    
    public override void OnExit()
    {
        
    }
    
    public override bool CanEnter()
    {
        return !m_sm.CheckIfSticked() || !m_sm.CheckIfInAir();
    }
    
    public override bool CanExit()
    {
        return m_sm.CheckIfSticked() || m_sm.CheckIfInAir();
    }
    
    public override void OnUpdate()
    {
        Jump();
        m_direction = GetInPutDirection();
    }
    
    public override void OnFixedUpdate()
    {
        m_rb.AddForce(m_direction * (m_speed* Time.fixedDeltaTime), ForceMode2D.Force);
        m_rb.AddForce(-m_direction * (m_dragForce * Time.fixedDeltaTime), ForceMode2D.Force);
        ClampVelocity();
        Debug.Log("Must move : " + (m_direction * (m_speed* Time.fixedDeltaTime)));


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
            m_rb.AddForce(Vector2.up * m_jumpPower, ForceMode2D.Impulse);
        }
    }

    private void ClampVelocity()
    {
        Vector2 velocity = m_rb.velocity;
        velocity.x = Mathf.Clamp(velocity.x, -MAX_MOVEMENTSPEED, MAX_MOVEMENTSPEED);
        m_rb.velocity = velocity;
    }
    
}
