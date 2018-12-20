using UnityEngine;
using System.Collections;

public class QuarticEaseOutIn : IEasing
{
	public float Calculate( float t, float b, float c, float d )
	{
		if (t < d / 2f) {
			return -(c / 2f) * ((t = (t * 2f) / d - 1f) * t * t * t - 1f) + b;
		}
		return (c / 2f) * (t = (t * 2f - d) / d) * t * t * t + (b + c / 2f);
	}
}