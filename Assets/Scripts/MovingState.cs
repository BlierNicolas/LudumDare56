using Inputs.Data;
using Managers;
using UnityEngine;

public class MovingState : BaseState
{
    private Vector2 m_direction;
    
    public MovingState(CharacterStateMachine sm) : base(sm) { }

    public override bool CanEnter()
    {
        return !m_sm.CheckIfInAir();
    }
    
    public override bool CanExit()
    {
        return m_sm.CheckIfInAir();
    }
    
    public override void OnUpdate()
    {
        Jump();
        m_direction = GetInputDirection();
        PlayFootStepsSounds();
    }

    private void PlayFootStepsSounds()
    {
        if (m_direction.x != 0)
        {
            if (!m_sm.footStepsGameObject.activeSelf)
            {
                m_sm.footStepsGameObject.SetActive(true);
                return;
            }
        }
        else
        {
            m_sm.footStepsGameObject.SetActive(false);
        }
    }
    
    public override void OnFixedUpdate()
    {
        m_sm.Rigidbody.AddForce(GetMovementAcceleration(m_direction, m_sm.Speed), ForceMode2D.Impulse);
        m_sm.Rigidbody.AddForce(GetMovementDeceleration(), ForceMode2D.Impulse);
        
        ClampVelocity();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.Instance.PlayJumpSound();
            m_sm.Rigidbody.AddForce(Vector2.up * m_sm.JumpPower, ForceMode2D.Impulse);
        }
    }

    public override bool CanConsumeInput(EInputType inputType)
    {
        return inputType switch
        {
            EInputType.Walk_Left => true,
            EInputType.Walk_Right => true,
            EInputType.Jump => !m_sm.CheckIfInAir(),
            _ => false
        };
    }
    
}
