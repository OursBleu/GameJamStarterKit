using UnityEngine;
using System.Collections;

public class Mute : NegativeBuff
{
    public Mute(GameObject self, float duration) : base(self, duration)
    {
    }

    public override void BuffEnter()
    {
        IManagerInput input = Self.GetComponent<IManagerInput>();
        if (input != null) input.IsEnabled = false;
    }

    public override void BuffExit()
    {
        IManagerInput input = Self.GetComponent<IManagerInput>();
        if (input != null) input.IsEnabled = true;
    }
}