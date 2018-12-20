using UnityEngine;
using System.Collections;

public class QuarticEaseOut : IEasing
{
	public float Calculate( float t, float b, float c, float d )
	{
		return -c * ((t = t / d - 1) * t * t * t - 1) + b;
	}
}