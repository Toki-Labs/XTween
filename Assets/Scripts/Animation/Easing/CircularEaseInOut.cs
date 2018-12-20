using UnityEngine;
using System;
using System.Collections;

public class CircularEaseInOut : IEasing
{
	public float Calculate( float t, float b, float c, float d )
	{
		if ((t /= d / 2f) < 1f) {
			return -c / 2f * ((float)Math.Sqrt(1f - t * t) - 1f) + b;
		}
		return c / 2f * ((float)Math.Sqrt(1f - (t -= 2f) * t) + 1f) + b;
	}
}