using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FsmBoar : Fsm
{
    ManagerHealth _health;
    ManagerCollision _collision;
    IManagerInput _input;
    ManagerLocomotion _locomotion;

    bool IsHit
    {
        get
        {
            if (CurrentState == States["dead"]) return false;
            else if (CurrentState == States["hit"]) return false;
            return _collision.IsColliding && _collision.IsOtherInDifferentTeam && _collision.LastCollidedObject.tag != "Player";
        }
    }

    protected override void Awake()
    {
        base.Awake();

        _health = gameObject.GetOrAdd<ManagerHealth>(); _health.Init(3);
        _collision = gameObject.GetOrAdd<ManagerCollision>(); _collision.TeamIndex = 1; _collision.Damages = 2;
        _input = gameObject.GetOrAdd<ManagerInputBoar>();
        _locomotion = gameObject.GetOrAdd<ManagerLocomotion>();

        AddState("moving", new StateMoving());
        AddState("jumping", new StateJumping());
        AddState("hit", new StateHit());
        AddState("dead", new StateDead());

        AddTransition(new[] { "hit", "dead" }, "hit", () => { return IsHit; }, true);
        AddTransition("moving", "dead", () => { return _health.IsEmpty; });
        AddTransition("moving", "jumping", () => { return _input[1]; });
        AddTransition("jumping", "moving", () => { return _locomotion.IsGrounded; });
        AddTransition("hit", "dead", () => { return CurrentState.IsOver && _health.IsEmpty; });
        AddTransition("hit", "moving", () => { return CurrentState.IsOver; });
        AddTransition("dead", "moving", () => { return !_health.IsEmpty; });

        SetFirstState("moving");
    }

}
