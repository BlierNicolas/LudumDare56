using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickedState : MainState
{
    public StickedState(CharacterStateMachine sm) : base(sm)
    {
    }
    
    public override void OnEnter()
    {
        Debug.Log("Entering Sticked");
    }
    
    public override void OnExit()
    {
        Debug.Log("Exiting Sticked");
    }
    
    public override bool CanEnter()
    {
        return m_stateMachine.CheckIfSticked();
    }
    
    public override bool CanExit()
    {
        return !m_stateMachine.CheckIfSticked();
    }
    
    public override void OnUpdate()
    {
    }
    
    public override void OnFixedUpdate()
    {

    }
}
