using UnityEngine;
using System;
using System.Collections.Generic;

public class ManagerHealth : Manager
{
    private int _maxValue = 1;
    public int MaxValue { get { return _maxValue; } set { _maxValue = value; Value = _currentValue; } }

    private int _currentValue;
    public int Value
    {
        get { return _currentValue; }
        set { _currentValue = value; Mathf.Clamp(_currentValue, 0, _maxValue); }
    }

    public bool IsEmpty { get { return (_currentValue <= 0); } }

    public void Init(int maxValue)
    {
        _maxValue = maxValue;
        Fill();
    }

    public void Fill()
    {
        _currentValue = _maxValue;
    }
    
}
