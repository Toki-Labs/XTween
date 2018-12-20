using UnityEngine;
using System.Collections;

public class QuadraticEaseInOut : IEasing
{
	public float Calculate( float t, float b, float c, float d )
	{
		if ((t /= d / 2f) < 1f) {
			return c / 2f * t * t + b;
		}
		return -c / 2f * ((--t) * (t - 2f) - 1f) + b;
	}
}