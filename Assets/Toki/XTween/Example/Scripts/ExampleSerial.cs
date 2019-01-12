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

public class ExampleSerial : ExampleBase
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
	private Vector3 _position3D;
	private Vector3 _position3DSecond;
    
	/************************************************************************
	*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
	************************************************************************/
		
	/************************************************************************
	*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	************************************************************************/
	public Text textCode;
	public GameObject target3DSecond;
		
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
		this.uiContainer.defaultTime = 0.6f;
		this.uiContainer.defaultEasingType = (int)EasingType.Expo;
		this.uiContainer.dropdownContainer.gameObject.SetActive(false);
	}

	protected override IEnumerator StartExample()
	{
		yield return null;
		this._position3D = this.target3D.transform.localPosition;
		this._position3DSecond = this.target3DSecond.transform.localPosition;
	}
    
	/************************************************************************
	*	 	 	 	 	Coroutine Declaration	 	  			 	 		*
	************************************************************************/
	protected override IEnumerator CoroutineStart()
	{
		if( this._tween != null ) this._tween.Stop();
		this.target3D.transform.localPosition = this._position3D;
		this.target3DSecond.transform.localPosition = this._position3DSecond;
		yield return new WaitForSeconds(0.5f);
		TweenUIData data = this.uiContainer.Data;
		if( this._tween == null )
		{
			this._tween = XTween.SerialTweens
			(
				false,
				target3DSecond.ToPosition3D(-940f, -160f, -500f, data.time, data.Easing),
				target3D.ToPosition3D(200f, 70f, -1500f, data.time, data.Easing)
			).SetLock().Play();
		}
		else
		{
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
		string input = 
		"XTween.SerialTweens\n" +
		"(\n" +
		"\t\t<color=#3F9CD6>false</color>,\n" +
		"\t\tXTween<color=#DCDC9D>.To(</color>target3DSecond, XHash.New<color=#DCDC9D>.AddX(</color><color=#A7CE89>-940f</color><color=#DCDC9D>).AddY(</color><color=#A7CE89>-160f</color><color=#DCDC9D>).AddZ(</color><color=#A7CE89>-500f</color><color=#DCDC9D>)</color>, <color=#A7CE89>"+ data.time +"f,</color> "+ easing +"<color=#DCDC9D>)</color>,\n" +
		"\t\tXTween<color=#DCDC9D>.To(</color>target3D, XHash.New<color=#DCDC9D>.AddX(</color><color=#A7CE89>200f</color><color=#DCDC9D>).AddY(</color><color=#A7CE89>70f</color><color=#DCDC9D>).AddZ(</color><color=#A7CE89>-1500f</color><color=#DCDC9D>)</color>, <color=#A7CE89>"+ data.time +"f,</color> "+ easing +"<color=#DCDC9D>)</color>\n" +
		")<color=#DCDC9D>.Play()</color>;";
		this.textCode.text = input;
	}
}