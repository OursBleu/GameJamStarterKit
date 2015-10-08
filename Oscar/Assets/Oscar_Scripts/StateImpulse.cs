using UnityEngine;
using System.Collections;

public class StateImpulse : State
{
    float _jumpForce = 80f; //4000f if force and not impulse
    Vector2 _direction = Vector2.up;

    ManagerLocomotion _locomotion;

    public StateImpulse(Fsm fsm, Vector2 direction, float jumpForce) : base(fsm)
    {
        _locomotion = Fsm.gameObject.GetOrAdd<ManagerLocomotion>();
        _direction = direction;
        _jumpForce = jumpForce;
    }

    public override void StateEnter()
    {
        _locomotion.Move(_direction, _jumpForce, true);
    }

    public override void StateUpdate()
    {
        
    }

    public override void StateExit()
    {

    }
   
}
