using UnityEngine;
using System.Collections;

public class Slow : NegativeBuff
{
    private float _parameter;
    public float Parameter { get { return _parameter; } set { _parameter = value; } }

    public Slow(GameObject self, float duration) : base(self, duration)
    {
        _parameter = 1f;
    }

    public Slow(GameObject self, float duration, float parameter) : base(self, duration)
    {
        _parameter = parameter;
    }

    HaveMovements _manager;
    float _speedDecrement;

    public override void BuffEnter()
    {
        _manager = Self.GetComponent<HaveMovements>();
        if (_manager)
        {
            _speedDecrement = _manager.MaxSpeed * Parameter;
            _manager.Speed -= _speedDecrement;
        }
    }

    public override void BuffExit()
    {
        if (_manager) _manager.Speed += _speedDecrement;
    }
}