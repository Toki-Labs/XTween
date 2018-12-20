using UnityEngine;
using System.Collections;

public class Quad
{
	static Quad()
	{
		easeIn = new QuadraticEaseIn();
		easeOut = new QuadraticEaseOut();
		easeInOut = new QuadraticEaseInOut();
		easeOutIn = new QuadraticEaseOutIn();
	}
		
	public static IEasing easeIn;
	public static IEasing easeOut;
	public static IEasing easeInOut;
	public static IEasing easeOutIn;
}