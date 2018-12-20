using UnityEngine;
using System;
using System.Collections;

public class CircularEaseOutIn : IEasing
{
	public float Calculate( float t, float b, float c, float d )
	{
		if (t < d / 2f) {
			return (c / 2f) * (float)Math.Sqrt(1f - (t = (t * 2f) / d - 1f) * t) + b;
		}
		return -(c / 2f) * ((float)Math.Sqrt(1f - (t = (t * 2f - d) / d) * t) - 1f) + (b + c / 2f);
	}
}