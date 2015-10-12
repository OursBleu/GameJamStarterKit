using UnityEngine;
using System.Collections;

public abstract class Instantiable : MonoBehaviour
{
    private GameObject _launcher;
    public GameObject Launcher
    {
        get { return _launcher; }
        set { _launcher = value; }
    }
    
   
}
