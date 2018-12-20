using UnityEngine;
using System.Collections;

public class Circ
{
	static Circ()
	{
		easeIn = new CircularEaseIn();
		easeOut = new CircularEaseOut();
		easeInOut = new CircularEaseInOut();
		easeOutIn = new CircularEaseOutIn();
	}
		
	public static IEasing easeIn;
	public static IEasing easeOut;
	public static IEasing easeInOut;
	public static IEasing easeOutIn;
}