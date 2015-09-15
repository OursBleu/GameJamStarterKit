using UnityEngine;
using System.Collections;

public abstract class PositiveBuff : Buff
{

    public PositiveBuff(GameObject self, float duration) : base(self, duration)
    {
        IsPositive = true;
    }
}