using UnityEngine;
using System.Collections;

public class StateProjectile : State
{
    // L'angle du tir
    protected float _shotAngle;

    // La distance entre le centre du tireur et le spawn du projectile
    protected Vector2 _shotOffset;

    // Le prefab a partir duquel on va instancier le projectile
    private GameObject _projectilePrefab;

    public StateProjectile(Fsm fsm, string prefabPath, float angle, float duration) : base(fsm)
    {
        StateDuration = duration;

        _shotAngle = angle;
        _shotOffset = (Quaternion.Euler(0f, 0f, _shotAngle) * (3f * Vector2.right));
        _projectilePrefab = Resources.Load<GameObject>(prefabPath);
    }

    public override void StateEnter()
    {
        Vector2 direction = new Vector2(Fsm.transform.localScale.normalized.x, 0f);
        Vector3 shotOffsetAccountingDirection = (_shotOffset.AsVector3() * direction.x);
        shotOffsetAccountingDirection.y = -Mathf.Abs(shotOffsetAccountingDirection.y);

        InstantiableFireball projectile = Fsm.Instantiate(_projectilePrefab).GetComponent<InstantiableFireball>();
        projectile.Launcher = Fsm.gameObject;
        projectile.transform.position = Fsm.transform.position + shotOffsetAccountingDirection;
        projectile.transform.rotation = Quaternion.identity;
        projectile.transform.right = Quaternion.Euler(0f, 0f, -direction.x * _shotAngle) * direction;
    }
   
}
