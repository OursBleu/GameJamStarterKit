using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;


public abstract class AbstractState : MonoBehaviour
{
    protected StatesManager state;
    public event Action OnTransition = () => { };

    protected virtual void Awake()
    {
        state = GetComponent<StatesManager>();
        if (!state) state = gameObject.AddComponent<StatesManager>();
    }

    public virtual void SetupState() { }
    public virtual void StateEnter() { }
    public virtual void StateExit() { }
    public virtual void StateUpdate() { }
    public virtual void SetupTransitions() { }

#region SetupTransitions()_methods

    public void Transit(AbstractState nextState)
    {
        if (nextState == null) return;
        Action transition = () => { state.NextState = nextState; };
        OnTransition += transition;
    }

    public void Transit(AbstractState nextState, float delay)
    {
        if (nextState == null) return;
        Action transition = () => { if (state.ElapsedTime >= delay) state.NextState = nextState; };
        OnTransition += transition;
    }

    public void Transit(AbstractState nextState, Func<bool> condition)
    {
        if (nextState == null) return;
        Action transition = () => { if (condition()) state.NextState = nextState; };
        OnTransition += transition;
    }

    public void Transit(AbstractState nextState, float delay, Func<bool> condition)
    {
        if (nextState == null) return;
        Action transition = () => { if (state.ElapsedTime >= delay - 0.001 && condition()) state.NextState = nextState; };
        OnTransition += transition;
    }

    public void TransitFromOtherState(AbstractState otherState)
    {
        if (otherState == null) return;
        Action transitionFromOtherState = () => { state.NextState = this; };
        otherState.OnTransition += transitionFromOtherState;
    }

    public void TransitFromOtherState(AbstractState otherState, float delay)
    {
        if (otherState == null) return;
        Action transitionFromOtherState = () => { if (state.ElapsedTime >= delay) state.NextState = this; };
        otherState.OnTransition += transitionFromOtherState;
    }

    public void TransitFromOtherState(AbstractState otherState, Func<bool> condition)
    {
        if (otherState == null) return;
        Action transitionFromOtherState = () => { if (condition()) state.NextState = this; };
        otherState.OnTransition += transitionFromOtherState;
    }

    public void TransitFromOtherState(AbstractState otherState, float delay, Func<bool> condition)
    {
        if (otherState == null) return;
        Action transitionFromOtherState = () => { if (state.ElapsedTime >= delay - 0.001 && condition()) state.NextState = this; };
        otherState.OnTransition += transitionFromOtherState;
    }

    protected AbstractManager Require(Type managerType)
    {
        Component manager = GetComponent(managerType);
        if (!manager) manager = gameObject.AddComponent(managerType);
        return manager as AbstractManager;
    }

    protected AbstractManager RequireSwitch(bool condition, Type defaultManager, Type alternativeManager)
    {
        return Require(condition ? defaultManager : alternativeManager);
    }

#endregion

#region StatesManager_methods

    public void OnTransitionProxy()
    {
        OnTransition();
    }

    public void BindManagersToFields()
    {
        // REPLACE { anim = Require(typeof(HaveAnimations)) as HaveAnimations; } IN SETUPSTATE()
        // ALL MANAGER FIELDS MUST BE "PROTECTED" TO AVOID UNITY WARNINGS

        FieldInfo[] fields = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (FieldInfo field in fields)
        {
            if (!field.FieldType.Name.Contains("Have") || field.FieldType.IsInterface) { }
            else
            {
                field.SetValue(this, GetComponent(field.FieldType));
                if (field.GetValue(this) == null) field.SetValue(this, gameObject.AddComponent(field.FieldType));
            }
        }
    }

#endregion

}