using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlobalManagerParticles : MonoBehaviour
{

    private string _particlePath = "Particles/";

    public ParticleSystem Play(string path, Vector3 position, bool looping = false)
    {
        GameObject prefab = Resources.Load(_particlePath + path) as GameObject;
        GameObject instance = Instantiate(prefab, position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
        ParticleSystem ps = instance.GetComponent<ParticleSystem>();
        if (!looping) Destroy(ps.gameObject, ps.duration);
        return ps;
    }

    public ParticleSystem Play(string path, Transform target, Vector3 direction, bool looping = false)
    {
        GameObject prefab = Resources.Load(_particlePath + path) as GameObject;
        GameObject instance = Instantiate(prefab) as GameObject;
        ParticleSystem ps = instance.GetComponent<ParticleSystem>();
        instance.transform.position = target.position;
        instance.transform.parent = target;
        instance.transform.forward = -direction;
        if (!looping) Destroy(ps.gameObject, ps.duration);
        return ps;
    }

}
