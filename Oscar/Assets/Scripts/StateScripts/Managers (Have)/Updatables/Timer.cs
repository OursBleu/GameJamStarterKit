using UnityEngine;
using System;
using System.Collections.Generic;

public class Timer : AbstractUpdatable
{
    float _duration;
    float _remaining;
    bool _isRunning;

    #region Properties

    public bool IsRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }

    public float Duration
    {
        get { return _duration; }
        set { _duration = value; }
    }

    public float Remaining
    {
        get { return _remaining; }
    }

    public float Elapsed
    {
        get { return (_duration - _remaining); }
    }

    public float Completion
    {
        get { return 1 - (_remaining / _duration); }
    }

    public bool IsReady
    {
        get { return _remaining == 0f; }
    }

    #endregion

    public Timer() { }

    public Timer(float duration)
    {
        _duration = duration;
    }

    public override void Update()
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
        _remaining = _duration;
        _isRunning = true;
    }

    
}
