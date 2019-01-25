using UnityEngine;
using System;
using System.Collections;

namespace Toki.Tween
{
	public class ExponentialEaseOut : IEasing
	{
		public float Calculate( float t, float b, float c, float d )
		{
			return t == d ? b + c : c * (1f - (float)Math.Pow(2f, -10f * t / d)) + b;
		}
	}
}