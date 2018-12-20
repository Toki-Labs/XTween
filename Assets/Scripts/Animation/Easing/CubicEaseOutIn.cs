using UnityEngine;
using System.Collections;

public class CubicEaseOutIn : IEasing
{
	public float Calculate( float t, float b, float c, float d )
	{
		return t < d / 2f ? c / 2f * ((t = t * 2f / d - 1f) * t * t + 1f) + b : c / 2f * (t = (t * 2f - d) / d) * t * t + b + c / 2f;
	}
}