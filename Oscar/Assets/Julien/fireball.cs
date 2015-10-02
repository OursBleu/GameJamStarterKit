using UnityEngine;
using System.Collections;

public class fireball : MonoBehaviour {
	public float TimeAlive;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Destroy(gameObject,TimeAlive);
	}

}
