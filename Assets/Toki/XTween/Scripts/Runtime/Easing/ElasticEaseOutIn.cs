using UnityEngine;
using System;
using System.Collections;

namespace Toki.Tween
{
	public class ElasticEaseOutIn : IEasing
	{
		public float a;
		public float p;
			
		public ElasticEaseOutIn( float a = 0f, float p = 0f )
		{
			this.a = a;
			this.p = p;
		}
			
		public float Calculate( float t, float b, float c, float d )
		{
			float s;
				
			c /= 2f;
				
			if (t < d / 2f) {
				if ((t *= 2f) == 0f) {
					return b;
				}
				if ((t /= d) == 1f) {
					return b + c;
				}
				if (p == 0f) {
					p = d * 0.3f;
				}
				if (a == 0f || a < (float)Math.Abs(c)) {
					a = c;
					s = p / 4f;
				}
				else {
					s = p / (2f * (float)Math.PI) * (float)Math.Asin(c / a);
				}
				return a * (float)Math.Pow(2f, -10f * t) * (float)Math.Sin((t * d - s) * (2f * (float)Math.PI) / p) + c + b;
			}
			else {
				if ((t = t * 2f - d) == 0f) {
					return (b + c);
				}
				if ((t /= d) == 1f) {
					return (b + c) + c;
				}
				if (p == 0f) {
					p = d * 0.3f;
				}
				if (a == 0f || a < (float)Math.Abs(c)) {
					a = c;
					s = p / 4f;
				}
				else {
					s = p / (2f * (float)Math.PI) * (float)Math.Asin(c / a);
				}
				return -(a * (float)Math.Pow(2f, 10f * (t -= 1f)) * (float)Math.Sin((t * d - s) * (2f * (float)Math.PI) / p)) + (b + c);
			}
		}
	}
}