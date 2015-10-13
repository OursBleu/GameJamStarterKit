using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ManagerCollision : Manager
{
    // L'équipe de l'objet, les membres d'une même équipe ne peuvent pas s'infliger de dégâts
    // 0 : le héros, ses alliés et toutes leurs attaques
    // 1 : les pièges du décor, les monstres et toutes leurs attaques 
    //-1 : les objets qui ne peuvent pas recevoir de dégâts
    private int _teamIndex = -1;
    public int TeamIndex { get { return _teamIndex; } set { _teamIndex = value; } }

    // Les dégâts que l'utilisateur fait à un objet portant aussi se script
    private int _damages = 0;
    public int Damages { get { return _damages; } set { _damages = value; } }

    // Si oui ou non l'utilisateur est en contact avec un autre objet
    private bool _isColliding = false;
    public bool IsColliding { get { return _isColliding; } }


    // Le dernier objet avec lequel l'utilisateur est rentré en contact
    private Transform _lastCollidedObject = null;
    public Transform LastCollidedObject { get { return _lastCollidedObject; } }

    // Si oui ou non le dernier objet que l'utilisateur a touché possède aussi ce manager
    public bool HasOtherACollisionManager
    {
        get
        {
            if (_lastCollidedObject == null) return false;
            ManagerCollision otherCollisionManager = _lastCollidedObject.GetComponent<ManagerCollision>();
            return (otherCollisionManager != null);
        }
    }

    // Si oui ou non l'objet touché est dans une équipe adverse (on considère toujours qu'un objet de l'équipe -1 est dans son équipe)
    public bool IsOtherInDifferentTeam
    {
        get
        {
            if (_lastCollidedObject == null) return false;
            ManagerCollision otherCollisionManager = _lastCollidedObject.GetComponent<ManagerCollision>();
            if (!otherCollisionManager) return false;
            return (otherCollisionManager.TeamIndex != -1 && otherCollisionManager.TeamIndex != this.TeamIndex);
        }
    }

    private void StartCollision(GameObject other)
    {
        _isColliding = true;
        _lastCollidedObject = other.transform;
    }

    private void EndCollision()
    {
        _isColliding = false;
        _lastCollidedObject = null;
    }

    public bool SetCollisionInfos(Collider2D target)
    {
        // FIRST, CHECK IF THE TARGET GAMEOBJECT CAN HANDLE COLLISIONS

        ManagerCollision sourceCollisionManager = GetComponent<ManagerCollision>();
        ManagerCollision targetCollisionManager = target.GetComponent<ManagerCollision>();
        if (target == null) return false;
        if (!targetCollisionManager) return false;
        if (targetCollisionManager && sourceCollisionManager._teamIndex == targetCollisionManager._teamIndex) return false;

        // SET ALL THE TARGET COLLISION ATTRIBUTES TO REFERENCE THIS OBJECT

        targetCollisionManager._isColliding = true;
        targetCollisionManager._lastCollidedObject = transform;

        // RESET ALL THE TARGET COLLISION ATTRIBUTES ONCE THE INFORMATIONS HAVE BEEN PROCESSED

        targetCollisionManager.Invoke("EndCollision", 0.2f);

        return true;
    }

    public bool SetCollisionInfos(Collider2D[] targets)
    {
        bool atLeastOneHit = false;

        foreach (Collider2D target in targets)
        {
            if (SetCollisionInfos(target)) atLeastOneHit = true;
        }

        return atLeastOneHit;
    }

    void OnCollisionEnter2D(Collision2D collision) { StartCollision(collision.gameObject); }
    void OnCollisionStay2D(Collision2D collision) { StartCollision(collision.gameObject); }
    void OnCollisionExit2D(Collision2D collision) { EndCollision(); }
    void OnTriggerEnter2D(Collider2D collider) { StartCollision(collider.gameObject); }
    void OnTriggerStay2D(Collider2D collider) { StartCollision(collider.gameObject); }
    void OnTriggerExit2D(Collider2D collider) { EndCollision(); }

    public override string ToString()
    {
        string res = "Collision : ";
        res += "Team " + TeamIndex;
        if (LastCollidedObject != null)
        {
            res += " of " + LastCollidedObject.gameObject.name;
            res += " from " + (IsOtherInDifferentTeam ? "a different" : "the same") + " Team ";
        }
        return res + "\n";
    }
}
