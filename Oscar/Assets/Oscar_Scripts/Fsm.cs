using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Fsm : MonoBehaviour
{
    private int _lastUsedIndex = 0;

    private Dictionary<int, State> _states;
    protected Dictionary<int, State> States { get { return _states; } set { _states = value; } }

    private State _currentState;
    protected State CurrentState { get { return _currentState; } set { _currentState = value; } }

    private State _nextState;

    private float _elapsedTime;
    public float ElapsedTime { get { return _elapsedTime; } }

    protected virtual void Awake()
    {
        _states = new Dictionary<int, State>();
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


    protected int AddState(State value)
    {
        int _currentIndex = _lastUsedIndex;
        _lastUsedIndex += 1;

        _states.Add((_currentIndex), value);
        value.Fsm = this;

        return (_currentIndex);
    }

    protected void AddTransition(int stateIndex, int nextStateIndex, Func<bool> test)
    {
        Func<State> transition = () => { if (test()) return States[nextStateIndex]; else return CurrentState; };
        States[stateIndex].Transitions.Add(transition);
    }

    protected void AddTransition(int[] statesIndex, int nextStateIndex, Func<bool> test, bool reverse=false)
    {
        Func<State> transition = () => { if (test()) return States[nextStateIndex]; else return CurrentState; };

        foreach (int stateIndex in States.Keys)
        {
            if (!reverse && Array.IndexOf(statesIndex, stateIndex) != -1) States[stateIndex].Transitions.Add(transition);
            else if (reverse && Array.IndexOf(statesIndex, stateIndex) == -1) States[stateIndex].Transitions.Add(transition);
        }
    }

    protected void AddEffect(int stateIndex, Action action)
    {
        States[stateIndex].Effects.Add(action);
    }

    protected void SetFirstState(int firstStateIndex)
    {
        _currentState = States[firstStateIndex];
        _currentState.StateEnter();
        ApplyEffects();
    }

    private void CheckForTransitions()
    {
        foreach (Func<State> transition in _currentState.Transitions)
        {
            _nextState = transition();
            if (_nextState != _currentState) break;
        }
    }

    private void AdvanceToNextState()
    {
        if (_nextState == _currentState) return;

        Debug.Log(_nextState);

        _currentState.StateExit();

        _elapsedTime = 0f;
        _currentState = _nextState;
        _nextState = null;

        _currentState.StateEnter();
        ApplyEffects();
    }

    private void ApplyEffects()
    {
        foreach (Action effect in _currentState.Effects)
        {
            effect();
        }
    }

}
