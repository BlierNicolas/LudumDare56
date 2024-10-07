using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class CharacterStateMachine : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }

    private Collider m_collisionGroundChecker;

    [field: SerializeField] public float Speed { get; private set; } = 30.0f;
    [field: SerializeField] public float DragForce { get; private set; } = 20.0f;
    [field: SerializeField] public float RotationIncrement { get; private set; } = 90;
    [field: SerializeField] public float JumpPower { get; private set; } = 15;
    [field: SerializeField] public float RotationAngle { get; private set; } = 90.0f;
    [field: SerializeField] public GameObject footStepsGameObject { get; set; }
    public BaseState CurrentState { get; private set; }
    private bool _hasSpawnedAnotherTetramino = false;

    private float m_currentSpeed;
    public bool m_isInAir = false;
    private Transform m_objectToPlaceIn;
    private List<BaseState> m_States = new List<BaseState>();

    public bool ShowDebugLogState { get; private set; } = false;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();

        BuildStateMachine();
    }

    private void Start()
    {
        InitializeState(m_States[0]);
        GameManager.Instance.isPlayerActive = true;
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
        m_isInAir = CurrentState is InAirState;
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

        if (other.gameObject.layer == 6)
        {
            SoundManager.Instance.PlayStickSound();
            if (!_hasSpawnedAnotherTetramino)
            {
                GameManager.Instance.isPlayerActive = false;
                Destroy(GetComponent<CharacterStateMachine>());
                GameManager.Instance.SpawnNextTetramino();
                _hasSpawnedAnotherTetramino = true;
            }
        }
        else
        {
            SoundManager.Instance.PlayHitSound();
            Debug.Log("hit");
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        m_isInAir = true;

    }

    public bool CheckIfInAir()
    {
        if (Physics.Raycast(transform.position + (Vector3.down * 0.5f), Vector3.down, 0.5f))
        {
            return false;
        }

        return m_isInAir;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + (Vector3.down * 0.5f), Vector3.down * 0.5f);
    }
}