using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }

    private Collider m_collisionGroundChecker;

    [field: SerializeField] public float Speed { get; private set; } = 5.0f;
    [field: SerializeField] public float DragForce { get; private set; } = 2.5f;
    [field: SerializeField] public float RotationIncrement { get; private set; } = 90;
    [field: SerializeField] public float JumpPower { get; private set; } = 5;
    public IState CurrentState { get; private set; }
    
    
    private float m_currentSpeed;
    public bool m_isInAir = false;
    private Transform m_objectToPlaceIn;
    private List<BaseState> m_States = new List<BaseState>();

    public bool ShowDebugLogState { get; private set; } = false;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        
        BuildStateMachine();
        InitializeState(m_States[^1]); //End of list
    }

    private void BuildStateMachine()
    {
        // Order of importance
        m_States.Add(new InAirState(this));
        m_States.Add(new MovingState(this));
    }

    private void InitializeState(BaseState newState)
    {
        CurrentState = newState;
        CurrentState.OnEnter();
    }

    private void Update()
    {
        TryChangeState();
        
        CurrentState.OnUpdate();
    }

    private void FixedUpdate()
    {
        CurrentState.OnFixedUpdate();
    }

    private void TryChangeState()
    {
        if (!CurrentState.CanExit()) return;

        foreach (var state in m_States)
        {
            if (state == CurrentState || !state.CanEnter()) continue;
            
            ChangeState(state);
            return;
        }
    }

    private void ChangeState(BaseState newState)
    {
        CurrentState.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        m_isInAir = false;
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        m_isInAir = true;
    }

    public bool CheckIfInAir()
    {
        return m_isInAir;
    }
}
