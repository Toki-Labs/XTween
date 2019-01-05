using UnityEngine;
using System;
using System.Collections;

namespace Toki.Tween
{
	public class SineEaseInOut : IEasing
	{
		public float Calculate( float t, float b, float c, float d )
		{
			return -c / 2f * ((float)Math.Cos((float)Math.PI * t / d) - 1f) + b;
		}
	}
}