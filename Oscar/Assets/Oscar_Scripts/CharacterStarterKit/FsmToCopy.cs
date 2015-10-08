﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FsmToCopy : FsmStandard
{
    protected override void Awake()
    {
        base.Awake();

        // donner une équipe : 0 c'est le héros, 1 les monstres, -1 les neutres
        _collision.TeamIndex = 0;

        // attributer un simulateur d'input qu'on a créé
        _input = gameObject.GetOrAdd<ManagerInputPlayer>();

        // fixer la valeur de la vie
        _health.Init(10);

        // fixer la vitesse et la puissance de saut*
        _locomotion.Init(150f);

        // créer un état pour chaque skill qu'on a créé
        // précision : les états moving, jumping, falling, hit et dead sont ajoutés par défaut
        int skill1 = AddState(new StateProjectile(this));

        // créer deux transitions (moving=>skill, skill=>moving) pour chaque skill qu'on a ajouté en tant qu'état
        AddTransition(moving, skill1, () => { return _input[2]; });
        AddTransition(skill1, moving, () => { return CurrentState.IsOver && _ground.IsGrounded; });
    }

}
