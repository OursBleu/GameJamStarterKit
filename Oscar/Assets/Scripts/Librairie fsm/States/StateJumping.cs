using UnityEngine;
using System.Collections;

public class StateJumping : State
{
    ManagerLocomotion _locomotion;

    public StateJumping(Fsm fsm) : base(fsm)
    {
        _locomotion = Fsm.gameObject.GetOrAdd<ManagerLocomotion>();
    }

    public override void StateEnter()
    {
        _locomotion.Move(Vector2.up, _locomotion.JumpForce, true);
    }

    public override void StateUpdate()
    {
        
    }

    public override void StateExit()
    {

    }
   
}
