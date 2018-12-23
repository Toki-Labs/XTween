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
		if( this._tween != null )
		{
			this._tween.Stop();
			this._tween = null;
		}
		this.target3D.transform.localPosition = this._position3D;
		this.target3DSecond.transform.localPosition = this._position3DSecond;
		yield return new WaitForSeconds(0.5f);
		TweenUIData data = this.uiContainer.Data;
		this._tween = XTween.SerialTweens
		(
			false,
			XTween.To(this.target3DSecond, XHash.New.AddX(-940f).AddY(-160f).AddZ(-500f), data.time, data.Easing),
			XTween.To(this.target3D, XHash.New.AddX(200f).AddY(70f).AddZ(-1500f), data.time, data.Easing)
		);
		this._tween.Play();
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