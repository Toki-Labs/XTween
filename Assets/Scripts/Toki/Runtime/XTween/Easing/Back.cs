using UnityEngine;
using System.Collections;

public class Back
{	
	static Back()
	{
		easeIn = new BackEaseIn();
		easeOut = new BackEaseOut();
		easeInOut = new BackEaseInOut();
		easeOutIn = new BackEaseOutIn();
	}
		
	public static IEasing easeIn;
	public static IEasing easeOut;
	public static IEasing easeInOut;
	public static IEasing easeOutIn;
		
	/**
		* 
		* @param	s	Specifies the amount of overshoot, where the higher the value, the greater the overshoot.
		* @return	An easing with passed parameter.
		*/
	public static IEasing easeInWith( float s = 1.70158f )
	{
		return new BackEaseIn(s);
	}
		
	/**
		* 
		* @param	s	Specifies the amount of overshoot, where the higher the value, the greater the overshoot.
		* @return	An easing with passed parameter.
		*/
	public static IEasing easeOutWith( float s = 1.70158f )
	{
		return new BackEaseOut(s);
	}
		
	/**
		* 
		* @param	s	Specifies the amount of overshoot, where the higher the value, the greater the overshoot.
		* @return	An easing with passed parameter.
		*/
	public static IEasing easeInOutWith( float s = 1.70158f )
	{
		return new BackEaseInOut(s);
	}
		
	/**
		* 
		* @param	s	Specifies the amount of overshoot, where the higher the value, the greater the overshoot.
		* @return	An easing with passed parameter.
		*/
	public static IEasing easeOutInWith( float s = 1.70158f )
	{
		return new BackEaseOutIn(s);
	}
}