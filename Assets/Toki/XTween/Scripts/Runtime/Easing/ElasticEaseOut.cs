using UnityEngine;
using System;
using System.Collections;

namespace Toki.Tween
{
	public class ElasticEaseOut : IEasing
	{
		public float a;
		public float p;
			
		public ElasticEaseOut( float a = 0f, float p = 0f )
		{
			this.a = a;
			this.p = p;
		}
			
		public float Calculate( float t, float b, float c, float d )
		{
			if (t == 0) {
				return b;
			}
			if ((t /= d) == 1f) {
				return b + c;
			}
			if (p == 0f) {
				p = d * 0.3f;
			}
			float s;
			if (a == 0f || a < (float)Math.Abs(c)) {
				a = c;
				s = p / 4f;
			}
			else {
				s = p / (2f * (float)Math.PI) * (float)Math.Asin(c / a);
			}
			return a * (float)Math.Pow(2f, -10f * t) * (float)Math.Sin((t * d - s) * (2f * (float)Math.PI) / p) + c + b;
		}
	}
}