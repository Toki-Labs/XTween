using UnityEngine;
using System.Collections;

public class Quart
{
	static Quart()
	{
		easeIn = new QuarticEaseIn();
		easeOut = new QuarticEaseOut();
		easeInOut = new QuarticEaseInOut();
		easeOutIn = new QuarticEaseOutIn();
	}
		
	public static IEasing easeIn;
	public static IEasing easeOut;
	public static IEasing easeInOut;
	public static IEasing easeOutIn;
}