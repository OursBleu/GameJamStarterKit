using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillCreator : MonoBehaviour {

    public string nom;
    public List<string> liste;

	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("g"))
        {
           Debug.Log(nom + " : " + RandomElement(liste) + "\n");
        }
	}

    string RandomElement(List<string> liste)
    {
        return liste[Mathf.RoundToInt(Random.Range(0, liste.Count - 1))];
    }
}
