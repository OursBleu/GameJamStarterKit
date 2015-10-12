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

    public static Vector3 x(this Vector3 v)
    {
        return new Vector3(v.x, 0f, 0f);
    }

    public static Vector3 xy(this Vector3 v)
    {
        return new Vector3(v.x, v.y, 0f);
    }

    public static Vector3 xz(this Vector3 v)
    {
        return new Vector3(v.x, 0f, v.z);
    }

    public static Vector3 yz(this Vector3 v)
    {
        return new Vector3(0f, v.y, v.z);
    }

    public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
    {
        return gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
    }

    public static Color WithAlpha(this Color color, float alpha)
    {
        return new Color(color.r, color.g, color.b, alpha);
    }

    public static void DestroyIfOutOfScreen(this GameObject gameObject, float multiplicator = 0f)
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        Vector3 screenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        if (screenPos.x < -screenWidth * multiplicator || screenPos.x > screenWidth * (1f + multiplicator) ||
            screenPos.y < -screenHeight * multiplicator || screenPos.y > screenHeight * (1f + multiplicator)) MonoBehaviour.Destroy(gameObject);
    }

    public static bool IsOutOfScreen(this Transform transform, float multiplicator = 0f)
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        if (screenPos.x < -screenWidth * multiplicator || screenPos.x > screenWidth * (1f + multiplicator) ||
            screenPos.y < -screenHeight * multiplicator || screenPos.y > screenHeight * (1f + multiplicator)) return true;
        else return false;
    }

    public static Vector3 SetScaleX(this Transform transform, float x)
    {
        Vector3 scale = transform.localScale;
        scale.x = x;
        transform.localScale = scale;
        return scale;
    }

}