using UnityEngine;
using System.Collections;

public class StateFireball : State
{
    protected float _shotSpeed;
    protected Vector2 _shotOffset;
    private GameObject _fireballPrefab;

    protected ManagerCollision _collision;

    public override void StateInit()
    {
        Duration = 0.25f;
        _shotSpeed = 150f;
        _shotOffset = 3f * Vector2.right;
        _fireballPrefab = Resources.Load<GameObject>("Objects/Fireball");
        _collision = Fsm.gameObject.GetOrAdd<ManagerCollision>();
    }

    public override void StateEnter()
    {
        Vector2 direction = new Vector2(Fsm.transform.localScale.normalized.x, 0f);
        Vector3 shotOffset = (_shotOffset.AsVector3() * direction.x); shotOffset.y = -Mathf.Abs(shotOffset.y);
        GameObject go = Fsm.Instantiate(_fireballPrefab, Fsm.transform.position + shotOffset, Quaternion.identity) as GameObject;

        InstantiatedProjectile projectile = go.GetComponent<InstantiatedProjectile>();
        projectile.Locomotion.DefaultSpeed = _shotSpeed;
        projectile.Collision.TeamIndex = _collision.TeamIndex;
        projectile.Collision.Damages = 1;

        SetupProjectile(projectile);
    }

    public override void StateUpdate()
    {
        
    }

    public override void StateExit()
    {

    }

    protected virtual void SetupProjectile(InstantiatedProjectile projectile)
    {
        projectile.Locomotion.DefaultDirection = new Vector2(Fsm.transform.localScale.normalized.x, 0f);
        projectile.transform.SetScaleX(Mathf.Sign(Fsm.transform.localScale.x) * projectile.transform.localScale.x);
    }
   
}
