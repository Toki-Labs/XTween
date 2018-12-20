using UnityEngine;
using System.Collections;

public class Quint
{
	static Quint()
	{
		easeIn = new QuinticEaseIn();
		easeOut = new QuinticEaseOut();
		easeInOut = new QuinticEaseInOut();
		easeOutIn = new QuinticEaseOutIn();
	}
		
	public static IEasing easeIn;
	public static IEasing easeOut;
	public static IEasing easeInOut;
	public static IEasing easeOutIn;
}