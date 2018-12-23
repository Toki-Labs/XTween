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

public class ExampleCombination : ExampleBase
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
	private Vector3 _scale2D;
	private Vector3 _angle2D;
	private Vector3 _position3D;
	private Vector3 _scale3D;
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
	protected override IEnumerator StartExample()
	{
		yield return null;
		this._position2D = this.target2D.transform.localPosition;
		this._scale2D = this.target2D.transform.localScale;
		this._angle2D = this.target2D.transform.localEulerAngles;
		this._position3D = this.target3D.transform.localPosition;
		this._scale3D = this.target3D.transform.localScale;
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
		this.target2D.transform.localPosition = this._position2D;
		this.target2D.transform.localScale = this._scale2D;
		this.target2D.transform.localEulerAngles = this._angle2D;
		this.target3D.transform.localPosition = this._position3D;
		this.target3D.transform.localScale = this._scale3D;
		this.target3D.transform.localEulerAngles = this._angle3D;
		yield return new WaitForSeconds(0.5f);
		TweenUIData data = this.uiContainer.Data;
		if( this.container2D.activeSelf )
		{
			XHash hash = XHash.New
				.AddX(800f).AddY(300f)
				.AddScaleX(400f).AddScaleY(400f)
				.AddRotationZ(330f);
			this._tween = XTween.To(this.target2D, hash, data.time, data.Easing);
			this._tween.Play();
		}
		else
		{
			XHash hash = XHash.New
				.AddX(200f).AddY(50f).AddZ(-1000f)
				.AddScaleX(400f).AddScaleY(400f).AddScaleZ(400f)
				.AddRotationY(-110f);
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
			"<color=#43C9B0>XHash</color> hash = XHash.New\n" + 
			"\t\t<color=#DCDC9D>.AddX(</color><color=#A7CE89>200f</color><color=#DCDC9D>).AddY(</color><color=#A7CE89>50f</color><color=#DCDC9D>).AddZ(</color><color=#A7CE89>-1000f</color><color=#DCDC9D>)\n" +
			"\t\t.AddScaleX(</color><color=#A7CE89>400f</color><color=#DCDC9D>).AddScaleY(</color><color=#A7CE89>400f</color><color=#DCDC9D>).AddScaleZ(</color><color=#A7CE89>400f</color><color=#DCDC9D>)\n" +
			"\t\t.AddRotationY(</color><color=#A7CE89>-110f</color><color=#DCDC9D>)</color>;\n" +
			"XTween<color=#DCDC9D>.To(</color>target3D, hash, <color=#A7CE89>"+ data.time +"f,</color> "+ easing +"<color=#DCDC9D>).Play()</color>;";

		}
		else
		{
			input =
			"<color=#43C9B0>XHash</color> hash = XHash.New\n" + 
			"\t\t<color=#DCDC9D>.AddX(</color><color=#A7CE89>800f</color><color=#DCDC9D>).AddY(</color><color=#A7CE89>300f</color><color=#DCDC9D>)\n" +
			"\t\t.AddScaleX(</color><color=#A7CE89>400f</color><color=#DCDC9D>).AddScaleY(</color><color=#A7CE89>400f</color><color=#DCDC9D>)\n" +
			"\t\t.AddRotationZ(</color><color=#A7CE89>330f</color><color=#DCDC9D>)</color>;\n" +
			"XTween<color=#DCDC9D>.To(</color>target2D, hash, <color=#A7CE89>"+ data.time +"f,</color> "+ easing +"<color=#DCDC9D>).Play()</color>;";
		}
		this.textCode.text = input;
	}
}