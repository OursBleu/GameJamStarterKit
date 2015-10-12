using UnityEngine;
using System.Collections;
/*
    Nom : FSM Sangleau
    Version : 0.1
    Créé par : Erwan Giry-Fouquet
    Description : 
    | FSM permettant à un sangleau de se déplacer vers le joueur, de le mordre,
    | et se de se repérer dans l'environnement en fonction du Manager_Sangleau
    Status : OK
*/
public class FSM_Sangleau : FsmStandard
{
    protected override void Awake()
    {
        base.Awake();
        _input = gameObject.GetOrAdd<Manager_Sangleau>();


        base.Awake();

        //Team : enemy ==> 1
        _collision.TeamIndex = 1;

        //Vie : 2 PV
        _health.Init(2);

        // On fixe la vitesse et la hauteur du saut.
        _locomotion.Init(120f, 80f);



    }
}
