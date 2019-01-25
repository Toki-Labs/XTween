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

public class ExamplePosition : ExampleBase
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
	private Vector3 _position2D;
	private Vector3 _position3D;
    
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
	protected override IEnumerator StartExample()
	{
		yield return null;
		this._position2D = this.target2D.transform.localPosition;
		this._position3D = this.target3D.transform.localPosition;
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
		this.target2D.transform.localPosition = this._position2D;
		this.target3D.transform.localPosition = this._position3D;
		yield return new WaitForSeconds(0.2f);
		TweenUIData data = this.uiContainer.Data;
		if( this.container2D.activeSelf )
		{
			this._tween = this.target2D.ToPosition2D(800f, 300f, data.time, data.Easing).Play();
		}
		else
		{
			this._tween = this.target3D.ToPosition3D(200f, 50f, -1500, data.time, data.Easing).Play();
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
			"XTween<color=#DCDC9D>.To(</color>target3D, XHash.New<color=#DCDC9D>.AddX(</color><color=#A7CE89>800f</color><color=#DCDC9D>).AddY(</color><color=#A7CE89>300f</color><color=#DCDC9D>).AddZ(</color><color=#A7CE89>-1500f</color><color=#DCDC9D>), "+ data.time +"f,</color> "+ easing +"<color=#DCDC9D>).Play()</color>;" :
			"XTween<color=#DCDC9D>.To(</color>target2D, XHash.New<color=#DCDC9D>.AddX(</color><color=#A7CE89>800f</color><color=#DCDC9D>).AddY(</color><color=#A7CE89>300f</color><color=#DCDC9D>), "+ data.time +"f,</color> "+ easing +"<color=#DCDC9D>).Play()</color>;";
		this.textCode.text = input;
	}
}