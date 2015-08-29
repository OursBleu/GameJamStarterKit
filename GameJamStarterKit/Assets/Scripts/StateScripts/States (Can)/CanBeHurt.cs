using UnityEngine;
using System.Collections;

public class CanBeHurt : AbstractState
{
    public string _damagingLayer = "Hazard";
    public string _damagedResource = "Health";

    protected HaveResources resource;
    protected HaveCollisions collision;
    protected HaveMovements movement;

    public override void SetupState()
    {
        resource.Add(_damagedResource, 0, 3);
    }

    public override void StateEnter()
    {
        resource[_damagedResource].Lose(1);
    }

    public override void SetupTransitions()
    {
        CanMove movingState = GetComponent<CanMove>();
        TransitFromOtherState(movingState, CollisionWithHazard);
    }

    bool CollisionWithHazard()
    {
        return (collision.IsColliding && _damagingLayer == collision.Layer);
    }
}