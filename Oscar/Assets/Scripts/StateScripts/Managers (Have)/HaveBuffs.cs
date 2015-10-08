using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class HaveBuffs : AbstractDictManager<Buff>
{
    List<string> _keysToRemove = new List<string>();

    public override Buff Add(string key, Buff value) 
    {
        Buff res = base.Add(key, value);
        if (res == null) return null;

        res.Restart();
        res.BuffEnter();

        return res;
	}

    protected override void Update()
    {
        base.Update();

        foreach (string key in _keysToRemove)
        {
            Dict.Remove(key);
        }
        _keysToRemove.Clear();
    }

    protected override void OnUpdating(string key, Buff value)
    {
        if (!value.IsRunning)
        {
            Clear(key);
        }
    }

    public void Clear(string key)
    {
        if (!_keysToRemove.Contains(key))
        {
            _keysToRemove.Add(key);
            Dict[key].BuffExit();
        }
    }

    public void Clear()
    {
        foreach (string key in Dict.Keys)
        {
            Clear(key);
        }
    }

    public void Clear(bool arePositiveBuffsToBeRemoved)
    {
        foreach (string key in Dict.Keys)
        {
            if (Dict[key].IsPositive == arePositiveBuffsToBeRemoved) Clear(key);
        }
    }

    public override string ToString()
    {
        string res = "Buffs : ";
        foreach (string key in Dict.Keys)
        {
            res += "[" + key + "] ";
        }
        return res + "\n";
    }
}
