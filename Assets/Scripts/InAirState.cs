using Inputs;
using Inputs.Data;
using UnityEngine;

public class InAirState : BaseState
{
    private Vector2 m_direction;
    private float m_timer = 0.0f;
    private float m_rotationWanted = 0.0f;
    
    public InAirState(CharacterStateMachine sm) : base(sm) {}
    
    public override bool CanEnter()
    {
        return m_sm.CheckIfInAir();
    }

    public override void OnEnter()
    {
        Debug.Log("inAir");
        m_timer = 0.0f;
    }

    public override bool CanExit()
    {
        return !m_sm.CheckIfInAir();
    }
    
    public override void OnUpdate()
    {
        CalculateRotationAngle();
        m_direction = GetInputDirection();

        m_timer -= Time.deltaTime;
    }
    
    public override void OnFixedUpdate()
    {
        m_sm.Rigidbody.AddForce(GetMovementAcceleration(m_direction, m_sm.Speed * 10), ForceMode2D.Force); //Should make 10 a parameter to change in the inspector
        
        ClampVelocity();
        
        ApplyRotation();
    }

    private void CalculateRotationAngle()
    {
        var zAngle = (InputManager.Instance.GetInput(EInputType.Rotate_Right) ? m_sm.RotationAngle : 0) - 
                     (InputManager.Instance.GetInput(EInputType.Rotate_Left) ? m_sm.RotationAngle : 0);
        
        if (zAngle != 0)
        {
            m_timer = m_sm.RotationCooldown;
        }

        m_rotationWanted += zAngle;

        m_rotationWanted = Mathf.Repeat(m_rotationWanted, 360); //Clamps the value
    }

    private void ApplyRotation()
    {
        var newRotation = Mathf.MoveTowardsAngle(m_sm.Rigidbody.rotation, m_rotationWanted,
            m_sm.RotationSpeed * Time.fixedDeltaTime);
        m_sm.Rigidbody.MoveRotation(newRotation);
    }

    public override bool CanConsumeInput(EInputType inputType)
    {
        return inputType switch
        {
            EInputType.Walk_Left => true,
            EInputType.Walk_Right => true,
            EInputType.Rotate_Left => m_timer <= 0,
            EInputType.Rotate_Right => m_timer <= 0,
            _ => false
        };
    }
}
