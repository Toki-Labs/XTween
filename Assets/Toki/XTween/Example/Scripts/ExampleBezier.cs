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

public class ExampleBezier : ExampleBase
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
	protected override void Initialize()
	{
		this.uiContainer.defaultEasingType = (int)EasingType.Expo;
	}

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
		yield return new WaitForSeconds(0.5f);
		TweenUIData data = this.uiContainer.Data;
		if( this.container2D.activeSelf )
		{
			XHash hash = XHash.New.AddX(800f).AddY(300f)
						.AddControlPointX(-1300f,550f).AddControlPointY(550f,-800f);
			this._tween = XTween.To(this.target2D, hash, data.time, data.Easing);
			this._tween.Play();
		}
		else
		{
			XHash hash = XHash.New.AddX(200f).AddY(50f).AddZ(-1500f)
						.AddControlPointX(-1000f,550f).AddControlPointY(550f,-300f);
			this._tween = XTween.To(this.target3D, hash, data.time, data.Easing);
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
		string input;
		if(this.uiContainer.is3D)
		{
			input =
			"<color=#43C9B0>XHash</color> hash = XHash.New<color=#DCDC9D>.AddX(</color><color=#A7CE89>200f</color><color=#DCDC9D>).AddY(</color><color=#A7CE89>50f</color><color=#DCDC9D>).AddZ(</color><color=#A7CE89>-1500f</color><color=#DCDC9D>)</color>\n" +
			"\t\t\t\t\t\t  <color=#DCDC9D>.AddControlPointX(</color><color=#A7CE89>-1000f,550f</color><color=#DCDC9D>).AddControlPointY(</color><color=#A7CE89>550f,-300f</color><color=#DCDC9D>);</color>\n" +
			"XTween<color=#DCDC9D>.To(</color>target3D, hash, <color=#A7CE89>"+ data.time +"f,</color> "+ easing +"<color=#DCDC9D>).Play()</color>;";
		}
		else
		{
			input =
			"<color=#43C9B0>XHash</color> hash = XHash.New<color=#DCDC9D>.AddX(</color><color=#A7CE89>800f</color><color=#DCDC9D>).AddY(</color><color=#A7CE89>300f</color><color=#DCDC9D>)</color>\n" +
			"\t\t\t\t\t\t  <color=#DCDC9D>.AddControlPointX(</color><color=#A7CE89>-1300f,550f</color><color=#DCDC9D>).AddControlPointY(</color><color=#A7CE89>550f,-800f</color><color=#DCDC9D>);</color>\n" +
			"XTween<color=#DCDC9D>.To(</color>target2D, hash, <color=#A7CE89>"+ data.time +"f,</color> "+ easing +"<color=#DCDC9D>).Play()</color>;";
		}
		this.textCode.text = input;
	}
}