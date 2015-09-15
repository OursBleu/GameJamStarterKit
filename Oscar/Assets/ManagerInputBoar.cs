using UnityEngine;
using System.Collections;

public class ManagerInputBoar : Manager, IManagerInput, IDisableable
{
    public Transform target;
    public float distanceToTarget;
    public Vector2 targetDirection;

    private bool _isEnabled = true;
    public bool IsEnabled
    {
        get { return _isEnabled; }
        set { _isEnabled = value; }
    }

    public Vector2 Direction
    {
        get
        {
            // UPDATING PERCEPTIONS

            target = GameObject.FindGameObjectWithTag("Player").transform;
            distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            targetDirection = (target.transform.position - transform.position).normalized;

            // TAKING ACT

            if (distanceToTarget < 3.5f) return -targetDirection * 0.5f;
            else return Vector2.zero;
        }
    }

    public bool this[int index]
    {
        get
        {
            if (!_isEnabled) return false;

            return false;
        }
    }
}
