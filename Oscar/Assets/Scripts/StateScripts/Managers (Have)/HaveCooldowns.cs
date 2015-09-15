using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HaveCooldowns : AbstractDictManager<Timer>
{
	public Timer Add (string name, float length) 
    {
        Timer res = Add(name);
        if (res == null) return null;

        res.Duration = length;
        return res;
	}

    public override string ToString()
    {
        string res = "Cooldowns : ";
        foreach (string key in Dict.Keys)
        {
            res += "[" + key + " : " +  Dict[key].Remaining.ToString("F1") + "] ";
        }
        return res + "\n";
    }
}
