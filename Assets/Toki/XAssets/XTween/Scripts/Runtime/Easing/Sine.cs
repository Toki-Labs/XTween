using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class Sine
	{
		static Sine()
		{
			easeIn = new SineEaseIn();
			easeOut = new SineEaseOut();
			easeInOut = new SineEaseInOut();
			easeOutIn = new SineEaseOutIn();
		}
			
		public static IEasing easeIn;
		public static IEasing easeOut;
		public static IEasing easeInOut;
		public static IEasing easeOutIn;
	}
}