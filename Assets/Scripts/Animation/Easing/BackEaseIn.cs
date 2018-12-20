using UnityEngine;
using System.Collections;

public class BackEaseIn : IEasing
{
	public float s;
		
	public BackEaseIn( float s = 1.70158f )
	{
		this.s = s;
	}
		
	public float Calculate( float t, float b, float c, float d )
	{
		return c * (t /= d) * t * ((s + 1) * t - s) + b;
	}
}