using UnityEngine;
using System.Collections;

public class Linear
{
	private static IEasing _linear = new EaseNone();
		
	public static IEasing linear = _linear;
	public static IEasing easeNone = _linear;
	public static IEasing easeIn = _linear;
	public static IEasing easeOut = _linear;
	public static IEasing easeInOut = _linear;
	public static IEasing easeOutIn = _linear;
}