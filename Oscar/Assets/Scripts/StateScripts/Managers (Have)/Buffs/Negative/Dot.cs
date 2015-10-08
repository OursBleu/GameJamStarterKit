using UnityEngine;
using System.Collections;

public class Dot : NegativeBuff
{
    private int _damages;
    public int Damages { get { return _damages; } set { _damages = value; } }

    private float _rate;
    public float Rate { get { return _rate; } set { _rate = value; } }

    private string _damagedResource;
    public string DamagedResource { get { return _damagedResource; } set { _damagedResource = value; } }

    public Dot(GameObject self, float duration) : base(self, duration)
    {
        _damages = 1;
        _rate = 1f;
        _damagedResource = "health";
    }

    public Dot(GameObject self, float duration, int damages, float rate, string damagedResource) : base(self, duration)
    {
        _damages = damages;
        _rate = rate;
        _damagedResource = damagedResource;
    }

    HaveResources _resources;
    float nextTick;

    public override void BuffEnter()
    {
        _resources = Self.GetComponent<HaveResources>();

        nextTick = 0f;
    }

    public override void BuffUpdate()
    {
        if (Time.timeSinceLevelLoad >= nextTick)
        {
            _resources[_damagedResource].Lose(_damages);
            nextTick = Time.timeSinceLevelLoad + _rate;
        }
    }
}