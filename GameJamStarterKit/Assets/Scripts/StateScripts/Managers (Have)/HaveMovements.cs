using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class HaveMovements : AbstractManager
{
    public Rigidbody2D _rigidbody;
    public Vector2 _force = Vector2.zero;
    public Vector2 _impulse = Vector2.zero;

    float _speed = 80f;
    Vector2 _lookDirection = Vector2.zero;

    public float Speed { get { return _speed; } set { _speed = value; } }
    public Vector2 LookDirection { get { return _lookDirection; } set { _lookDirection = value; } }
    
    void Awake()
    {
        _rigidbody = gameObject.GetOrAdd<Rigidbody2D>();
        _rigidbody.drag = 10;
        _rigidbody.angularDrag = 0;
        _rigidbody.gravityScale = 0;
        _rigidbody.freezeRotation = true;
    }

    public void Move(Vector2 direction)
    {
        if (direction != Vector2.zero) _lookDirection = direction.normalized;
        _force += Vector2.ClampMagnitude(direction * _speed, _speed);
    }

    public void Move(Vector2 direction, float speed)
    {
        if (direction != Vector2.zero) _lookDirection = direction.normalized;
        _force += Vector2.ClampMagnitude(direction * speed, speed);
    }

    public void RushInDirection(Vector2 direction)
    {
        _impulse += direction * _speed;
    }

    public void ResetSpeed()
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
        Quaternion rotation = Quaternion.LookRotation(_lookDirection, transform.TransformDirection(-Vector3.forward));
        rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z + offset);
        return rotation;
    }
}
