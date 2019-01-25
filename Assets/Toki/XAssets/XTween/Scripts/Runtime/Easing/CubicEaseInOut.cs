using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class CubicEaseInOut : IEasing
	{
		public float Calculate( float t, float b, float c, float d )
		{
			return ((t /= d / 2f) < 1f) ? c / 2f * t * t * t + b : c / 2f * ((t -= 2f) * t * t + 2f) + b;
		}
	}
}