using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HaveCollisions : AbstractManager
{
    #region Attributes_and_Properties

    [SerializeField] int _teamIndex = -1;
    public int TeamIndex { get { return _teamIndex; } set { _teamIndex = value; } }

    bool _isColliding = false;
    public bool IsColliding { get { return _isColliding; } }

    Vector2 _impact = Vector3.forward;
    public Vector2 Impact { get { return _impact; } }

    Transform _other = null;
    public Transform Other { get { return _other; } }

    bool _isOtherOwnCollider = true;
    public bool IsOtherOwnCollider { get { return _isOtherOwnCollider; } }
    
    public bool IsOtherInDifferentTeam 
    { 
        get 
        {
            if (_other == null) return false;
            HaveCollisions otherCollisionManager = _other.GetComponent<HaveCollisions>();
            if (!otherCollisionManager) return false;
            return (otherCollisionManager.TeamIndex != this.TeamIndex); 
        }
    }

    #endregion

    public Vector2 NullPosition { get { return Vector2.zero; } }

    void StartCollision(GameObject other)
    {
        _isColliding = true;
        _other = other.transform;
        _isOtherOwnCollider = true;
        _impact = NullPosition;
    }

    public void EndCollision()
    {
        _isColliding = false;
        _other = null;
        _isOtherOwnCollider = true;
        _impact = NullPosition;
    }

    public static bool SetCollisionInfos(Transform source, Transform target, Vector3 impactInfos, bool directionOrPosition = false)
    {
        // FIRST, CHECK IF THE TARGET GAMEOBJECT CAN HANDLE COLLISIONS

        HaveCollisions sourceCollisionManager = source.GetComponent<HaveCollisions>();
        HaveCollisions targetCollisionManager = target.GetComponent<HaveCollisions>();
        if (target == null || source == null) return false;
        if (!targetCollisionManager) return false;
        if (targetCollisionManager && sourceCollisionManager._teamIndex == targetCollisionManager._teamIndex) return false;

        // SET ALL THE TARGET COLLISION ATTRIBUTES TO REFERENCE THIS OBJECT

        targetCollisionManager._isColliding = true;
        targetCollisionManager._other = source;
        targetCollisionManager._isOtherOwnCollider = false;

        if (directionOrPosition) targetCollisionManager._impact = impactInfos;
        else targetCollisionManager._impact = target.position - impactInfos;

        // RESET ALL THE TARGET COLLISION ATTRIBUTES ONCE THE INFORMATIONS HAVE BEEN PROCESSED

        targetCollisionManager.Invoke("EndCollision", 0.2f);

        return true;
    }

    public static void SetCollisionInfos(Transform source, Transform[] targets, Vector3 impactInfos, bool PreviousParameterIsDirection = false)
    {
        foreach (Transform target in targets)
        {
            HaveCollisions.SetCollisionInfos(source, target, impactInfos, PreviousParameterIsDirection);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) { StartCollision(collision.gameObject); }
    void OnCollisionExit2D(Collision2D collision) { EndCollision(); }
    void OnTriggerEnter2D(Collider2D collider) { StartCollision(collider.gameObject); }
    void OnTriggerExit2D(Collider2D collider) { EndCollision(); }

}
