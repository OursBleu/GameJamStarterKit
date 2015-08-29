using UnityEngine;
using System.Collections;

public static class Extensions
{

    public static Vector2 AsVector2(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    public static Vector3 AsVector3(this Vector2 v)
    {
        return new Vector3(v.x, v.y, 0f);
    }

    public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
    {
        return gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
    }

    public static Color WithAlpha(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }

}