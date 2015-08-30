using UnityEngine;
using System.Collections;

public class CanMove : AbstractState 
{
    public bool _isPlayer = true;
    Vector2 _inputDirection = Vector2.zero;

    protected HaveMovements movement;
    protected HaveAnimations anim;
    protected IInputManager input;

    public override void SetupState()
    {
        input = GetComponent<IInputManager>();
    }

    public override void StateUpdate()
    {
        if (_inputDirection.x == 0f && _inputDirection.y == 0f)
            anim.Play(HaveAnimations.Animations.idle, movement.LookDirection);
        else
            anim.Play(HaveAnimations.Animations.walk, movement.LookDirection);

        _inputDirection = input.Direction;

        movement.Move(_inputDirection);
    }

    public override void SetupTransitions()
    {
        // GET BUMPED AFTER BEING HURT

        TransitFromOtherState(state.BaseState, IsMoving);
    }

    bool IsMoving()
    {
        return (input.Direction != Vector2.zero);
    }
}
