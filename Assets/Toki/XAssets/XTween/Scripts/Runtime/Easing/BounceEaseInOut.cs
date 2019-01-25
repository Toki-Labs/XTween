using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class BounceEaseInOut : IEasing
	{
		public float Calculate( float t, float b, float c, float d )
		{
			if (t < d / 2f) {
				if ((t = (d - t * 2f) / d) < (1f / 2.75f)) {
					return (c - (c * (7.5625f * t * t))) * 0.5f + b;
				}
				if (t < (2f / 2.75f)) {
					return (c - (c * (7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f))) * 0.5f + b;
				}
				if (t < (2.5f / 2.75f)) {
					return (c - (c * (7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f))) * 0.5f + b;
				}
				return (c - (c * (7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f))) * 0.5f + b;
			}
			else {
				if ((t = (t * 2f - d) / d) < (1f / 2.75f)) {
					return (c * (7.5625f * t * t)) * 0.5f + c * 0.5f + b;
				}
				if (t < (2f / 2.75f)) {
					return (c * (7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f)) * 0.5f + c * 0.5f + b;
				}
				if (t < (2.5f / 2.75f)) {
					return (c * (7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f)) * 0.5f + c * 0.5f + b;
				}
				return (c * (7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f)) * 0.5f + c * 0.5f + b;
			}
		}
	}
}