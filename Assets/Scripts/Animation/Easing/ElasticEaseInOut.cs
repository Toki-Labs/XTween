using UnityEngine;
using System;
using System.Collections;

public class ElasticEaseInOut : IEasing
{
	public float a;
	public float p;
		
	public ElasticEaseInOut( float a = 0f, float p = 0f )
	{
		this.a = a;
		this.p = p;
	}
		
	public float Calculate( float t, float b, float c, float d )
	{
		if (t == 0f) {
			return b;
		}
		if ((t /= d / 2f) == 2f) {
			return b + c;
		}
		if (p == 0f) {
			p = d * (0.3f * 1.5f);
		}
		float s;
		if (a == 0f || a < (float)Math.Abs(c)) {
			a = c;
			s = p / 4f;
		}
		else {
			s = p / (2f * (float)Math.PI) * (float)Math.Asin(c / a);
		}
		if (t < 1f) {
			return -0.5f * (a * (float)Math.Pow(2f, 10f * (t -= 1)) * (float)Math.Sin((t * d - s) * (2f * (float)Math.PI) / p)) + b;
		}
		return a * (float)Math.Pow(2f, -10f * (t -= 1f)) * (float)Math.Sin((t * d - s) * (2f * (float)Math.PI) / p) * 0.5f + c + b;
	}
}