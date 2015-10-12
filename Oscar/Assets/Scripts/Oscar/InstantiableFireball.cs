using UnityEngine;
using System.Collections;

public class InstantiableFireball : Instantiable
{
    private ManagerLocomotion _locomotion;
    private ManagerCollision _collision;

    void Awake()
    {
        _locomotion = gameObject.GetOrAdd<ManagerLocomotion>();
        _collision = gameObject.GetOrAdd<ManagerCollision>();

        _locomotion.DefaultSpeed = 300f;
        Grid.particleSystem.Play("ball", transform, transform.right, true);
    }

    void Update()
    { 
        ManagerCollision launcherCol = Launcher.GetComponent<ManagerCollision>();
        if (launcherCol) _collision.TeamIndex = launcherCol.TeamIndex;

        _locomotion.Move(transform.right);

        if (_collision.IsColliding && (!_collision.HasOtherACollisionManager || _collision.IsOtherInDifferentTeam))
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Grid.particleSystem.Play("explosion", transform.position);
    }

    
   
}
