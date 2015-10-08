using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FsmOscar : FsmStandard
{
    protected override void Awake()
    {
        // attribuer un simulateur d'input qu'on a créé
        _input = gameObject.GetOrAdd<ManagerInputPlayer>();

        // appeler la fonction Awake de la classe parente qui va se charger de créer les états et transitions de bases
        base.Awake();

        // donner une équipe : 0 c'est le héros, 1 les monstres, -1 les neutres
        _collision.TeamIndex = 0;

        // fixer la valeur de la vie
        _health.Init(10);

        // fixer la vitesse et la puissance de saut
        _locomotion.Init(150f, 80f);

        // créer un état pour chaque skill qu'on a créé
        // précision : les états moving, jumping, falling, hit et dead sont ajoutés par défaut
        int groundFireball = AddState(new StateProjectile(this));
        int airFireball = AddState(new StateMeteor(this));

        // animations
        // AddEffect(moving, () => { _animator.SetInteger("state", 1); }

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
