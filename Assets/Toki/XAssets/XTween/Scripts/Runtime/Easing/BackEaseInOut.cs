using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class BackEaseInOut : IEasing
	{
		public float s;
			
		public BackEaseInOut( float s = 1.70158f )
		{
			this.s = s;
		}
			
		public float Calculate( float t, float b, float c, float d )
		{
			if ((t /= d / 2f) < 1f) {
				return c / 2f * (t * t * (((s * 1.525f) + 1f) * t - s * 1.525f)) + b;
			}
			return c / 2f * ((t -= 2f) * t * (((s * 1.525f) + 1f) * t + s * 1.525f) + 2f) + b;
		}
	}
}