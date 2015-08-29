using UnityEngine;
using System.Collections;

public class CanBeBumped : AbstractState
{
    public float _knockbackSpeed = 200f;
    public float _duration = 0.2f;
    Vector3 _impactPosition;
    Vector2 _knockbackDirection;

    protected HaveResources resource;
    protected HaveMovements movement;
    protected HaveCollisions collision;

    public override void StateEnter()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        // GET THE IMPACT DIRECTION FROM THE COLLISION MANAGER

        _knockbackDirection = collision.Impact;

        // SET DEFAULT IMPACT POSITION IF NONE REGISTERED (EX : IMPACT FROM RUNNING INTO HAZARD)

        if (_knockbackDirection == collision.NullPosition) _knockbackDirection = -movement.LookDirection;
    }

    public override void StateUpdate()
    {
        // LOOK IN THE IMPACT DIRECTION

        movement.LookDirection = -_knockbackDirection;

        // GO AWAY FROM THE IMPACT

        movement.Move(_knockbackDirection, _knockbackSpeed);
    }

    public override void StateExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public override void SetupTransitions()
    {
        // GET BUMPED AFTER BEING HURT

        CanBeHurt hurtState = GetComponent<CanBeHurt>();
        TransitFromOtherState(hurtState);
    }


}