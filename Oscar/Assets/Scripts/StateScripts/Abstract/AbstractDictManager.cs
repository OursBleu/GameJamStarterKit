using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AbstractDictManager<T> : Manager where T : AbstractUpdatable, new()
{

	private Dictionary<string, T> _dict = new Dictionary<string, T>();
    public Dictionary<string, T> Dict { get { return _dict; } }

    public T this[string key] 
    {
        get
        {
            T res = _dict[key];
            if (res == null)
            {
                Debug.Log("Dictionnary[\"" + key + "\"] n'est pas défini.");
                return null;
            }
            else return _dict[key];
        }
    }

    public virtual T Add(string key)
    {
        if (_dict.ContainsKey(key))
        {
            Debug.Log("Dictionnary[\"" + key + "\"] a déjà été défini.");
            return null;
        }
        T value = new T();
        _dict.Add(key, value);
        return value;
	}

    public virtual T Add(string key, T value)
    {
        if (_dict.ContainsKey(key))
        {
            Debug.Log("Dictionnary[\"" + key + "\"] a déjà été défini.");
            return null;
        }
        _dict.Add(key, value);
        return value;
    }

    protected virtual void Update()
    {
        foreach (KeyValuePair<string, T> entry in _dict)
        {
            entry.Value.Update();
            OnUpdating(entry.Key, entry.Value);
        }
    }

    protected virtual void OnUpdating(string key, T value) { }

}
