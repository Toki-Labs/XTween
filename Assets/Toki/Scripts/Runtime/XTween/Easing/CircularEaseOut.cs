using UnityEngine;
using System;
using System.Collections;

public class CircularEaseOut : IEasing
{
	public float Calculate( float t, float b, float c, float d )
	{
		return c * (float)Math.Sqrt(1f - (t = t / d - 1f) * t) + b;
	}
}