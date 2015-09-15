using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FsmPlayer : Fsm
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
            return _collision.IsColliding && _collision.IsOtherInDifferentTeam;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        _health = gameObject.GetOrAdd<ManagerHealth>(); _health.Init(10);
        _collision = gameObject.GetOrAdd<ManagerCollision>(); _collision.TeamIndex = 0;
        _input = gameObject.GetOrAdd<ManagerInputPlayer>();
        _locomotion = gameObject.GetOrAdd<ManagerLocomotion>();

        AddState("moving", new StateMoving());
        AddState("jumping", new StateJumping());
        AddState("hit", new StateHit());
        AddState("dead", new StateDead());
        AddState("fireball", new StateFireball());
        AddState("meteor", new StateMeteor());

        AddTransition(new[] { "hit", "dead" }, "hit", () => { return IsHit; }, true);
        AddTransition("moving", "dead", () => { return _health.IsEmpty; });
        AddTransition("moving", "jumping", () => { return _input[1]; });
        AddTransition("moving", "fireball", () => { return _input[2]; });
        AddTransition("jumping", "meteor", () => { return _input[2]; });
        AddTransition("jumping", "moving", () => { return _locomotion.IsGrounded; });
        AddTransition("hit", "dead", () => { return CurrentState.IsOver && _health.IsEmpty; });
        AddTransition("hit", "moving", () => { return CurrentState.IsOver; });
        AddTransition("dead", "moving", () => { return !_health.IsEmpty; });

        // transitions supplémentaires pour plus de réactivité en cas d'input très rapide

        AddTransition("fireball", "jumping", () => { return CurrentState.IsOver && !_locomotion.IsGrounded; });
        AddTransition("fireball", "moving", () => { return CurrentState.IsOver && _locomotion.IsGrounded; });
        AddTransition("meteor", "jumping", () => { return CurrentState.IsOver && !_locomotion.IsGrounded; });
        AddTransition("meteor", "moving", () => { return CurrentState.IsOver && _locomotion.IsGrounded; });
        
        SetFirstState("moving");
    }

}
