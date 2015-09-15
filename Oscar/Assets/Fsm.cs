using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Fsm : MonoBehaviour
{

    private Dictionary<string, State> _states;
    protected Dictionary<string, State> States { get { return _states; } set { _states = value; } }

    private Dictionary<State, List<Func<State>>> _transitions;

    private State _currentState;
    protected State CurrentState { get { return _currentState; } set { _currentState = value; } }

    private State _nextState;

    private float _elapsedTime;
    public float ElapsedTime { get { return _elapsedTime; } }

    protected virtual void Awake()
    {
        _states = new Dictionary<string, State>();
        _transitions = new Dictionary<State, List<Func<State>>>();
    }

    protected virtual void Update()
    {
        _currentState.StateUpdate();
        CheckForTransitions();
        if (_nextState != null) AdvanceToNextState();
    }

    protected virtual void LateUpdate()
    {
        _elapsedTime += Time.deltaTime;
    }

    protected void AddState(string key, State value)
    {
        _states.Add(key, value);
        _transitions.Add(value, new List<Func<State>>());
        value.Fsm = this;
        value.StateInit();
    }

    protected void AddTransition(string currentState, string nextState, Func<bool> test)
    {
        Func<State> transition = () => { if (test()) return States[nextState]; else return CurrentState; };
        _transitions[_states[currentState]].Add(transition);
    }

    protected void AddTransition(string[] selectedStates, string nextState, Func<bool> test, bool reverse=false)
    {
        Func<State> transition = () => { if (test()) return States[nextState]; else return CurrentState; };

        foreach (string state in States.Keys)
        {
            if (!reverse && Array.IndexOf(selectedStates, state) != -1) _transitions[States[state]].Add(transition);
            else if (reverse && Array.IndexOf(selectedStates, state) == -1) _transitions[States[state]].Add(transition);
        }
    }

    protected void SetFirstState(string firstState)
    {
        _currentState = States[firstState];
        _currentState.StateEnter();
    }

    private void AdvanceToNextState()
    {
        if (_nextState == _currentState) return;

        Debug.Log(_currentState + " => " + _nextState);

        _currentState.StateExit();

        _elapsedTime = 0f;
        _currentState = _nextState;
        _nextState = null;

        _currentState.StateEnter();
    }

    private void CheckForTransitions()
    {
        foreach (Func<State> transition in _transitions[_currentState])
        {
            _nextState = transition();
            if (_nextState != _currentState) break;
        }
    }

}
