using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractSkill : AbstractState
{

#region SkillInfos

    private string _name;
    private int _key;
    private float _cd;

    protected void SetInfos(string name, int key)
    {
        _name = name;
        _key = key;
        if (float.IsNaN(_cd) && !cooldown._cooldowns.ContainsKey(_name)) cooldown.Add(_name, _cd);
    }

    protected void SetDelays(float windup, float threat, float windown, float cd)
    {
        _windup = windup; _threat = threat; _windown = windown; _cd = windup + threat + windown + cd;
        if (_name != null && !cooldown._cooldowns.ContainsKey(_name)) cooldown.Add(_name, _cd);
    }

#endregion

#region SkillRythm

    private int _previousState;
    private int _currentState = -1;
    private bool _stateJustChanged;
    protected bool IsFirstFrame { get { return _stateJustChanged; } }

    private float _windup;
    protected bool IsWindup { get { return (0 <= state.ElapsedTime && state.ElapsedTime <= _windup); } }

    private float _threat;
    protected bool IsThreat { get { return (_windup < state.ElapsedTime && state.ElapsedTime <= _windup + _threat); } }

    private float _windown;
    protected bool IsWindown { get { return (_windup + _threat < state.ElapsedTime && state.ElapsedTime <= _windup + _threat + _windown); } }
    

#endregion

#region CollisionLayers

    private string _damagingLayer;

    private string _damageableLayer;
    private int _damageableLayerBit;
    protected string DamageableLayer
    {
        get 
        { 
            return _damageableLayer;
        }
        set 
        { 
            _damageableLayer = value; 
            _damageableLayerBit = 1 << LayerMask.NameToLayer(value); 
        }
    }

#endregion

    protected HaveMovements movement;
    protected HaveAnimations anim;
    protected HaveCollisions collision;
    protected IInputManager input;
    protected HaveCooldowns cooldown;

    public override void SetupState()
    {
        _damagingLayer = "Hazard";
        DamageableLayer = "Entity";
        input = GetComponent(typeof(IInputManager)) as IInputManager;
    }

    public override void StateEnter()
    {
        cooldown[_name].Restart();
    }

    public override void StateUpdate()
    {
        _previousState = _currentState;

        if (IsWindup) _currentState = 0;
        else if (IsThreat) _currentState = 1;
        else if (IsWindown) _currentState = 2;

        if (_currentState != _previousState) _stateJustChanged = true;
        else _stateJustChanged = false;
    }

    public override void StateExit()
    {

    }

    public override void SetupTransitions()
    {
        CanMove movingState = GetComponent<CanMove>();
        Transit(movingState, _windup + _threat + _windown);
        TransitFromOtherState(movingState, SkillKeyPressed);
    }

#region Child_Zone_methods

    protected void Cone(Vector3 origin, Vector2 direction, float length, float angle)
    {
        Transform[] hits = ConeCastAll(origin, movement.LookDirection, length, angle, _damageableLayerBit);
        collision.ForceCollision(hits, _damagingLayer, origin);
        
    }

    protected void Circle(Vector3 origin, float radius)
    {
        Transform[] hits = GetAllTransforms(Physics2D.OverlapCircleAll(origin, radius, _damageableLayerBit));
        collision.ForceCollision(hits, _damagingLayer, origin);
    }

    protected void Square(Vector3 origin, Vector2 direction, float length, float width)
    {
        Vector2 start = origin + Quaternion.Euler(0, 0, -90)*direction*(width/2);
        Vector2 end = origin + new Vector3(direction.x, direction.y, 0f)*length + Quaternion.Euler(0, 0, 90)*direction*(width/2);
        Transform[] hits = GetAllTransforms(Physics2D.OverlapAreaAll(start, end, _damageableLayerBit));
        collision.ForceCollision(hits, _damagingLayer, origin);
    }

#endregion

#region Private_Helpers_methods

    bool SkillKeyPressed()
    {
        return (input[_key] && cooldown[_name].IsReady);
    }

    Transform[] GetAllTransforms(RaycastHit2D[] hits)
    {
        Transform[] res = new Transform[hits.Length];
        for (int i = 0; i < hits.Length; i++) res[i] = hits[i].transform;
        return res;
    }

    Transform[] GetAllTransforms(Collider2D[] hits)
    {
        Transform[] res = new Transform[hits.Length];
        for (int i=0 ; i<hits.Length ; i++) res[i] = hits[i].transform;
        return res;
    }

    public Transform[] ConeCastAll(Vector3 position, Vector2 direction, float coneLength, float coneWidth, int layerMask)
    {
        Debug.DrawRay(position, Quaternion.Euler(0, 0, -coneWidth / 2) * direction * coneLength, Color.red);
        Debug.DrawRay(position, Quaternion.Euler(0, 0, coneWidth / 2) * direction * coneLength, Color.red);

        // CIRCLECASTING ALL

        Collider2D[] hits = Physics2D.OverlapCircleAll(position, coneLength, layerMask);
        List<Transform> inConeTransforms = new List<Transform>();
        foreach (Collider2D hit in hits)
        {
            Debug.DrawLine(position, hit.transform.position, Color.yellow);

            // CHECKING IF EACH HIT IS IN CONE

            Vector2 targetDir = (hit.transform.position - position).normalized;
            float angleDiff = Vector2.Angle(targetDir, direction);
            bool isInCone = (angleDiff <= coneWidth / 2);
            if (isInCone) inConeTransforms.Add(hit.transform);
        }

        return inConeTransforms.ToArray();
    }

#endregion

}