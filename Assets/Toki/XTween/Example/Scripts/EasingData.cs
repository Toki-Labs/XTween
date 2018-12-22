/**********************************************************************************
/*		File Name 		: XTweenExporter.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-27
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public enum EasingType
{
	Linear,
	Back,
	Bounce,
	Circ,
	Cubic,
	Elastic,
	Expo,
	Quad,
	Quart,
	Quint,
	Sine
}

public enum EasingInOutType
{
	In,
	Out,
	InOut,
	OutIn
}

public class EasingData
{
	/************************************************************************
	*	 	 	 	 	Static Variable Declaration	 	 	 	 	 	    *
	************************************************************************/
	private static Dictionary<EasingType, Dictionary<EasingInOutType,IEasing>> _dic= new Dictionary<EasingType, Dictionary<EasingInOutType,IEasing>>
	{
		{EasingType.Linear, new Dictionary<EasingInOutType,IEasing>
		{
			{EasingInOutType.In,Linear.easeIn},{EasingInOutType.Out,Linear.easeOut},
			{EasingInOutType.InOut,Linear.easeInOut},{EasingInOutType.OutIn,Linear.easeOutIn}
		}},
		{EasingType.Back, new Dictionary<EasingInOutType,IEasing>
		{
			{EasingInOutType.In,Back.easeIn},{EasingInOutType.Out,Back.easeOut},
			{EasingInOutType.InOut,Back.easeInOut},{EasingInOutType.OutIn,Back.easeOutIn}
		}},
		{EasingType.Bounce, new Dictionary<EasingInOutType,IEasing>
		{
			{EasingInOutType.In,Bounce.easeIn},{EasingInOutType.Out,Bounce.easeOut},
			{EasingInOutType.InOut,Bounce.easeInOut},{EasingInOutType.OutIn,Bounce.easeOutIn}
		}},
		{EasingType.Circ, new Dictionary<EasingInOutType,IEasing>
		{
			{EasingInOutType.In,Circ.easeIn},{EasingInOutType.Out,Circ.easeOut},
			{EasingInOutType.InOut,Circ.easeInOut},{EasingInOutType.OutIn,Circ.easeOutIn}
		}},
		{EasingType.Cubic, new Dictionary<EasingInOutType,IEasing>
		{
			{EasingInOutType.In,Cubic.easeIn},{EasingInOutType.Out,Cubic.easeOut},
			{EasingInOutType.InOut,Cubic.easeInOut},{EasingInOutType.OutIn,Cubic.easeOutIn}
		}},
		{EasingType.Elastic, new Dictionary<EasingInOutType,IEasing>
		{
			{EasingInOutType.In,Elastic.easeIn},{EasingInOutType.Out,Elastic.easeOut},
			{EasingInOutType.InOut,Elastic.easeInOut},{EasingInOutType.OutIn,Elastic.easeOutIn}
		}},
		{EasingType.Expo, new Dictionary<EasingInOutType,IEasing>
		{
			{EasingInOutType.In,Expo.easeIn},{EasingInOutType.Out,Expo.easeOut},
			{EasingInOutType.InOut,Expo.easeInOut},{EasingInOutType.OutIn,Expo.easeOutIn}
		}},
		{EasingType.Quad, new Dictionary<EasingInOutType,IEasing>
		{
			{EasingInOutType.In,Quad.easeIn},{EasingInOutType.Out,Quad.easeOut},
			{EasingInOutType.InOut,Quad.easeInOut},{EasingInOutType.OutIn,Quad.easeOutIn}
		}},
		{EasingType.Quart, new Dictionary<EasingInOutType,IEasing>
		{
			{EasingInOutType.In,Quart.easeIn},{EasingInOutType.Out,Quart.easeOut},
			{EasingInOutType.InOut,Quart.easeInOut},{EasingInOutType.OutIn,Quart.easeOutIn}
		}},
		{EasingType.Quint, new Dictionary<EasingInOutType,IEasing>
		{
			{EasingInOutType.In,Quint.easeIn},{EasingInOutType.Out,Quint.easeOut},
			{EasingInOutType.InOut,Quint.easeInOut},{EasingInOutType.OutIn,Quint.easeOutIn}
		}},
		{EasingType.Sine, new Dictionary<EasingInOutType,IEasing>
		{
			{EasingInOutType.In,Sine.easeIn},{EasingInOutType.Out,Sine.easeOut},
			{EasingInOutType.InOut,Sine.easeInOut},{EasingInOutType.OutIn,Sine.easeOutIn}
		}}
	};
	
	/************************************************************************
	*	 	 	 	 	Static Method Declaration	 	 	 	     	 	*
	************************************************************************/
	public static Dictionary<EasingType,Dictionary<EasingInOutType,IEasing>> Dic
	{
		get
		{
			return _dic;
		}
	}

	public static string[] GetEnumNameList<T>() where T : struct, IConvertible
	{
		int length = Enum.GetNames(typeof(T)).Length;
		string[] enums = new string[length];
		for ( int i = 0; i < length; ++i )
		{
			enums[i] = Enum.GetName( typeof(T), i );
		}
		return enums;
	}
	
	public static List<string> EasingList
	{
		get
		{
			return new List<string>(GetEnumNameList<EasingType>());
		}
	}

	public static List<string> EasingInOutList
	{
		get
		{
			return new List<string>(GetEnumNameList<EasingInOutType>());
		}
	}

	public static IEasing GetEasing(EasingType type, EasingInOutType inOutType)
	{
		return _dic[type][inOutType];
	}


	/************************************************************************
	*	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
	************************************************************************/
    
	/************************************************************************
	*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
	************************************************************************/
		
	/************************************************************************
	*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	************************************************************************/
		
	/************************************************************************
	*	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
	************************************************************************/
    
	/************************************************************************
	*	 	 	 	 	Coroutine Declaration	 	  			 	 		*
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Private Method Declaration	 	 	 	 	 		*
	************************************************************************/
    
	/************************************************************************
	*	 	 	 	 	Protected Method Declaration	 	 	 	 	 	*
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Public Method Declaration	 	 	 	 	 		*
	************************************************************************/
    
}