using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HaveHUD : MonoBehaviour {

    Text str;

    void Start()
     {
        str = GameObject.Find("Text").GetComponent<Text>();
     }
	
	void Update () 
    {
        str.text = "";

        HaveResources resource = GetComponent<HaveResources>();
        if (resource) 
        {
            foreach (KeyValuePair<string, Resource> pair in resource.Resources)
            {
                str.text += pair.Key + " : " + pair.Value.Value + "\n";
            }
        }

        HaveCooldowns cooldown = GetComponent<HaveCooldowns>();
        if (cooldown)
        {
            foreach (KeyValuePair<string, Cooldown> pair in cooldown.Cooldowns)
            {
                str.text += pair.Key + " : " + pair.Value.Remaining.ToString("F1") + "\n";
            }
        }

        HaveCollisions collision = GetComponent<HaveCollisions>();
        if (collision)
        {
            str.text += "TeamIndex : " + collision.TeamIndex + "\n";
            str.text += "Other : " + (collision.Other != null ? collision.Other.gameObject.name : "/") + "\n";
            str.text += "ForcedCollision : " + collision.IsOtherOwnCollider + "\n";
        }
	}

    void OnGui()
    {
        str.rectTransform.position = Camera.main.WorldToViewportPoint(transform.position);
    }
}
