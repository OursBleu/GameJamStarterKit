using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FsmPlayer : Fsm
{
    ManagerHealth _health;
    ManagerCollision _collision;
    IManagerInput _input;
    ManagerGroundProbe _ground;

    protected override void Awake()
    {
        base.Awake();

        _health = gameObject.GetOrAdd<ManagerHealth>(); _health.Init(10);
        _collision = gameObject.GetOrAdd<ManagerCollision>(); _collision.TeamIndex = 0;
        _input = gameObject.GetOrAdd<ManagerInputPlayer>();
        _ground = gameObject.GetOrAdd<ManagerGroundProbe>();

        int moving = AddState(new StateMoving(this));
        int jumping = AddState(new StateImpulse(this, Vector2.up, 80f));
        int falling = AddState(new StateFalling(this));
        int hit = AddState(new StateHit(this));
        int dead = AddState(new StateDead(this));
        int groundFireball = AddState(new StateProjectile(this));
        int airFireball = AddState(new StateMeteor(this));

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

        // feature 1 : fireball

        AddTransition(moving, groundFireball, () => { return _input[2]; });
        AddTransition(groundFireball, moving, () => { return CurrentState.IsOver && _ground.IsGrounded; });
        AddTransition(groundFireball, falling, () => { return CurrentState.IsOver && !_ground.IsGrounded; });

        // feature 2 : air fireball

        AddTransition(new[] { jumping, falling }, airFireball, () => { return _input[2]; });
        AddTransition(airFireball, falling, () => { return CurrentState.IsOver && !_ground.IsGrounded; });
        AddTransition(airFireball, moving, () => { return CurrentState.IsOver && _ground.IsGrounded; });
    }

}
