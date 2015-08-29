using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillFireBall : AbstractSkill
{
    public AbstractInvocation _prefab;
    AbstractInvocation _invocation;

    public override void SetupState()
    {
        base.SetupState();

        SetInfos(name: "FireBall", key: 2);
        SetDelays(windup: 0.2f, threat: 0.1f, windown: 0.5f, cd: 0f);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        if (IsWindup && IsFirstFrame)
        {
            GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color.WithAlpha(0.5f);
        }
        else if (IsThreat && IsFirstFrame)
        {
            GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color.WithAlpha(1f);
            _invocation = Instantiate(_prefab, transform.position + movement.LookDirection.AsVector3(), movement.DirToRot(90)) as AbstractInvocation;
            _invocation.Launcher = this.transform;
            _invocation.TeamIndex = this.collision.TeamIndex;
            _invocation.LifeTime = 5f;
        }
        else if (IsWindown && IsFirstFrame)
        {
            GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color.WithAlpha(0.5f);
        }
    }

    public override void StateExit()
    {
        base.StateExit();

        GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color.WithAlpha(1f);
    }
}