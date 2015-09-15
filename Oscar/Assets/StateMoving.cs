using UnityEngine;
using System.Collections;

public class StateMoving : State
{

    ManagerLocomotion _locomotion;
    IManagerInput _input;

    public override void StateInit()
    {
        _locomotion = Fsm.gameObject.GetOrAdd<ManagerLocomotion>();
        _input = Fsm.gameObject.GetComponent<IManagerInput>();
    }

    public override void StateEnter()
    {

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

    }
   
}
