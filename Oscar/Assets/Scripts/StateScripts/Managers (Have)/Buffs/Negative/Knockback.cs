using UnityEngine;
using System.Collections;

public class Knockback : NegativeBuff
{
    private float _parameter;
    public float Parameter { get { return _parameter; } set { _parameter = value; } }

    public Knockback(GameObject self, float duration) : base(self, duration)
    {
        _parameter = 300f;
    }

    public Knockback(GameObject self, float duration, float parameter) : base(self, duration)
    {
        _parameter = parameter;
    }

    HaveMovements _movement;
    ManagerInputPlayer _input;
    Vector2 _direction;

    public override void BuffEnter()
    {
        _movement = Self.GetComponent<HaveMovements>();
        if (_movement)
        {
            _movement.IsEnabled = false;
            _direction = -_movement.Direction;
            _movement.DirectionTarget = -_direction;
            if (_parameter < 0)
            {
                _direction = -_direction;
                _parameter = -_parameter;
            }
        }

        _input = Self.GetComponent<ManagerInputPlayer>();
        if (_input) _input.IsEnabled = false;

        Self.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    public override void BuffUpdate()
    {
        if (_movement) _movement.Move(_direction, _parameter, true);
    }

    public override void BuffExit()
    {
        if (_movement)
        {
            _movement.IsEnabled = true;
            _movement.DirectionTarget = Vector2.zero;
        }

        if (_input) _input.IsEnabled = true;

        Self.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Discrete;
    }
    
}