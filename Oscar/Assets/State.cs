using UnityEngine;
using System.Collections;

public abstract class State
{
    private Fsm _fsm;
    public Fsm Fsm { get { return _fsm; } set { _fsm = value; } }

    private float _duration;
    protected float Duration { get { return _duration; } set { _duration = value; } }
    public bool IsOver { get { return Fsm.ElapsedTime >= _duration; } }

    public virtual void StateInit() { }
    public virtual void StateEnter() { }
    public virtual void StateExit() { }
    public virtual void StateUpdate() { }

}