using UnityEngine;
using System.Collections;

public class QuinticEaseOutIn : IEasing
{
	public float Calculate( float t, float b, float c, float d )
	{
		if (t < d / 2) {
			return (c / 2) * ((t = (t * 2) / d - 1) * t * t * t * t + 1) + b;
		}
		return (c / 2) * (t = (t * 2 - d) / d) * t * t * t * t + (b + c / 2);
	}
}