/**********************************************************************************
/*		File Name 		: XTweenExporter.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-27
/*		Modified Date 	: 
/**********************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public struct TweenUIData
{
	public float time;
	public EasingType easingType;
	public EasingInOutType inOutType;
	public IEasing Easing
	{
		get
		{
			return EasingData.GetEasing(easingType, inOutType);
		}
	}
}

public class TweenUIContainer : MonoBehaviour
{
	/************************************************************************
	*	 	 	 	 	Static Variable Declaration	 	 	 	 	 	    *
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Static Method Declaration	 	 	 	     	 	*
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Private Variable Declaration	 	 	 	 	 	*
	************************************************************************/
    
	/************************************************************************
	*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
	************************************************************************/
		
	/************************************************************************
	*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	************************************************************************/
	public int defaultEasingType = (int)EasingType.Elastic;
	public InputField inputTime;
	public DropdownEasing dropdownEasing;
	public DropdownInOut dropdownInOut;
	public Dropdown dropdownContainer;
	public Action<string> containerChangeHandler;
	public Action uiChangeHandler;
		
	/************************************************************************
	*	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
	************************************************************************/
	public TweenUIData Data
	{
		get
		{
			TweenUIData data = new TweenUIData();
			data.time = this.InputTimeValue;
			data.easingType = dropdownEasing.EasingType;
			data.inOutType = dropdownInOut.InOutType;
			return data;
		}
	}

	private float InputTimeValue
	{
		get
		{
			string value = this.inputTime.text;
			float valueFloat;
			float.TryParse(value, out valueFloat);
			if( valueFloat <= 0f || valueFloat > 10f )
			{
				valueFloat = 1f;
			}
			return valueFloat;
		}
	}

	public bool is3D
	{
		get
		{
			return this.dropdownContainer.value == 0;
		}
	}
	
	/************************************************************************
	*	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
	************************************************************************/
	void Start()
	{
		this.inputTime.text = "1";
		this.inputTime.onValueChange.AddListener( x=> this.uiChangeHandler() );
		this.dropdownEasing.dropdown.value = this.defaultEasingType;
		this.dropdownEasing.dropdown.onValueChanged.AddListener( x => this.uiChangeHandler() );
		this.dropdownInOut.dropdown.onValueChanged.AddListener( x => this.uiChangeHandler() );
		this.dropdownContainer.onValueChanged.AddListener( x => this.uiChangeHandler() );
		this.uiChangeHandler();
	}
    
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
	public void InputTimeChangeHandler()
	{
		this.inputTime.text = this.InputTimeValue.ToString();
	}
    
	public void DropdownContainerChangeHandler()
	{
		this.containerChangeHandler(this.dropdownContainer.options[this.dropdownContainer.value].text);
	}
}