using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class ManagerLocomotion : Manager, IDisableable
{
    private Rigidbody2D _rigidbody;
    public Rigidbody2D Rigidbody { get { return _rigidbody; } }

    private Vector2 _defaultDirection = Vector2.right;
    public Vector2 DefaultDirection { get { return _defaultDirection; } set { _defaultDirection = value; } }

    float _defaultSpeed;
    public float DefaultSpeed
    {
        get { return _defaultSpeed; }
        set { _defaultSpeed = value; }
    }

    private float _maxSpeed = 150f;
    public float MaxSpeed { get { return _maxSpeed; } set { _maxSpeed = value; } }

    Vector2 _lastDirection = Vector2.zero;
    public Vector2 LastDirection { get { return _lastDirection; } set { _lastDirection = value; } }

    private Vector2 _forcedDirection = Vector2.zero;
    public Vector2 ForcedDirection { get { return _forcedDirection; } set { _forcedDirection = value; } }

    public bool IsDirectionLocked
    {
        get
        {
            return _forcedDirection != Vector2.zero;
        }
        set
        {
            if (value) _forcedDirection = _lastDirection; else _forcedDirection = Vector2.zero;
        }
    }

    private Vector2 _force = Vector2.zero;
    private Vector2 _impulse = Vector2.zero;

    #region alteration_material

    private int _disablingStacks = 0;
    private bool _isEnabled = true;
    public bool IsEnabled
    {
        get { return _isEnabled; }
        set
        {
            if (_isEnabled) Reset();
            if (value == false) _disablingStacks++;
            else _disablingStacks--;
            _isEnabled = (_disablingStacks <= 0);
        }
    }

    public void Reset()
    {
        _force = Vector2.zero;
        _rigidbody.velocity = Vector2.zero;
    }

    #endregion

    void Awake()
    {
        _defaultSpeed = _maxSpeed;

        _rigidbody = gameObject.GetOrAdd<Rigidbody2D>();
        _rigidbody.gravityScale = 0;
        _rigidbody.drag = 10;
        _rigidbody.angularDrag = 0;
        _rigidbody.freezeRotation = true;
    }

    public void Move(bool isImpulse = false, bool ignoreDisabling = false)
    {
        Move(_defaultDirection, _defaultSpeed, isImpulse, ignoreDisabling);
    }

    public void Move(Vector2 direction, bool isImpulse = false, bool ignoreDisabling = false)
    {
        Move(direction, _defaultSpeed, isImpulse, ignoreDisabling);
    }

    public void Move(Vector2 direction, float speed, bool isImpulse = false, bool ignoreDisabling = false)
    {
        if ((!ignoreDisabling && !_isEnabled) || speed <= 0) return;

        if (_forcedDirection != Vector2.zero) _lastDirection = _forcedDirection;
        else if (direction != Vector2.zero) _lastDirection = direction.normalized;

        if (!isImpulse) _force += Vector2.ClampMagnitude(direction * speed, speed);
        else _impulse += Vector2.ClampMagnitude(direction * speed, speed);
    }

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, _lastDirection * 4f, Color.red);

        _rigidbody.AddForce(_force);
        _force = Vector2.zero;

        _rigidbody.AddForce(_impulse, ForceMode2D.Impulse);
        _impulse = Vector2.zero;
    }

    public Quaternion DirToRot(float offset)
    {
        Vector2 direction = _lastDirection;
        if (direction == Vector2.zero) direction = Vector2.down;

        Quaternion rotation = Quaternion.LookRotation(direction, transform.TransformDirection(-Vector3.forward));
        rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z + offset);
        return rotation;
    }

    public override string ToString()
    {
        string res = "";
        res += "Speed : " + _defaultSpeed + "/" + _maxSpeed;
        return res + "\n";
    }
}
