using Inputs;
using Inputs.Data;
using UnityEngine;

public class InAirState : BaseState
{
    private Vector2 m_direction;

    public InAirState(CharacterStateMachine sm) : base(sm) {}
    
    public override bool CanEnter()
    {
        return true;
    }
    
    public override bool CanExit()
    {
        return true;
    }
    
    public override void OnUpdate()
    {
        RotateCharacter();
        m_direction = GetInputDirection();
    }
    
    public override void OnFixedUpdate()
    {
        m_sm.Rigidbody.AddForce(GetMovementAcceleration(m_direction, m_sm.Speed * 10), ForceMode2D.Force); //Should make 10 a parameter to change in the inspector
        
        ClampVelocity();
    }

    private void RotateCharacter()
    {
        Vector3 angle = new Vector3(0, 0,
            (InputManager.Instance.GetInput(EInputType.Rotate_Right) ? 45 :
                0) - (InputManager.Instance.GetInput(EInputType.Rotate_Left) ? -45 : 0));
        
        m_sm.Rigidbody.transform.Rotate(angle);
    }
    
    public override bool CanConsumeInput(EInputType inputType)
    {
        return inputType switch
        {
            EInputType.Walk_Left => true,
            EInputType.Walk_Right => true,
            EInputType.Rotate_Left => true,
            EInputType.Rotate_Right => true, //should probably add a timer so that we don't spin at 60 times per seconds
            _ => false
        };
    }
}
