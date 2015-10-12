using UnityEngine;
using System.Collections;

public class StateDead : State
{
    bool _isDestroyedByDeath;

    SpriteRenderer _renderer;

    public StateDead(Fsm fsm, bool isDestroyedByDeath = false) : base(fsm)
    {
        _renderer = Fsm.GetComponent<SpriteRenderer>();

        _isDestroyedByDeath = isDestroyedByDeath;
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
        if (!_isDestroyedByDeath)
        {
            _renderer.sortingOrder = 1;
            _renderer.color = Color.Lerp(Color.white, Color.black, 0f);
            Fsm.gameObject.layer = LayerMask.NameToLayer("Entity");
        }
        else
        {
            Fsm.Destroy(Fsm.gameObject);
        }
    }
   
}
