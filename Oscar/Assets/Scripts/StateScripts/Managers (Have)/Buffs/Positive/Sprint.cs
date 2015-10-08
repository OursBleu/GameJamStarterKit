using UnityEngine;
using System.Collections;

public class Sprint : NegativeBuff
{
    private float _parameter;
    public float Parameter { get { return _parameter; } set { _parameter = value; } }

    public Sprint(GameObject self, float duration) : base(self, duration)
    {
        _parameter = 1f;
    }

    public Sprint(GameObject self, float duration, float parameter) : base(self, duration)
    {
        _parameter = parameter;
    }

    HaveMovements _manager;
    float _speedIncrement;

    public override void BuffEnter()
    {
        _manager = Self.GetComponent<HaveMovements>();
        if (_manager)
        {
            _speedIncrement = _manager.MaxSpeed * Parameter;
            _manager.Speed += _speedIncrement;
        }
    }

    public override void BuffExit()
    {
        if (_manager)
        {
            _manager.Speed -= _speedIncrement;
        }
    }
}