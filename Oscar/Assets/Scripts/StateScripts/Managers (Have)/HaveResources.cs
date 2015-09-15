using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HaveResources : AbstractDictManager<Resource>
{
    public Resource Add (string name, int minValue, int maxValue)
    {
        Resource res = Add(name);
        if (res == null) return null;

        res.MinValue = minValue;
        res.MaxValue = maxValue;
        return res;
    }

    public override string ToString()
    {
        string res = "Resources : ";
        foreach (string key in Dict.Keys)
        {
            res += "[" + key + " : " + Dict[key].Value + "/" + Dict[key].MaxValue + "] ";
        }
        return res + "\n";
    }
}
