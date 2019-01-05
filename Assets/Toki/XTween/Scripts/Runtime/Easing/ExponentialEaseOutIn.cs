using UnityEngine;
using System;
using System.Collections;

namespace Toki.Tween
{
	public class ExponentialEaseOutIn : IEasing
	{
		public float Calculate( float t, float b, float c, float d )
		{
			if (t < d / 2.0f) {
				return t * 2.0f == d ? b + c / 2.0f : c / 2.0f * (1 - (float)Math.Pow(2f, -10f * t * 2.0f / d)) + b;
			}
			return (t * 2.0f - d) == 0f ? b + c / 2.0f : c / 2.0f * (float)Math.Pow(2f, 10f * ((t * 2f - d) / d - 1f)) + b + c / 2.0f;
		}
	}
}