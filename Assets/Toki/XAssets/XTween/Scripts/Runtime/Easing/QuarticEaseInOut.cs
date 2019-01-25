using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class QuarticEaseInOut : IEasing
	{
		public float Calculate( float t, float b, float c, float d )
		{
			if ((t /= d / 2f) < 1f) {
				return c / 2f * t * t * t * t + b;
			}
			return -c / 2f * ((t -= 2f) * t * t * t - 2f) + b;
		}
	}
}