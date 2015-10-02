using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ManagerGroundProbe : Manager
{
    private Collider2D _collider;
    private Vector2 _colliderExtents;

    //private RaycastHit2D _groundProbe;

    public RaycastHit2D GroundProbe
    {
        get
        {
            Vector2 startPoint = _collider.bounds.center;
            float width = _colliderExtents.x;
            int layerMask = 1 << LayerMask.NameToLayer("Ground");

            RaycastHit2D ray = Physics2D.CircleCast(startPoint, width, Vector2.down, Mathf.Infinity, layerMask);
            Debug.DrawLine(startPoint, ray.point); //debug
            return ray;
        }

        //set { _groundProbe = value; }
    }

    private float _borneInf;
    private float _borneSup;

    public bool IsCloseToGround
    {
        get 
        {
            return (GroundProbe.distance < _borneSup);
        }

    }

    public bool IsGrounded
    {
        get
        {
            return (GroundProbe.distance <= _borneInf);
        }
    }

    private float _maximumWalkableAngle = 60f;

    public float Angle
    {
        get
        {
            return (Vector2.Angle(Vector2.up, GroundProbe.normal));
        }
    }

    public bool IsOnWalkableSlope
    {
        get
        {
            float angle = Angle;
            float facing = Mathf.Sign(transform.localScale.x);
            if ((facing > 0 && angle > -_maximumWalkableAngle) || (facing < 0 && angle < _maximumWalkableAngle))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _colliderExtents = _collider.bounds.extents;

        _borneInf = _colliderExtents.y + 0.01f;
        _borneSup = _colliderExtents.y + 0.1f;
    }

}
