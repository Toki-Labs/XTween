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

public class ExampleEvent : ExampleBase
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
    
	/************************************************************************
	*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
	************************************************************************/
		
	/************************************************************************
	*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	************************************************************************/
	public Text textCode;
	public Text textLog;
		
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
		this.uiContainer.dropdownContainer.gameObject.SetActive(false);
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
		this.target3D.transform.localPosition = this._position3D;
		this.textLog.text = "Tween Ready";
		yield return new WaitForSeconds(0.5f);
		TweenUIData data = this.uiContainer.Data;
		this.textLog.text = "Tweening...";
		this._tween = XTween.To(this.target3D, XHash.New.AddX(200f).AddY(50f).AddZ(-1500f), data.time, data.Easing);
		this._tween.AddOnComplete(Executor<string>.New(TweenComplete, "Tween Complete!"));
		this._tween.Play();
	}

	void TweenComplete(string message)
	{
		textLog.text = message;
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
			"<color=#43C9B0>IAni</color> tween = XTween<color=#DCDC9D>.To(</color>target2D, XHash.New<color=#DCDC9D>.AddX(</color><color=#A7CE89>800f</color><color=#DCDC9D>).AddY(</color><color=#A7CE89>300f</color><color=#DCDC9D>), "+ data.time +"f,</color> "+ easing +"<color=#DCDC9D>)</color>;\n" +
			"tween.onComplete = Executor<<color=#3F9CD6>string</color>><color=#DCDC9D>.New(</color>TweenComplete, <color=#CE9178>\"Tween Complete!\"</color>);\n" +
			"tween<color=#DCDC9D>.onPlay();</color>\n" +
			"\n" +
			"<color=#3F9CD6>void</color> <color=#DCDC9D>TweenComplete(</color><color=#3F9CD6>string</color> message)\n" +
			"{\n" +
			"\t\ttextLog.text = message;\n" +
			"}";
		this.textCode.text = input;
	}
}