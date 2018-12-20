using UnityEngine;
using System.Collections;

public class QuinticEaseIn : IEasing
{
	public float Calculate( float t, float b, float c, float d )
	{
		return c * (t /= d) * t * t * t * t + b;
	}
}