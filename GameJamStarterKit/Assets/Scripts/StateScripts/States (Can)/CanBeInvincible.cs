using UnityEngine;
using System.Collections;

public class CanBeInvincible : AbstractState
{
    public float _duration = 0.7f;
    float _blinkRate = 0.1f;

    public override void StateEnter()
    {
        StartCoroutine(Blink(_duration, _blinkRate));
    }

    public override void StateExit()
    {
        StopAllCoroutines();
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public override void StateUpdate()
    {
        GetComponent<CanMove>().StateUpdate();
    }

    public override void SetupTransitions()
    {
        CanBeBumped bumpState = GetComponent<CanBeBumped>();
        CanBeHurt hurtState = GetComponent<CanBeHurt>();
        if(bumpState) TransitFromOtherState(bumpState, bumpState._duration);
        else if (hurtState) TransitFromOtherState(hurtState);

        CanMove movingState = GetComponent<CanMove>();
        Transit(movingState, _duration);
    }

    IEnumerator Blink(float duration, float rate)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        float elapsedTime = 0f;
        while (elapsedTime < _duration)
        {
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(rate);
            elapsedTime += rate;
        }
        renderer.enabled = true;
    }


}