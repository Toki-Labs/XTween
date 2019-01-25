using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class BackEaseOutIn : IEasing
	{
		public float s;
			
		public BackEaseOutIn( float s = 1.70158f )
		{
			this.s = s;
		}	
			
		public float Calculate( float t, float b, float c, float d )
		{
			if (t < d / 2) {
				return (c / 2) * ((t = (t * 2) / d - 1) * t * ((s + 1) * t + s) + 1) + b;
			}
			return (c / 2) * (t = (t * 2 - d) / d) * t * ((s + 1) * t - s) + (b + c / 2);
		}
	}
}