using UnityEngine;
using System.Collections;

public class StateMeteor : StateFireball
{
    protected float _shotAngle = 80f;

    public override void StateInit()
    {
        base.StateInit();

        Duration = 0.1f;
        _shotSpeed = 400f;
        _shotOffset = (Quaternion.Euler(0f, 0f, _shotAngle) * _shotOffset).AsVector2() + Vector2.right;
    }

    public override void StateEnter()
    {
        base.StateEnter();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    public override void StateExit()
    {
        base.StateExit();
    }

    protected override void SetupProjectile(InstantiatedProjectile projectile)
    {
        base.SetupProjectile(projectile);

        Vector2 dir = projectile.Locomotion.DefaultDirection;
        dir = Quaternion.Euler(0f, 0f, -dir.x * _shotAngle) * dir;
        projectile.Locomotion.DefaultDirection = dir;
    }
   
}
