using UnityEngine;
using System;
using System.Collections;

public class SineEaseOut : IEasing
{
	public float Calculate( float t, float b, float c, float d )
	{
        return c * (float)Math.Sin(t / d * ((float)Math.PI / 2f)) + b;
	}
}
