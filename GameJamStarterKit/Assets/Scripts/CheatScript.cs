using UnityEngine;
using System.Collections;

public class CheatScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("u")) GetComponent<Collider2D>().enabled = !GetComponent<Collider2D>().enabled;
        else if (Input.GetKeyDown("i")) GetComponent<HaveResources>()["Health"].Gain(100);
        // else if (Input.GetKeyDown("o"))
        else if (Input.GetKeyDown("p")) Application.LoadLevel(Application.loadedLevel);
	
	}
}
