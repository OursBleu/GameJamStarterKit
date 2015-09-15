using UnityEngine;
using System.Collections;

public class StateDead : State
{
    SpriteRenderer _renderer;

    public override void StateInit()
    {
        _renderer = Fsm.GetComponent<SpriteRenderer>();
    }

    public override void StateEnter()
    {
        _renderer.sortingOrder = 0;
        _renderer.color = Color.Lerp(Color.white, Color.black, 0.6f);
        Fsm.gameObject.layer = LayerMask.NameToLayer("Dead");
    }

    public override void StateUpdate()
    {

    }

    public override void StateExit()
    {
        _renderer.sortingOrder = 1;
        _renderer.color = Color.Lerp(Color.white, Color.black, 0f);
        Fsm.gameObject.layer = LayerMask.NameToLayer("Entity");
    }
   
}
