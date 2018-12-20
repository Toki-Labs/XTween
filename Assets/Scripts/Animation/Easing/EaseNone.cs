using UnityEngine;
using System.Collections;

public class EaseNone : IEasing
{
	public float Calculate( float t, float b, float c, float d )
	{
		return c * t / d + b;
	}
}
