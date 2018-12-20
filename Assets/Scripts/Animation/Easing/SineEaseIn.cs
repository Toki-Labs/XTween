using UnityEngine;
using System;
using System.Collections;

public class SineEaseIn : IEasing
{
	public float Calculate( float t, float b, float c, float d )
	{
		return -c * (float)Math.Cos(t / d * ((float)Math.PI / 2f)) + c + b;
	}
}