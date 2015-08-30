using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HaveCollisions : AbstractManager
{
    [SerializeField] int _teamIndex = -1;
    public int TeamIndex { get { return _teamIndex; } set { _teamIndex = value; } }

    bool _isColliding = false;
    public bool IsColliding { get { return _isColliding; } }

    Transform _other = null;
    public Transform Other { get { return _other; } }

    string _layer = "";
    public string Layer { get { return _layer; } }

    Vector2 _impact = Vector3.forward;
    public Vector2 Impact { get { return _impact; } }

    public Vector2 NullPosition { get { return Vector2.zero; } }

    void StartCollision(GameObject other)
    {
        _isColliding = true;
        _other = other.transform;
        _layer = LayerMask.LayerToName(other.layer);
        _impact = NullPosition;
    }

    void EndCollision()
    {
        _isColliding = false;
        _other = null;
        _layer = "";
        _impact = NullPosition;
    }

    public bool ForceCollision(Transform target, string layer, Vector3 impactInfos, bool directionOrPosition = false)
    {
        // FIRST, CHECK IF THE TARGET GAMEOBJECT CAN HANDLE COLLISIONS

        HaveCollisions otherCollisionManager = target.GetComponent<HaveCollisions>();
        if (otherCollisionManager == null || otherCollisionManager._teamIndex == this._teamIndex) return false;

        // SET ALL THE TARGET COLLISION ATTRIBUTES TO REFERENCE THIS OBJECT

        otherCollisionManager._isColliding = true;
        otherCollisionManager._other = this.transform;
        otherCollisionManager._layer = layer;

        if (directionOrPosition) otherCollisionManager._impact = impactInfos;
        else otherCollisionManager._impact = otherCollisionManager.transform.position - impactInfos;

        // RESET ALL THE TARGET COLLISION ATTRIBUTES ONCE THE INFORMATIONS HAVE BEEN PROCESSED

        otherCollisionManager.Invoke("EndCollision", 0.2f);

        return true;
    }

    public void ForceCollision(Transform[] targets, string layer, Vector3 impactPosition, bool PreviousParameterIsDirection = false)
    {
        foreach (Transform target in targets)
        {
            ForceCollision(target, layer, impactPosition, PreviousParameterIsDirection);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) { StartCollision(collision.gameObject); }
    void OnCollisionExit2D(Collision2D collision) { EndCollision(); }
    void OnTriggerEnter2D(Collider2D collider) { StartCollision(collider.gameObject); }
    void OnTriggerExit2D(Collider2D collider) { EndCollision(); }

}
