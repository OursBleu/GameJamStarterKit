using UnityEngine;
using System.Collections;

public class ManagerCamera : Manager 
{
    public float _xOffset = 5f;
    public float _speed = 400f;

    Transform _target;
    Rigidbody2D _rigidbody;

    void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

	void Start () 
    {
        _target = GameObject.FindWithTag("Player").transform;
	}
	
	void LateUpdate () 
    {
        Vector3 newPos = _target.transform.position + (Mathf.Sign(_target.transform.localScale.x) * _xOffset) * Vector3.right;
        Vector2 dir = (newPos - transform.position).normalized;
        _rigidbody.AddForce(dir * _speed);
	}
}
