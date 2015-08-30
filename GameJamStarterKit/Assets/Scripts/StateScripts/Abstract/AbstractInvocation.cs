using UnityEngine;
using System.Collections;

public abstract class AbstractInvocation : MonoBehaviour {


    protected Transform _launcher;
    public Transform Launcher { get { return _launcher; } set { _launcher = value; } }
    public HaveCollisions LauncherCollisionManager { 
        get 
        { 
            return _launcher.GetComponent<HaveCollisions>(); 
        }
    }

    protected float _elapsedTime = 0f;
    public float ElapsedTime { get { return _elapsedTime; } }

    protected float _lifeTime = Mathf.Infinity;
    public float LifeTime { get { return _lifeTime; } set { _lifeTime = value; } }


	protected virtual void Awake () 
    {
	}

    protected virtual void Update()
    {
        if (_elapsedTime >= _lifeTime) Destroy(gameObject);
    }

    protected virtual void LateUpdate()
    {
        _elapsedTime += Time.deltaTime;
    }
}
