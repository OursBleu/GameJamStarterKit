using UnityEngine;
using System.Collections;
/*
    Nom : Manager Sangleau
    Version : 0.1
    Créé par : Erwan Giry-Fouquet
    Description : 
    | IA (Manager) permettant à un sangleau de se déplacer vers le joueur, de le mordre,
    | et se de se repérer dans l'environnement
    Status : OK
*/
public class Manager_Sangleau : Manager, IManagerInput
{
    Transform playerTransform;

    //range of détection
    private float _range = 10f;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        //Si le joueur est à portée de détection, se diriger vers lui, sinon ne pas bouger
        if (distPlayer() < _range)
        {
            _direction = dirPlayer();
        }
        else _direction = Vector2.zero;

    }

    private Vector2 _direction= Vector2.zero;
    public Vector2 Direction
    {
        get
        {
            return _direction;
        }
    }

  
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

    Vector2 vectPlayer()
    {
        return (playerTransform.position - gameObject.transform.position).AsVector2();
    }

    Vector2 dirPlayer()
    {
        return vectPlayer().normalized;
    }

    float distPlayer()
    {
        return vectPlayer().magnitude;
    }
}

