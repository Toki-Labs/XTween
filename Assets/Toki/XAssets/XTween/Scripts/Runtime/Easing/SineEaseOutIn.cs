using UnityEngine;
using System;
using System.Collections;

namespace Toki.Tween
{
	public class SineEaseOutIn : IEasing
	{
		public float Calculate( float t, float b, float c, float d )
		{
			if (t < d / 2f) {
				return (c / 2f) * (float)Math.Sin((t * 2f) / d * ((float)Math.PI / 2f)) + b;
			}
			return -(c / 2f) * (float)Math.Cos((t * 2f - d) / d * ((float)Math.PI / 2f)) + (c / 2f) + (b + c / 2f);
		}
	}
}