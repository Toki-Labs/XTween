using UnityEngine;
using System;
using System.Collections;

namespace Toki.Tween
{
	public class CircularEaseIn : IEasing
	{
		public float Calculate( float t, float b, float c, float d )
		{
			return -c * ((float)Math.Sqrt(1f - (t /= d) * t) - 1f) + b;
		}
	}
}