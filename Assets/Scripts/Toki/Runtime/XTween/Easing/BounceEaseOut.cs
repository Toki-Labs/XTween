using UnityEngine;
using System.Collections;

public class BounceEaseOut : IEasing
{
    public float Calculate(float t, float b, float c, float d)
    {
        if ((t /= d) < (1f / 2.75f))
        {
            return c * (7.5625f * t * t) + b;
        }
        if (t < (2f / 2.75f))
        {
            return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f) + b;
        }
        if (t < (2.5f / 2.75f))
        {
            return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f) + b;
        }
        return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f) + b;
    }
}