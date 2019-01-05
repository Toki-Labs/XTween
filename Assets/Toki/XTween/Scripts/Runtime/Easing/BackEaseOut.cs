using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class BackEaseOut : IEasing
	{
		public float s;
			
		public BackEaseOut( float s = 1.70158f )
		{
			this.s = s;
		}
			
		public float Calculate( float t, float b, float c, float d )
		{
			return c * ((t = t / d - 1) * t * ((s + 1) * t + s) + 1) + b;
		}
	}
}