using UnityEngine;
using System;
using System.Collections;

namespace Toki.Tween
{
	public class ExponentialEaseIn : IEasing
	{
		public float Calculate( float t, float b, float c, float d )
		{
			return t == 0f ? b : c * (float)Math.Pow(2f, 10f * (t / d - 1f)) + b;
		}
	}
}