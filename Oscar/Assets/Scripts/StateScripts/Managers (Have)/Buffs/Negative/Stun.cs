using UnityEngine;
using System.Collections;

public class Stun : NegativeBuff
{
    public Stun(GameObject self, float duration) : base(self, duration)
    {
    }

    public override void BuffEnter()
    {
        HaveMovements movement = Self.GetComponent<HaveMovements>();
        if (movement) movement.IsEnabled = false;

        IManagerInput input = Self.GetComponent<IManagerInput>();
        if (input != null) input.IsEnabled = false;
    }

    public override void BuffExit()
    {
        HaveMovements movement = Self.GetComponent<HaveMovements>();
        if (movement) movement.IsEnabled = true;

        IManagerInput input = Self.GetComponent<IManagerInput>();
        if (input != null) input.IsEnabled = true;
    }
}