using UnityEngine;
using System.Collections;

public class AbstractProjectile : AbstractInvocation {

    public float _speed = 90f;
    public string _folderName = "SpriteSheets";
    public string _spriteName = "FireBall";
    protected HaveAnimations anim;
    protected HaveMovements movement;

	protected override void Awake () 
    {
        base.Awake();

        anim = gameObject.GetOrAdd<HaveAnimations>();
        anim.FolderName = _folderName;
        anim.SpriteName = _spriteName;
        anim.Play("");

        movement = gameObject.GetOrAdd<HaveMovements>();
        movement.Speed = _speed;

        LifeTime = 5f;
	}

    protected override void Update() 
    {
        base.Update();

        movement.Move(transform.right);
        if (collision.IsColliding)
        {
            if (!collision.ForceCollision(collision.Other.transform, "Hazard", transform.right, true)) return;
            //anim.PlayInVoid("Explosion");
            Destroy(gameObject);
        }
	}
}
