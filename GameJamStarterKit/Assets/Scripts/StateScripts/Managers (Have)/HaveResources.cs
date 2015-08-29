using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HaveResources : AbstractManager
{

    Dictionary<string, Resource> _resources = new Dictionary<string, Resource>();

    public Dictionary<string, Resource> Resources { get { return _resources; } }

    public Resource this[string key] 
    {
        get {
            Resource res = _resources[key];
            if (res == null)
            {
                Debug.Log("resource[\"" + key + "\"] n'est pas défini.");
                return new Resource(0,1);
            }
            else return _resources[key]; 
        } 
    }

    public Resource Add (string name, int minValue, int maxValue)
    {
        if (_resources.ContainsKey(name))
        {
            Debug.Log("resource[\"" + name + "\"] a déjà été défini.");
            return null;
        }
        Resource resource = new Resource(minValue, maxValue);
        _resources.Add(name, resource);
        return resource;
    }

}
