using UnityEngine;
using System.Collections;

public class StateMoving : State
{
    ManagerLocomotion _locomotion;
    ManagerGroundProbe _ground;
    IManagerInput _input;

    public StateMoving(Fsm fsm) : base(fsm)
    {
        _locomotion = Fsm.gameObject.GetOrAdd<ManagerLocomotion>();
        _ground = Fsm.gameObject.GetOrAdd<ManagerGroundProbe>();
        _input = Fsm.gameObject.GetComponent<IManagerInput>();
    }

    public override void StateEnter()
    {

    }

    public override void StateUpdate()
    {
        RaycastHit2D groundProbe = _ground.GroundProbe;

        // MOVE

        Vector2 direction = _input.Direction.x * Fsm.transform.right;
        Fsm.transform.up = groundProbe.normal;
        _locomotion.Move(direction);

        // X SCALE INVERSION

        Vector3 scale = Fsm.transform.localScale;
        if ((direction.x < 0f && scale.x > 0f) || (direction.x > 0f && scale.x < 0f)) scale.x *= -1;
        Fsm.transform.localScale = scale;

        // GROUND HUGGING /!\ bug : se colle au plafond et aux murs quasi-verticaux, se clipse trop tôt aux pentes

        Collider2D col = Fsm.GetComponent<Collider2D>();
        if (_ground.IsCloseToGround && _input.Direction != Vector2.zero)
        {
            Fsm.transform.position = groundProbe.point + (col.bounds.extents.y * groundProbe.normal);
        }
    }

    public override void StateExit()
    {
        Fsm.transform.up = Vector3.up;
    }

}
