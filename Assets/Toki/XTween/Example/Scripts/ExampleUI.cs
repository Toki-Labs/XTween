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

public class ExampleUI : ExampleBase
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
	private Vector3 _positionButton;
	private Vector3 _sizeButton;
	private Vector2 _offsetMinDropdown;
	private Vector2 _offsetMaxDropdown;
    
	/************************************************************************
	*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
	************************************************************************/
		
	/************************************************************************
	*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	************************************************************************/
	public Text textCode;
	public GameObject button;
	public GameObject dropdown;
		
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
		this.uiContainer.dropdownContainer.gameObject.SetActive(false);
		this.uiContainer.dropdownEasing.dropdown.value = (int)EasingType.Bounce;
		yield return null;
		RectTransform transButton = this.button.transform as RectTransform;
		this._positionButton = transButton.anchoredPosition3D;
		this._sizeButton = transButton.sizeDelta;
		RectTransform transDropdown = this.dropdown.transform as RectTransform;
		this._offsetMinDropdown = transDropdown.offsetMin;
		this._offsetMaxDropdown = transDropdown.offsetMax;
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
		RectTransform transButton = this.button.transform as RectTransform;
		transButton.anchoredPosition3D = this._positionButton;
		transButton.sizeDelta = this._sizeButton;
		RectTransform transDropdown = this.dropdown.transform as RectTransform;
		transDropdown.offsetMin = this._offsetMinDropdown;
		transDropdown.offsetMax = this._offsetMaxDropdown;
		yield return new WaitForSeconds(0.5f);
		TweenUIData data = this.uiContainer.Data;
		XHash hashButton = XHash.New.AddX(400f).AddY(-250f).AddWidth(800f).AddHeight(400f);
		XHash hashDropdown = XHash.New.AddLeft(2000f).AddRight(300f).AddTop(250f).AddBottom(790f);
		this._tween =XTween.ParallelTweens
		(
			false,
			XTween.To(this.button, hashButton, data.time, data.Easing),
			XTween.To(this.dropdown, hashDropdown, data.time, data.Easing)
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
			"<color=#43C9B0>XHash</color> hashButton = XHash.New<color=#DCDC9D>.AddX(<color=#A7CE89>400f</color>).AddY(<color=#A7CE89>-250f</color>).AddWidth(<color=#A7CE89>800f</color>).AddHeight(<color=#A7CE89>400f</color>);</color>\n" +
			"XTween<color=#DCDC9D>.To(</color>button, hashButton, <color=#DCDC9D>" + data.time +"f,</color> "+ easing +"<color=#DCDC9D>).Play();</color>\n" +
			"<color=#43C9B0>XHash</color> hashDropdown = XHash<color=#DCDC9D>.New.AddLeft(<color=#A7CE89>2000f</color>).AddRight(<color=#A7CE89>300f</color>).AddTop(<color=#A7CE89>250f</color>).AddBottom(<color=#A7CE89>790f</color>);</color>\n" +
			"XTween<color=#DCDC9D>.To(</color>dropdown, hashDropdown, <color=#A7CE89>"+ data.time +"f,</color> "+ easing +"<color=#DCDC9D>).Play();</color>";

		this.textCode.text = input;
	}
}