using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class HaveParticles : AbstractManager {

    public AreaEnum _zone;
    public Vector3 _direction;
    public float _length;

    public enum AreaEnum
    { 
        cone,
        line,
        circle
    }

    public Dictionary<AreaEnum, ParticleSystem> AreaDict;

    void Awake () {
        AreaDict = new Dictionary<AreaEnum, ParticleSystem>();

        int i = 0;
        foreach (var name in Enum.GetNames(typeof(AreaEnum))) 
        {
            GameObject ps = Resources.Load("Particles/"+name) as GameObject;
            AreaDict.Add((AreaEnum)i++, ps.GetComponent<ParticleSystem>());
        }
    }

    void Start() 
    {
        //Play(_zone, Vector3.zero, _direction);
    }

    void Update()
    {
        //Debug.DrawLine(Vector3.zero, _direction * _length, Color.red);
    }

    public ParticleSystem Play(AreaEnum area, Vector3 direction) {
        ParticleSystem ps = Instantiate(AreaDict[area], transform.position, MapRotationToDirection(AreaDict[area].transform.rotation, direction)) as ParticleSystem;
        Destroy(ps.gameObject, ps.duration);
        return ps;
    }

    Quaternion MapRotationToDirection(Quaternion initialRotation, Vector3 wantedDirection)
    {
        Vector3 prefabAngle = initialRotation.eulerAngles;
        float sign = (wantedDirection.x > 0 ? 1 : -1);
        float angleDiff = Vector3.Angle(Vector3.up, wantedDirection);
        Quaternion correctRotation = Quaternion.Euler(prefabAngle.x, prefabAngle.y, prefabAngle.z - (sign * angleDiff));
        return correctRotation;
    }

}
