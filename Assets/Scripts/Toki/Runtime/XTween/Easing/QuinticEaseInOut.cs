using UnityEngine;
using System.Collections;

public class QuinticEaseInOut : IEasing
{
	public float Calculate( float t, float b, float c, float d )
	{
		if ((t /= d / 2) < 1) {
			return c / 2 * t * t * t * t * t + b;
		}
		return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
	}
}