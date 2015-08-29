using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HaveCooldowns : AbstractManager
{

	public Dictionary<string, Cooldown> _cooldowns = new Dictionary<string, Cooldown>();

    public Dictionary<string, Cooldown> Cooldowns { get { return _cooldowns; } }

    public Cooldown this[string key] 
    {
        get
        {
            Cooldown res = _cooldowns[key];
            if (res == null)
            {
                Debug.Log("cooldown[\"" + key + "\"] n'est pas défini.");
                return null;
            }
            else return _cooldowns[key];
        }
    }

	public Cooldown Add (string name, float length) 
    {
        if (_cooldowns.ContainsKey(name))
        {
            Debug.Log("cooldown[\"" + name + "\"] a déjà été défini.");
            return null;
        }
        Cooldown cooldown = new Cooldown(length);
        _cooldowns.Add(name, cooldown);
        return cooldown;
	}

    void Update()
    {
        foreach (KeyValuePair<string, Cooldown> entry in _cooldowns)
        {
            entry.Value.Update();
        }
    }

}
