using UnityEngine;
using System;
using System.Collections.Generic;

public class Buff : Timer
{
    private GameObject _self;
    public GameObject Self { get { return _self; } set { _self = value; } }

    private bool _isPositive;
    public bool IsPositive { get { return _isPositive; } set { _isPositive = value; } }

    private bool _isClearable;
    public bool IsClearable { get { return _isClearable; } set { _isClearable = value; } }

    public Buff() : base() { }

    public Buff(GameObject self, float duration, bool isPositive=true) : base(duration)
    {
        _self = self;
        _isPositive = isPositive;
        _isClearable = true;
    }

    public override void Update()
    {
        base.Update();
        BuffUpdate();
    }

    public virtual void BuffEnter() { }
    public virtual void BuffUpdate() { }
    public virtual void BuffExit() { }

    
}
