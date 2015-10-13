using UnityEngine;
using System.Collections;
/*
    Nom : FSM Sangleau
    Version : 0.2
    Créé par : Erwan Giry-Fouquet
    Description : 
    | FSM permettant à un sangleau de se déplacer vers le joueur, de le mordre,
    | et se de se repérer dans l'environnement en fonction du Manager_Sangleau
    Status : OK, à mettre à jour quand marc modifiera le système des colliders (2D => 3D)
*/
public class FSM_Sangleau : FsmStandard
{
    //Les balises délimitant la zone de repos du Sangleau
    public GameObject baliseGauche;
    public GameObject baliseDroite;

    //Le mâle alpha 
    public GameObject alpha;

    protected override void Awake()
    {
        _input = gameObject.GetOrAdd<Manager_Sangleau>();

        base.Awake();

        //Team : enemy ==> 1
        _collision.TeamIndex = 1;
        _collision.Damages = 1;

        //Vie : 2 PV
        _health.Init(2);

        // On fixe la vitesse et la hauteur du saut.
        _locomotion.Init(110f, 80f);


    }
}
