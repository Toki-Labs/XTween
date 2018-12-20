using UnityEngine;
using System.Collections;

public class Cubic
{
	static Cubic()
	{
		easeIn = new CubicEaseIn();
		easeOut = new CubicEaseOut();
		easeInOut = new CubicEaseInOut();
		easeOutIn = new CubicEaseOutIn();
	}
		
	public static IEasing easeIn;
	public static IEasing easeOut;
	public static IEasing easeInOut;
	public static IEasing easeOutIn;
}