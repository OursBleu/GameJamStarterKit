using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class HaveMovements : Manager, IDisableable
{
    private Rigidbody2D _rigidbody;
    private Vector2 _force = Vector2.zero;
    private Vector2 _impulse = Vector2.zero;

    private float _speed = 100f;
    public float MaxSpeed { get { return _speed; } set { _speed = value; } }

    float _currentSpeed;

    public float Speed
    {
        get { return _currentSpeed; }
        set
        {
            _currentSpeed = value;
        }
    }

    Vector2 _lookDirection = Vector2.zero;
    public Vector2 Direction { get { return _lookDirection; } set { _lookDirection = value; } }

    private Vector2 _directionTarget = Vector2.zero;
    public Vector2 DirectionTarget { get { return _directionTarget; } set { _directionTarget = value; } }

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

#endregion

    void Awake()
    {
        _currentSpeed = _speed;
        _rigidbody = gameObject.GetOrAdd<Rigidbody2D>();
        _rigidbody.drag = 10;
        _rigidbody.angularDrag = 0;
        _rigidbody.gravityScale = 0;
        _rigidbody.freezeRotation = true;
    }

    public void Move(Vector2 direction, float speed, bool ignoreDisabling=false)
    {
        if ((!ignoreDisabling && !_isEnabled) || speed < 0) return;

        if (_directionTarget != Vector2.zero) _lookDirection = _directionTarget;
        else if (direction != Vector2.zero) _lookDirection = direction.normalized;

        _force += Vector2.ClampMagnitude(direction * speed, speed);
    }

    public void Move(Vector2 direction, bool ignoreDisabling = false)
    {
        Move(direction, _currentSpeed, ignoreDisabling);
    }

    public void RushInDirection(Vector2 direction)
    {
        if (!_isEnabled) return;

        _impulse += direction * _currentSpeed;
    }

    public void Reset()
    {
        _force = Vector2.zero;
        _impulse = Vector2.zero;
        _rigidbody.velocity = Vector2.zero;
    }

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, _lookDirection * 4f, Color.red);

        _rigidbody.AddForce(_force);
        _force = Vector2.zero;

        _rigidbody.AddForce(_impulse, ForceMode2D.Impulse);
        _impulse = Vector2.zero;
    }

    public Quaternion DirToRot(float offset)
    {
        Vector2 direction = _lookDirection;
        if (direction == Vector2.zero) direction = Vector2.down;

        Quaternion rotation = Quaternion.LookRotation(direction, transform.TransformDirection(-Vector3.forward));
        rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z + offset);
        return rotation;
    }

    public override string ToString()
    {
        string res = "";
        res += "Speed : " + _currentSpeed + "/" + _speed;
        return res + "\n";
    }
}
