using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class Bounce
	{
		static Bounce()
		{
			easeIn =  new BounceEaseIn();
			easeOut = new BounceEaseOut();
			easeInOut = new BounceEaseInOut();
			easeOutIn = new BounceEaseOutIn();
		}
			
		public static IEasing easeIn;
		public static IEasing easeOut;
		public static IEasing easeInOut;
		public static IEasing easeOutIn;
	}
}