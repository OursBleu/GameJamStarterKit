using UnityEngine;
using System.Collections;

public class StateHit : State
{
    private float _knockbackSpeed = 600f;
    private Vector2 _knockbackDirection = Vector2.zero;

    ManagerLocomotion _locomotion;
    ManagerHealth _health;
    ManagerCollision _collision;

    public StateHit(Fsm fsm) : base(fsm)
    {
        StateDuration = 0.25f;
        _locomotion = Fsm.gameObject.GetOrAdd<ManagerLocomotion>();
        _health = Fsm.gameObject.GetOrAdd<ManagerHealth>();
        _collision = Fsm.gameObject.GetOrAdd<ManagerCollision>();
    }

    public override void StateEnter()
    {
        _health.Value -= _collision.LastCollidedObject.GetComponent<ManagerCollision>().Damages;
        _knockbackDirection = Vector2.up; // -_locomotion.LastDirection;
        _locomotion.Move(_knockbackDirection, _knockbackSpeed);
    }

    public override void StateUpdate()
    {
        
    }

    public override void StateExit()
    {

    }
   
}
