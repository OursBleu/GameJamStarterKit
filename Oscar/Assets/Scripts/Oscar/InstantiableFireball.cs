using UnityEngine;
using System.Collections;

public class InstantiableFireball : Instantiable
{
    private ManagerLocomotion _locomotion;
    private ManagerCollision _collision;

    void Awake()
    {
        _locomotion = gameObject.GetOrAdd<ManagerLocomotion>();
        _locomotion.DefaultSpeed = 300f;

        _collision = gameObject.GetOrAdd<ManagerCollision>();
        _collision.Damages = 1;

        Grid.particleSystem.Play("ball", transform, transform.right, true);
    }

    void Start()
    {
        ManagerCollision launcherCol = Launcher.GetComponent<ManagerCollision>();
        if (launcherCol) _collision.TeamIndex = launcherCol.TeamIndex;
    }

    void Update()
    { 
        _locomotion.Move(transform.right);
        gameObject.DestroyIfOutOfScreen(0.3f);

        if (_collision.IsColliding && (!_collision.HasOtherACollisionManager || _collision.IsOtherInDifferentTeam))
            DestroyProjectile();
    }

    void DestroyProjectile()
    {
        Grid.particleSystem.Play("explosion", transform.position);
        Destroy(gameObject);
    }
   
}
