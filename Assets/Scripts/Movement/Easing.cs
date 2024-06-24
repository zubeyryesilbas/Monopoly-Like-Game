using UnityEngine;

public static class Easing
{
    public static float Linear(float start, float end, float t)
    {
        return Mathf.Lerp(start, end, t);
    }

    public static float EaseInQuad(float start, float end, float t)
    {
        t = Mathf.Clamp01(t);
        return start + (end - start) * t * t;
    }

    public static float EaseOutQuad(float start, float end, float t)
    {
        t = Mathf.Clamp01(t);
        return start + (end - start) * t * (2 - t);
    }

    public static float EaseInOutQuad(float start, float end, float t)
    {
        t = Mathf.Clamp01(t);
        if (t < 0.5f) return start + (end - start) * 2 * t * t;
        return start + (end - start) * (1 - Mathf.Pow(-2 * t + 2, 2) / 2);
    }
}