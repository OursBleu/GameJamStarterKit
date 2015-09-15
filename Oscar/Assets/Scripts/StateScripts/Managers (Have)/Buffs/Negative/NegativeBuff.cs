using UnityEngine;
using System.Collections;

public abstract class NegativeBuff : Buff
{

    public NegativeBuff(GameObject self, float duration) : base(self, duration)
    {
        IsPositive = false;
    }
}