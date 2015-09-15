using UnityEngine;
using System.Collections;

public class InstantiatedProjectile : Instantiated
{
    private ManagerLocomotion _locomotion;
    public ManagerLocomotion Locomotion { get { return _locomotion; } set { _locomotion = value; } }

    private ManagerCollision _collision;
    public ManagerCollision Collision { get { return _collision; } set { _collision = value; } }

    void Awake()
    {
        _locomotion = gameObject.GetOrAdd<ManagerLocomotion>();
        _collision = gameObject.GetOrAdd<ManagerCollision>();
    }

    void Update()
    {
        _locomotion.Move();
        gameObject.DestroyIfOutOfScreen(Screen.width, Screen.height, 0.2f);
    }

    
   
}
