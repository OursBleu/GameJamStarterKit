using UnityEngine;
using System.Collections;

public class StateFalling : State
{
    PhysicsMaterial2D _defaultMaterial;
    PhysicsMaterial2D _material;
    Collider2D _collider;

    ManagerLocomotion _locomotion;
    IManagerInput _input;

    public StateFalling(Fsm fsm) : base(fsm)
    {
        _material = Resources.Load<PhysicsMaterial2D>("NoFriction");
        _collider = Fsm.gameObject.GetComponent<Collider2D>(); _collider.sharedMaterial = null;
        _locomotion = Fsm.gameObject.GetOrAdd<ManagerLocomotion>(); _locomotion.Rigidbody.gravityScale = 11f;
        _input = Fsm.gameObject.GetComponent<IManagerInput>();
    }

    public override void StateEnter()
    {
        _collider.sharedMaterial = _material;
        _locomotion.Rigidbody.gravityScale = 11f;
    }

    public override void StateUpdate()
    {
        // MOVE

        Vector2 direction = _input.Direction; direction.y = 0;
        _locomotion.Move(direction);

        // X SCALE INVERSION

        Vector3 scale = Fsm.transform.localScale;
        if ((direction.x < 0f && scale.x > 0f) || (direction.x > 0f && scale.x < 0f)) scale.x *= -1;
        Fsm.transform.localScale = scale;
    }

    public override void StateExit()
    {
        _collider.sharedMaterial = null;
        _locomotion.Rigidbody.gravityScale = 0f;
    }

}
