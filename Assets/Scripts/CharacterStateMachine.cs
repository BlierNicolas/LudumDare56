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

    [SerializeField] private float _speed;

    [SerializeField] private float _dragForce;

    [SerializeField] private float _rotationIncrement = 90;
    [SerializeField] private float _jumpPower = 5;

    public bool _isSticked = false;
    public bool _isInAir = false;
    
    private float _currentSpeed;

    private IState _currentState;

    private Transform _objectToPlaceIn;

    private List<MainState> _allStates = new List<MainState>();

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        _allStates.Add(new MovingState(this));
        _allStates.Add(new InAirState(this));

        _currentState = _allStates[0];
        _currentState.CanEnter();

    }

    void Update()
    {
        _currentState.OnUpdate();
        TransitionToNextState();
    }

    private void FixedUpdate()
    {
        _currentState.OnFixedUpdate();
    }

    private void TransitionToNextState()
    {
        if (!_currentState.CanExit())
        {
            return;
        }

        foreach (var state in _allStates)
        {
            if (state == _currentState || !state.CanEnter())
            {
                continue;
            }
            _currentState.OnExit();
            _currentState = state;
            _currentState.OnEnter();
            return;
        }
    }

    public Rigidbody2D GetRB()
    {
        return m_rb;
    }

    public float GetSpeed()
    {
        return _speed;
    }

    public float GetDragForce()
    {
        return _dragForce;
    }

    public float GetJumpPower()
    {
        return _jumpPower;
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        _isInAir = false;
        if (other.gameObject.layer == 3)
        {
            _objectToPlaceIn = other.transform;
            transform.parent = _objectToPlaceIn;
            _isSticked = true;
        }
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.layer == 3)
        {
            _isSticked = false;
            return;
        }
        _isInAir = true;
    }

    public bool CheckIfInAir()
    {
        return _isInAir;
    }
    
    public bool CheckIfSticked()
    {
        return _isSticked;
    }
}
