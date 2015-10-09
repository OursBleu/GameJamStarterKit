using UnityEngine;
using System.Collections;

// Quoi qu'il fasse un personnage est toujours dans un état (déplacement, attaque...), on peut considérer un état comme une capacité
// On peut définir ici une capacité qui pourra être attachée à n'importe quel personnage depuis sa fsm
// Pour connecter un état et donc une capacité à une fsm, on fera : int quelquechose = AddState(new StateQuelquechose(this));
public class StateToCopy : State
{
    // On liste ici tout les managers et composants qui vont nous servir dans cet état
    ManagerLocomotion _locomotion;
    IManagerInput _input;

    // Le constructeur de l'état, appelé seulement une fois : au moment où on utilise AddState dans la fsm.
    // Utile pour référencer les manager ou des composants, et fixer la durée de l'état
    public StateToCopy(Fsm fsm) : base(fsm)
    {
        // GetOrAdd permet d'ajouter un composant s'il n'est pas déjà attaché, dans tous les cas il retourne le composant.
        _input = Fsm.gameObject.GetComponent<IManagerInput>();

        // Par contre on ne peut par ajouter une interface comme composant, dans ce cas on doit utiliser GetComponent.
        _locomotion = Fsm.gameObject.GetOrAdd<ManagerLocomotion>();

        // StateDuration représente la durée de l'état, la propriété IsOver vaudra true quand cette durée sera écoulée.
        // Cette propriété sert principalement pour écrire des transitions dans la fsm : pour sortir de l'état quand il est achevé.
        // Il peut être laissé à zero si on ne sort de l'état que pour des conditions indépendantes du temps (ex : vie < 10)
        StateDuration = 0f;
    }

    // Une fonction appelée à chaque fois qu'on entre dans l'état.
    // Utile pour les actions qui ne s'effectuent qu'une fois (ex : saut, tir, dégâts entrants)
    public override void StateEnter()
    {

    }

    // Une fonction appelée à chaque frame qu'on passe dans l'état
    // Utile pour les actions qui s'effectuent en continu (ex : déplacement)
    public override void StateUpdate()
    {
        
    }

    // Une fonction appelée à chaque fois qu'on sort de l'état.
    // Utile pour rétablir l'état de certaines variables à la fin de l'état (ex : remettre la couleur par défaut après l'avoir changé dans le StateEnter)
    public override void StateExit()
    {

    }

}
