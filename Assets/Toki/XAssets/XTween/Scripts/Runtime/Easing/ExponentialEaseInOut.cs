using UnityEngine;
using System;
using System.Collections;

namespace Toki.Tween
{
	public class ExponentialEaseInOut : IEasing
	{
		public float Calculate( float t, float b, float c, float d )
		{
			if (t == 0) {
				return b;
			}
			if (t == d) {
				return b + c;
			}
			if ((t /= d / 2.0f) < 1.0f) {
				return c / 2f * (float)Math.Pow(2f, 10f * (t - 1f)) + b;
			}
			return c / 2f * (2f - (float)Math.Pow(2f, -10f * --t)) + b;
		}
	}
}