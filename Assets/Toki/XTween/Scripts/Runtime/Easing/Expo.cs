using UnityEngine;
using System.Collections;

public class Expo
{
	static Expo()
	{
		easeIn = new ExponentialEaseIn();
		easeOut = new ExponentialEaseOut();
		easeInOut = new ExponentialEaseInOut();
		easeOutIn = new ExponentialEaseOutIn();
	}
		
	public static IEasing easeIn;
	public static IEasing easeOut;
	public static IEasing easeInOut;
	public static IEasing easeOutIn;
}