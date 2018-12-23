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

public class ExampleAngle : ExampleBase
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
	private Vector3 _angle2D;
	private Vector3 _angle3D;
    
	/************************************************************************
	*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
	************************************************************************/
		
	/************************************************************************
	*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	************************************************************************/
	public Text textCode;
		
	/************************************************************************
	*	 	 	 	 	Getter & Setter Declaration	 	 	 	 	 		*
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Initialize & Destroy Declaration	 	 	 		*
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Life Cycle Method Declaration	 	 	 	 	 	*
	************************************************************************/
	protected override void Initialize()
	{
		this.uiContainer.defaultEasingType = (int)EasingType.Back;
	}

	protected override IEnumerator StartExample()
	{
		yield return null;
		this._angle2D = this.target2D.transform.localEulerAngles;
		this._angle3D = this.target3D.transform.localEulerAngles;
	}
    
	/************************************************************************
	*	 	 	 	 	Coroutine Declaration	 	  			 	 		*
	************************************************************************/
	protected override IEnumerator CoroutineStart()
	{
		if( this._tween != null )
		{
			this._tween.Stop();
			this._tween = null;
		}
		this.target2D.transform.localEulerAngles = this._angle2D;
		this.target3D.transform.localEulerAngles = this._angle3D;
		yield return new WaitForSeconds(0.5f);
		TweenUIData data = this.uiContainer.Data;
		if( this.container2D.activeSelf )
		{
			this._tween = XTween.To(this.target2D, XHash.New.AddRotationZ(400f), data.time, data.Easing);
			this._tween.Play();
		}
		else
		{
			this._tween = XTween.To(this.target3D, XHash.New.AddRotationY(-70f), data.time, data.Easing);
			this._tween.Play();
		}
	}
	
	/************************************************************************
	*	 	 	 	 	Private Method Declaration	 	 	 	 	 		*
	************************************************************************/
    
	/************************************************************************
	*	 	 	 	 	Protected Method Declaration	 	 	 	 	 	*
	************************************************************************/
	
	/************************************************************************
	*	 	 	 	 	Public Method Declaration	 	 	 	 	 		*
	************************************************************************/
	public override void UIChangeHandler()
	{
		TweenUIData data = this.uiContainer.Data;
		string easing = data.easingType.ToString() + ".ease" + data.inOutType.ToString();
		string input = this.uiContainer.is3D ?
			"XTween<color=#DCDC9D>.To(</color>target3D, XHash.New<color=#DCDC9D>.AddRotationY(</color><color=#A7CE89>-70f</color><color=#DCDC9D>), "+ data.time +"f,</color> "+ easing +"<color=#DCDC9D>).Play()</color>;" :
			"XTween<color=#DCDC9D>.To(</color>target2D, XHash.New<color=#DCDC9D>.AddRotationZ(</color><color=#A7CE89>400f</color><color=#DCDC9D>), "+ data.time +"f,</color> "+ easing +"<color=#DCDC9D>).Play()</color>;";
		this.textCode.text = input;
	}
}