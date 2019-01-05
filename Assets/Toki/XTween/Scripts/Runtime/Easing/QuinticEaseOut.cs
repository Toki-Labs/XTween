using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class QuinticEaseOut : IEasing
	{
		public float Calculate( float t, float b, float c, float d )
		{
			return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
		}
	}
}