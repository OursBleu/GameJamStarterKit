using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// Pour chaque nouveau personnage (que ce soit le joueur, un allié ou un ennemi) on doit créer une classe héritant de FsmStandard qu'on lui attachera ensuite
// Ce script lui permettra d'executer des actions de bases (bouger, sauter...) et permettra le programmeur d'ajouter facilement des capacités spéciales uniques
public class FsmToCopy : FsmStandard
{
    protected override void Awake()
    {
        // _input doit obligatoirement prendre la valeur d'une classe implémentant IManagerInput
        // Par exemple pour créer une IA, il faut créer un script du type ManagerInputSpider et le mettre dans _input
        _input = gameObject.GetOrAdd<ManagerInputPlayer>();

        // Appeler la fonction Awake de la classe parente lui permet de créer les états et transitions de bases
        // Nous aurons donc par défaut et pourrons utiliser les états moving, jumping, falling, hit et dead.
        base.Awake();

        // Un personnage doit avoir une équipe, les membres d'une même équipe ne peuvent pas se toucher avec leurs attaques
        // 0 représente le héros et ses alliés, 1 les monstres et -1 les personnages qui ne peuvent être blessés par personne
        _collision.TeamIndex = 0;

        // On fixe la valeur maximum de la vie.
        _health.Init(10);

        // On fixe la vitesse et la hauteur du saut.
        _locomotion.Init(150f, 80f);

        // On doit créer un état pour chaque attaque ou capacité possédé par le personnage
        // Par exemple pour créer une attaque, il faut créer un script du type StateFireball et le mettre dans un int
        int attaque1 = AddState(new StateToCopy(this));

        // Une fois que l'attaque est crée, il faut préciser comment est-ce qu'elle se lancera
        // Ici on crée une fonction qui dit que si le personnage décide de lancer l'action 1, il passe de déplacement à attaque
        AddTransition(moving, attaque1, () => { return _input[1]; });

        // Une fois que le personnage a attaqué, il faut qu'il retourne à son état de déplacement
        // Ici on crée une fonction qui dit que l'attaque est finie quand la durée définie à l'intérieur est dépassée
        AddTransition(attaque1, moving, () => { return CurrentState.IsOver; });
    }

}
