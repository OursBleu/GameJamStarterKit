using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FsmStandard : Fsm
{
    protected ManagerHealth _health;
    protected ManagerCollision _collision;
    protected IManagerInput _input;
    protected ManagerGroundProbe _ground;
    protected ManagerLocomotion _locomotion;

    protected int moving, jumping, falling, hit, dead;

    protected override void Awake()
    {
        base.Awake();

        _ground = gameObject.GetOrAdd<ManagerGroundProbe>();
        _health = gameObject.GetOrAdd<ManagerHealth>();
        _collision = gameObject.GetOrAdd<ManagerCollision>();
        //_input = gameObject.GetOrAdd<ManagerInputPlayer>();
        _locomotion = gameObject.GetOrAdd<ManagerLocomotion>();

        moving = AddState(new StateMoving(this));
        jumping = AddState(new StateJumping(this));
        falling = AddState(new StateFalling(this));
        hit = AddState(new StateHit(this));
        dead = AddState(new StateDead(this));

        SetFirstState(falling);

        AddTransition(new[] {hit, dead}, hit, () => { return _collision.IsColliding && _collision.IsOtherInDifferentTeam; }, true);
        AddTransition(moving, dead, () => { return _health.IsEmpty; });
        AddTransition(moving, jumping, () => { return _input[1] && _ground.IsGrounded; });
        AddTransition(moving, falling, () => { return !_ground.IsGrounded || !_ground.IsOnWalkableSlope; });
        AddTransition(jumping, falling, () => { return CurrentState.IsOver; });
        AddTransition(falling, moving, () => { return _ground.IsGrounded; });
        AddTransition(hit, dead, () => { return CurrentState.IsOver && _health.IsEmpty; });
        AddTransition(hit, moving, () => { return CurrentState.IsOver; });
        AddTransition(dead, moving, () => { return !_health.IsEmpty; });

    }

}
