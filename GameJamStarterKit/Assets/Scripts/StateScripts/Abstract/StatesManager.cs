using UnityEngine;
using System;
using System.Collections;

public class StatesManager : AbstractManager {

    AbstractState _baseState;
    public AbstractState BaseState { get { return _baseState; } }

    AbstractState _currentState;
    public AbstractState CurrentState { get { return _currentState; } }

    float _elapsedTime;
    public float ElapsedTime { get { return _elapsedTime; } }

    AbstractState _nextState;
    public AbstractState NextState { set { _nextState = value; } }

    void Start()
    {
        ChooseFirstState();
        SetupStates();
    }

    void ChooseFirstState()
    {
        _baseState = gameObject.GetOrAdd<CanIdle>();
        _currentState = _baseState;
    }

    void SetupStates()
    {
        AbstractState[] states = GetComponents<AbstractState>();
        foreach (AbstractState state in states)
        {
            state.BindManagersToFields();
            state.SetupState();
            state.SetupTransitions();
        }
    }

    void AdvanceToNextState()
    {
        //Debug.Log(_currentState.GetType().Name + " => " + _nextState.GetType().Name);

        if (_nextState == _currentState) return;

        _currentState.StateExit();

        _elapsedTime = 0f;
        _currentState = _nextState;
        _nextState = null;

        _currentState.StateEnter();
    }

    void Update()
    { 
        _currentState.StateUpdate();
        _currentState.OnTransitionProxy();
        if (_nextState != null) AdvanceToNextState();
    }

    void LateUpdate()
    {
        _elapsedTime += Time.deltaTime;
    }
}
