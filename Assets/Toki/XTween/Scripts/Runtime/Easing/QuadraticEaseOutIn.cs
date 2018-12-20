using UnityEngine;
using System.Collections;

public class QuadraticEaseOutIn : IEasing
{
	public float Calculate( float t, float b, float c, float d )
	{
		if (t < d / 2f) {
			return -(c / 2f) * (t = (t * 2f / d)) * (t - 2f) + b;
		}
		return (c / 2f) * (t = (t * 2f - d) / d) * t + (b + c / 2f);
	}
}