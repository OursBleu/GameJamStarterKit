using UnityEngine;
using System.Collections;

public class HaveIA : AbstractManager, IInputManager
{
    public Transform target;
    public float distanceToTarget;
    public Vector2 targetDirection;

    public Vector2 Direction
    {
        get
        {
            // UPDATING PERCEPTIONS

            target = GameObject.FindGameObjectWithTag("Player").transform;
            distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            targetDirection = (target.transform.position - transform.position).normalized;

            // TAKING ACT

            if (distanceToTarget < 3.5f) return -targetDirection * 0.1f;
            else return Vector2.zero;
        }
    }

    public bool this[int index]
    {
        get
        {
            return false;
        }
    }
}
