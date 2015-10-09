using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Tous les personnages du jeu sont contrôlés par un manette virtuelle
// Les ennemis simulent une direction du joystick et l'appui sur des boutons
// Leur IA doit donc leur dire dans quelle direction aller et dans quelle situation il doivent appuyer sur une touche
// On se contente de dire quel bouton est appuyé et non ce qu'il fait, on liera l'appui d'un bouton a une action dans la fsm du personnage
// Pour connecter un ManagerInput et donc une intelligence à une fsm, on fera : _input = GetOrAdd<ManagerInputQuelquechose>();
public class ManagerInputToCopy : Manager, IManagerInput
{
    // Ici on fixe la direction dans laquelle tente de se déplacer le personnage sous forme de vecteur (x,y)
    // Ex : Si vie du personnage > 10% alors aller vers la cible, sinon fuir
    public Vector2 Direction
    {
        get
        {
            return Vector2.zero;
        }
    }

    // Ici on dit quelle touche est enfoncée à cet instant précis (ex : si la touche 1 est pressée this[1] doit renvoyer vrai)
    // Ex : Si la cible est a portée d'attaque, le personnage peut vouloir appuyer sur 1
    public bool this[int index]
    {
        get
        {
            switch (index) 
            {
                case 0:
                    // 0 est une touche reservée pour le saut, retourner true quand le personnage veut sauter.
                    return false;
                case 1:
                    // Ex : si la cible est a portée d'attaque : return true, sinon : return false;
                    return false;
                case 2:
                    return false;
                case 3:
                    return false;
                default:
                    // ne pas changer ce qui est écrit dans le cas par défaut et ne rien rajouter
                    return false;
            }
        }
    }

}
