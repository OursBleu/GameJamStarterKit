using UnityEngine;
using System.Collections;

public class StateMeteor : StateProjectile
{
    protected float _shotAngle = 80f;

    public StateMeteor(Fsm fsm) : base(fsm)
    {
        StateDuration = 0.3f;
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

    protected override void SetupProjectile(InstantiableProjectile projectile)
    {
        base.SetupProjectile(projectile);

        Vector2 dir = projectile.Locomotion.DefaultDirection;
        dir = Quaternion.Euler(0f, 0f, -dir.x * _shotAngle) * dir;
        projectile.Locomotion.DefaultDirection = dir;
    }
   
}
