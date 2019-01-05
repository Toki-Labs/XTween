using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class CubicEaseOut : IEasing
	{
		public float Calculate( float t, float b, float c, float d )
		{
			return c * ((t = t / d - 1f) * t * t + 1f) + b;
		}
	}
}