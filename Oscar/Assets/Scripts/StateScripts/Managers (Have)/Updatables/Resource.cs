using UnityEngine;
using System;
using System.Collections.Generic;

public class Resource : AbstractUpdatable
{
    int _minValue;
    public int MinValue { get { return _minValue; } set { _minValue = value; } }
    int _maxValue;
    public int MaxValue { get { return _maxValue; } set { _maxValue = value; _currentValue = MaxValue; } }

    int _currentValue;
    public int Value { get { return _currentValue; } }

    public Resource() { }

    public Resource(int minValue, int maxValue)
    {
        _minValue = minValue;
        _maxValue = maxValue;
        _currentValue = maxValue;
    }

    public void Lose(int value)
    {
        _currentValue -= value;
        _currentValue = Mathf.Clamp(_currentValue, _minValue, _maxValue);
    }

    public void Gain(int value)
    {
        _currentValue += value;
        _currentValue = Mathf.Clamp(_currentValue, _minValue, _maxValue);
    }

    public void Fill()
    {
        _currentValue = _maxValue;
    }

    public bool IsEmpty()
    {
        return (_currentValue <= _minValue);
    }
}
