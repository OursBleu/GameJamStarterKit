using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillSword : AbstractSkill
{
    public override void SetupState()
    {
        base.SetupState();

        SetInfos(name: "Sword", key: 1);
        SetDelays(windup: 0.1f, threat: 0.4f, windown: 0.4f, cd: 0f);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        if (IsWindup && IsFirstFrame)
        {
            GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color.WithAlpha(0.5f);
        }
        else if (IsThreat)
        {
            GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color.WithAlpha(1f);
            gameObject.GetOrAdd<HaveParticles>().Play(HaveParticles.AreaEnum.cone, movement.LookDirection);
            Cone(transform.position, movement.LookDirection, 3f, 70f);
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