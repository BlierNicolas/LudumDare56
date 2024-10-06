using Inputs;
using Inputs.Data;
using UnityEngine;

public class BaseState : IState, IInputConsumer
{
    protected const float MAX_MOVEMENTSPEED = 100f;

    protected CharacterStateMachine m_sm;
    
    public BaseState(CharacterStateMachine sm)
    {
        m_sm = sm;
    }
    
    public virtual void OnEnter()
    {
        if (!m_sm.ShowDebugLogState) return;
        Debug.Log("Entering State :" + m_sm.CurrentState +  " " + Time.time);
    }

    public virtual void OnExit()
    {
        if (!m_sm.ShowDebugLogState) return;
        Debug.Log("Exiting State :" + m_sm.CurrentState + " " + Time.time);
    }

    public virtual bool CanEnter()
    {
        Debug.LogError("Currently in no state");
        return false;
    }

    public virtual bool CanExit()
    {
        Debug.LogError("Currently in no state");
        return false;
    }

    public virtual void OnUpdate() { }

    public virtual void OnFixedUpdate() { }
    
    protected Vector2 GetInputDirection()
    {
        Vector2 inputs =
            new Vector2(
                (InputManager.Instance.GetInput(EInputType.Walk_Right) ? 1 : 0) -
                (InputManager.Instance.GetInput(EInputType.Walk_Left) ? 1 : 0), 0); 
        
        return inputs.normalized;
    }

    protected Vector2 GetMovementAcceleration(Vector2 direction, float speed)
    {
        float force = speed * m_sm.Rigidbody.mass * Time.fixedDeltaTime;
        return direction * force;
    }

    protected Vector2 GetMovementDeceleration()
    {
        Vector2 velocity = m_sm.Rigidbody.velocity;
        float force = m_sm.DragForce * m_sm.Rigidbody.mass * Time.fixedDeltaTime;
        Vector2 deceleration = -velocity.normalized * force;
        
        if (deceleration.magnitude > velocity.magnitude)
        {
            deceleration = -velocity;
        }
            
        return deceleration;
    }

    protected void ClampVelocity()
    {
        var horizontalVelocity = new Vector2(m_sm.Rigidbody.velocity.x, 0);
        horizontalVelocity = Vector2.ClampMagnitude(horizontalVelocity, MAX_MOVEMENTSPEED);

        m_sm.Rigidbody.velocity = new Vector2(horizontalVelocity.x, m_sm.Rigidbody.velocity.y);
    }

    public virtual bool CanConsumeInput(EInputType inputType)
    {
        throw new System.NotImplementedException();
    }
}
