using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterStateMachine : MonoBehaviour
{
    private Rigidbody2D m_rb;

    private Collider m_collisionGroundChecker;

    [SerializeField] private float m_speed;

    [SerializeField] private float m_dragForce;

    [SerializeField] private float m_rotationIncrement = 90;
    [SerializeField] private float m_jumpPower = 5;

    public bool m_isSticked = false;
    public bool m_isInAir = false;
    
    private float m_currentSpeed;

    private IState m_currentState;

    private Transform m_objectToPlaceIn;

    private List<MainState> m_allStates = new List<MainState>();

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_allStates.Add(new MovingState(this));
        m_allStates.Add(new InAirState(this));

        m_currentState = m_allStates[0];
        m_currentState.CanEnter();

    }

    void Update()
    {
        m_currentState.OnUpdate();
        TransitionToNextState();
    }

    private void FixedUpdate()
    {
        m_currentState.OnFixedUpdate();
    }

    private void TransitionToNextState()
    {
        if (!m_currentState.CanExit())
        {
            return;
        }

        foreach (var state in m_allStates)
        {
            if (state == m_currentState || !state.CanEnter())
            {
                continue;
            }
            m_currentState.OnExit();
            m_currentState = state;
            m_currentState.OnEnter();
            return;
        }
    }

    public Rigidbody2D GetRB()
    {
        return m_rb;
    }

    public float GetSpeed()
    {
        return m_speed;
    }

    public float GetDragForce()
    {
        return m_dragForce;
    }

    public float GetJumpPower()
    {
        return m_jumpPower;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        m_isInAir = false;
        if (other.gameObject.layer == 3)
        {
            m_objectToPlaceIn = other.transform;
            transform.parent = m_objectToPlaceIn;
            m_isSticked = true;
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 3)
        {
            m_isSticked = false;
            return;
        }
        m_isInAir = true;
    }

    public bool CheckIfInAir()
    {
        return m_isInAir;
    }
    
    public bool CheckIfSticked()
    {
        return m_isSticked;
    }
}
