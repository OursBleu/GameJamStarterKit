using UnityEngine;
using System.Collections;

public class CanMove : AbstractState 
{
    public bool _isPlayer = true;
    Vector2 _inputDirection = Vector2.zero;

    protected HaveMovements movement;
    protected HaveAnimations anim;
    protected IInputManager input;
    //public IInputManager Input { get { return input; } }

    public override void SetupState()
    {
        //input = RequireSwitch(_isPlayer, typeof(HaveInputs), typeof(HaveIA)) as IInputManager;
        input = GetComponent<IInputManager>();
        if (input == null) input = gameObject.AddComponent<HaveInputs>();
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
}
