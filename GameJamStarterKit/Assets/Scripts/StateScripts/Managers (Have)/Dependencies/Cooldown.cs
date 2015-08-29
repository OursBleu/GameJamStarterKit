using UnityEngine;
using System;
using System.Collections.Generic;

public class Cooldown
{
    float _length;
    float _remaining;
    bool _isRunning;

    #region Properties

    public bool IsRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }

    public float Length
    {
        get { return _length; }
        set { _length = value; }
    }

    public float Remaining
    {
        get { return _remaining; }
    }

    public float Elapsed
    {
        get { return (_length - _remaining); }
    }

    public float Completion
    {
        get { return 1 - (_remaining / _length); }
    }

    public bool IsReady
    {
        get { return _remaining == 0f; }
    }

    #endregion

    public Cooldown(float length)
    {
        _length = length;
    }

    public void Update()
    {
        if (_isRunning) _remaining -= Time.deltaTime;

        if (_remaining < 0f)
        {
            _isRunning = false;
            _remaining = 0f;
        }
    }

    public void Restart()
    {
        _remaining = _length;
        _isRunning = true;
    }

    
}
