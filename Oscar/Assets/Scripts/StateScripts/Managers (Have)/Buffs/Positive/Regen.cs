using UnityEngine;
using System.Collections;

public class Regen : NegativeBuff
{
    private int _heal;
    public int Heal { get { return _heal; } set { _heal = value; } }

    private float _rate;
    public float Rate { get { return _rate; } set { _rate = value; } }

    private string _restoredResource;
    public string RestoredResource { get { return _restoredResource; } set { _restoredResource = value; } }

    public Regen(GameObject self, float duration) : base(self, duration)
    {
        _heal = 1;
        _rate = 1f;
        _restoredResource = "health";
    }

    public Regen(GameObject self, float duration, int damages, float rate, string damagedResource) : base(self, duration)
    {
        _heal = damages;
        _rate = rate;
        _restoredResource = damagedResource;
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
            _resources[_restoredResource].Gain(_heal);
            nextTick = Time.timeSinceLevelLoad + _rate;
        }
    }
}