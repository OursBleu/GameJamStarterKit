using UnityEngine;
using System.Collections;

public class CanDie : AbstractState
{
    protected HaveResources resource;
    protected HaveAnimations anim;
    protected HaveMovements movement;

    public override void StateEnter()
    {
        movement.ResetSpeed();
        anim.Play(HaveAnimations.Animations.idle);
        GetComponent<SpriteRenderer>().color = Color.black;
        GetComponent<SpriteRenderer>().sortingOrder = 1;
        GetComponent<Collider2D>().enabled = false;
    }

    public override void StateExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<SpriteRenderer>().sortingOrder = 2;
        GetComponent<Collider2D>().enabled = true;
    }

    public override void SetupTransitions()
    {
        CanMove movingState = GetComponent<CanMove>();
        TransitFromOtherState(movingState, NoHealthLeft);

        CanBeBumped bumpState = GetComponent<CanBeBumped>();
        CanBeInvincible invisState = GetComponent<CanBeInvincible>();
        if (bumpState) TransitFromOtherState(bumpState, bumpState._duration, NoHealthLeft);
        else if (invisState) TransitFromOtherState(invisState, NoHealthLeft);

        Transit(movingState, LifeRestored);
    }

    public bool LifeRestored()
    {
        return (!resource[GetComponent<CanBeHurt>()._damagedResource].IsEmpty());
    }

    public bool NoHealthLeft()
    {
        return (resource[GetComponent<CanBeHurt>()._damagedResource].IsEmpty());
    }

}