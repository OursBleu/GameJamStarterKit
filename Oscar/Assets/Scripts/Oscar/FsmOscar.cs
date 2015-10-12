using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FsmOscar : FsmStandard
{
    protected override void Awake()
    {
        _input = gameObject.GetOrAdd<ManagerInputPlayer>();

        base.Awake();

        _collision.TeamIndex = 0;

        _health.Init(10);

        _locomotion.Init(150f, 80f);

        int groundFireball = AddState(new StateProjectile(this, "Objects/Fireball", 0f, 0.25f));
        int airFireball = AddState(new StateProjectile(this, "Objects/Fireball", 80f, 0.3f));

        // feature 1 : fireball

        AddTransition(moving, groundFireball, () => { return _input[1]; });
        AddTransition(groundFireball, moving, () => { return CurrentState.IsOver && _ground.IsGrounded; });
        AddTransition(groundFireball, falling, () => { return CurrentState.IsOver && !_ground.IsGrounded; });

        // feature 2 : air fireball

        AddTransition(new[] { jumping, falling }, airFireball, () => { return _input[1]; });
        AddTransition(airFireball, falling, () => { return CurrentState.IsOver && !_ground.IsGrounded; });
        AddTransition(airFireball, moving, () => { return CurrentState.IsOver && _ground.IsGrounded; });
    }

}
