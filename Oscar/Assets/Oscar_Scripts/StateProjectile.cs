using UnityEngine;
using System.Collections;

public class StateProjectile : State
{
    protected float _shotSpeed;
    protected Vector2 _shotOffset;
    private GameObject _projectilePrefab;

    protected ManagerCollision _collision;

    public StateProjectile(Fsm fsm) : base(fsm)
    {
        StateDuration = 0.25f;
        _shotSpeed = 300f;
        _shotOffset = 3f * Vector2.right;
        _projectilePrefab = Resources.Load<GameObject>("Objects/Fireball");

        _collision = Fsm.gameObject.GetOrAdd<ManagerCollision>();
    }

    public override void StateEnter()
    {
        Vector2 direction = new Vector2(Fsm.transform.localScale.normalized.x, 0f);
        Vector3 shotOffset = (_shotOffset.AsVector3() * direction.x); shotOffset.y = -Mathf.Abs(shotOffset.y);
        GameObject go = Fsm.Instantiate(_projectilePrefab, Fsm.transform.position + shotOffset, Quaternion.identity) as GameObject;

        InstantiableProjectile projectile = go.GetComponent<InstantiableProjectile>();
        projectile.Locomotion.DefaultSpeed = _shotSpeed;
        projectile.Collision.TeamIndex = _collision.TeamIndex;
        projectile.Collision.Damages = 1;

        SetupProjectile(projectile);
        SetupParticles(projectile);
    }

    public override void StateUpdate()
    {
        
    }

    public override void StateExit()
    {

    }

    protected virtual void SetupProjectile(InstantiableProjectile projectile)
    {
        projectile.Locomotion.DefaultDirection = new Vector2(Fsm.transform.localScale.normalized.x, 0f);
        //projectile.transform.SetScaleX(Mathf.Sign(Fsm.transform.localScale.x) * projectile.transform.localScale.x);   
    }

    protected virtual void SetupParticles(InstantiableProjectile projectile)
    {
        Grid.particleSystem.Play("ball", projectile.transform, projectile.Locomotion.DefaultDirection, true);
    }
   
}
