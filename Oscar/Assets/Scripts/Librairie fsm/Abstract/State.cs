using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public abstract class State
{
    private Fsm _fsm;
    public Fsm Fsm { get { return _fsm; } set { _fsm = value; } }

    private float _stateDuration = 0f;
    protected float StateDuration { get { return _stateDuration; } set { _stateDuration = value; } }
    public bool IsOver { get { return Fsm.ElapsedTime >= _stateDuration; } }

    public List<Func<State>> _transitions = new List<Func<State>>();
    public List<Func<State>> Transitions { get { return _transitions; } set { _transitions = value; } }
    public List<Action> _effects = new List<Action>();
    public List<Action> Effects { get { return _effects; } set { _effects = value; } }


    public State(Fsm fsm)
    {
        _fsm = fsm;
    }

    public virtual void StateEnter() { }
    public virtual void StateExit() { }
    public virtual void StateUpdate() { }

}