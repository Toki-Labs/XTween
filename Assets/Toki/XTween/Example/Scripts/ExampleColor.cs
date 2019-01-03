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

public class ExampleColor : ExampleBase
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
	private SpriteRenderer sprite;
	private Color _color;
	private float _defaultFieldOfView;
	
	/************************************************************************
	*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
	************************************************************************/
		
	/************************************************************************
	*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	************************************************************************/
	public Text textCode;
	public RectTransform transInputCode;
		
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
		this.sprite = this.target2D.GetComponent<SpriteRenderer>();
		this.uiContainer.defaultEasingType = (int)EasingType.Linear;
	}

	protected override IEnumerator StartExample()
	{
		yield return null;
		this.uiContainer.dropdownContainer.value = 1;
		this.uiContainer.dropdownContainer.gameObject.SetActive(false);
		this._color = this.sprite.color;
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
		this.sprite.color = this._color;
		yield return new WaitForSeconds(0.5f);
		TweenUIData data = this.uiContainer.Data;
		XColorHash hash = XColorHash.New.AddRed(0.56f).AddGreen(0.83f).AddAlpha(1f);
		this._tween = XTween.To(sprite, hash, data.time, data.Easing);
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
		RectTransform trans = this.transInputCode;
		TweenUIData data = this.uiContainer.Data;
		string easing = data.easingType.ToString() + ".ease" + data.inOutType.ToString();
		string input = 
			"<color=#43C9B0>XColorHash</color> hash = XColorHash.New<color=#DCDC9D>.AddRed(</color><color=#A7CE89>0.56f</color><color=#DCDC9D>).AddGreen(</color><color=#A7CE89>0.83f</color><color=#DCDC9D>).AddAlpha(</color><color=#A7CE89>1f</color><color=#DCDC9D>);</color>\n" +
			"XTween<color=#DCDC9D>.ColorTo(</color>sprite, hash, <color=#A7CE89>"+ data.time +"f,</color> "+ easing +"<color=#DCDC9D>).Play()</color>;";
		this.textCode.text = input;
	}
}