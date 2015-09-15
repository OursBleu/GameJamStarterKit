using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ManagerCollision : Manager, IDisableable
{
    #region Attributes_and_Properties

    [SerializeField]
    private int _teamIndex = -1;
    public int TeamIndex { get { return (_isEnabled ? _teamIndex : -1); } set { _teamIndex = value; } }

    private bool _isColliding = false;
    public bool IsColliding { get { return _isColliding; } }

    private Transform _lastCollidedObject = null;
    public Transform LastCollidedObject { get { return _lastCollidedObject; } }

    private int _damages;
    public int Damages { get { return _damages; } set { _damages = value; } }

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

    private Vector2 _impact = Vector3.forward;
    public Vector2 Impact { get { return _impact; } }

    bool _isOtherOwnCollider = true;
    public bool IsOtherOwnCollider { get { return _isOtherOwnCollider; } }

    private bool _isEnabled = true;
    public bool IsEnabled
    {
        get
        {
            return _isEnabled;
        }
        set
        {
            _isEnabled = false;
        }
    }

    #endregion

    public Vector2 NullPosition { get { return Vector2.zero; } }

    void StartCollision(GameObject other)
    {
        _isColliding = true;
        _lastCollidedObject = other.transform;
        _isOtherOwnCollider = true;
        _impact = NullPosition;
    }

    public void EndCollision()
    {
        _isColliding = false;
        _lastCollidedObject = null;
        _isOtherOwnCollider = true;
        _impact = NullPosition;
    }

    public static bool SetCollisionInfos(Transform source, Transform target, Vector3 impactInfos, bool directionOrPosition = false)
    {
        // FIRST, CHECK IF THE TARGET GAMEOBJECT CAN HANDLE COLLISIONS

        ManagerCollision sourceCollisionManager = source.GetComponent<ManagerCollision>();
        ManagerCollision targetCollisionManager = target.GetComponent<ManagerCollision>();
        if (target == null || source == null) return false;
        if (!targetCollisionManager) return false;
        if (targetCollisionManager && sourceCollisionManager._teamIndex == targetCollisionManager._teamIndex) return false;

        // SET ALL THE TARGET COLLISION ATTRIBUTES TO REFERENCE THIS OBJECT

        targetCollisionManager._isColliding = true;
        targetCollisionManager._lastCollidedObject = source;
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
            ManagerCollision.SetCollisionInfos(source, target, impactInfos, PreviousParameterIsDirection);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) { StartCollision(collision.gameObject); }
    void OnCollisionExit2D(Collision2D collision) { EndCollision(); }
    void OnTriggerEnter2D(Collider2D collider) { StartCollision(collider.gameObject); }
    void OnTriggerExit2D(Collider2D collider) { EndCollision(); }

    public override string ToString()
    {
        string res = "Collision : ";
        res += "Team " + TeamIndex;
        if (LastCollidedObject != null)
        {
            res += " => " + (IsOtherOwnCollider ? "body" : "skill");
            res += " of " + LastCollidedObject.gameObject.name;
            res += " from " + (IsOtherInDifferentTeam ? "a different" : "the same") + " Team ";
        }
        return res + "\n";
    }
}
