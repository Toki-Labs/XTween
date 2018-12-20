using UnityEngine;
using System.Collections;

public class Elastic
{
	static Elastic()
	{
		easeIn = new ElasticEaseIn();
		easeOut = new ElasticEaseOut();
		easeInOut = new ElasticEaseInOut();
		easeOutIn = new ElasticEaseOutIn();
	}
		
	public static IEasing easeIn;
	public static IEasing easeOut;
	public static IEasing easeInOut;
	public static IEasing easeOutIn;
		
	/**
		* 
		* @param	a	Specifies the amplitude of the sine wave.
		* @param	p	Specifies the period of the sine wave.
		* @return	An easing with passed parameters.
		*/
	public static IEasing easeInWith( float a = 0, float p = 0)
	{
		return new ElasticEaseIn(a, p);
	}
		
	/**
		* 
		* @param	a	Specifies the amplitude of the sine wave.
		* @param	p	Specifies the period of the sine wave.
		* @return	An easing with passed parameters.
		*/
	public static IEasing easeOutWith( float a = 0, float p = 0)
	{
		return new ElasticEaseOut(a, p);
	}
		
	/**
		* 
		* @param	a	Specifies the amplitude of the sine wave.
		* @param	p	Specifies the period of the sine wave.
		* @return	An easing with passed parameters.
		*/
	public static IEasing easeInOutWith( float a = 0, float p = 0)
	{
		return new ElasticEaseInOut(a, p);
	}
		
	/**
		* 
		* @param	a	Specifies the amplitude of the sine wave.
		* @param	p	Specifies the period of the sine wave.
		* @return	An easing with passed parameters.
		*/
	public static IEasing easeOutInWith( float a = 0, float p = 0)
	{
		return new ElasticEaseOutIn(a, p);
	}
}