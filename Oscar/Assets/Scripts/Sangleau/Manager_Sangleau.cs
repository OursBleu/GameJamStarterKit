using UnityEngine;
using System.Collections;
/*
    Nom : Manager Sangleau
    Version : 0.2
    Créé par : Erwan Giry-Fouquet
    Description : 
    | IA (Manager) permettant à un sangleau de se déplacer vers le joueur, de le mordre,
    | et se de se repérer dans l'environnement
    Status : OK, à mettre à jour quand marc modifiera le système des colliders (2D => 3D)
*/
public class Manager_Sangleau : Manager, IManagerInput
{

    Transform playerTransform;
    Transform _gauche;
    Transform _droite;
    GameObject _myAlpha;


    //range of détection
    private float _range = 10f;
    private float _idleSpeedFactor = 0.4f;

    void Start()
        
    {
        //balises de zone de repos
        _gauche = GetComponent<FSM_Sangleau>().baliseGauche.transform;
        _droite = GetComponent<FSM_Sangleau>().baliseDroite.transform;

        //detection du mâle alpha
        _myAlpha = GetComponent<FSM_Sangleau>().alpha;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        FindNewTarget();
    }
    Vector2 _target = Vector2.zero;
    float _idleDuration = 0f;
    public Vector2 Direction
    {
        get
        {
            
            
            if (playerInRange())
            {
                return dirPlayer();
            }
            else
            {
                //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, 2f, 1<<LayerMask.NameToLayer("Ground"));
                //if (hit) FindNewTarget();
                //Debug.Log("t: " + _target.ToString());
                //Debug.Log("i: " + _idleDuration);
                if ((transform.position.AsVector2()-_target).magnitude<=0.25f)
                {
                    if (_idleDuration > 0) {
                        _idleDuration -= Time.deltaTime;
                        return Vector2.zero;
                    }
                    else FindNewTarget();
                }
                return (_target-transform.position.AsVector2()).normalized * _idleSpeedFactor;
            }
        }
    }
    void FindNewTarget()
    {
        _idleDuration = Random.value * 5 + 1; //Entre 1 et 6 secondes d'idle
        if (!_myAlpha)
        {

        
        //position aléatoire entre les deux balises
        _target = (Random.value * (_droite.position - _gauche.position) + _gauche.position).AsVector2();
        
        }
        else
        {
            //s'il y a un mâle alpha, on choisit une cible proche du mâle alpha
            _target = (8 * _myAlpha.GetComponent<Manager_Sangleau>()._target + 2 * (Random.value * (_droite.position - _gauche.position) + _gauche.position).AsVector2()) / 10;

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
    bool playerInRange()
    {
        bool result=false;
        //3D A implémenter plus tard
        /*RaycastHit hit;
        if (Physics.Raycast(transform.position, dirPlayer(),  out hit, 1 << LayerMask.NameToLayer("entities")))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                print("FOUNDED");
            }
            print("FOUNDED");
        }
        */
        //On check s'il y a une ligne de vision
        RaycastHit2D hit = Physics2D.Linecast(transform.position, playerTransform.position);
        bool visible = hit.transform == playerTransform;
        bool rangeOK = (distPlayer() < _range);
        float angle = Vector3.Angle(dirPlayer(), Vector3.right*transform.localScale.x);
        bool isFacing = angle > -80 && angle < 80;
        //Debug.DrawLine(transform.position, playerTransform.position);
        //Debug.Log(angle+" "+isFacing);
        result = rangeOK && visible && isFacing;
        return result;
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

