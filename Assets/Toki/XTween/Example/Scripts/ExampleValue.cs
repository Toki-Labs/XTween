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

public class ExampleValue : ExampleBase
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
		this.sprite = this.target2D.GetComponent<SpriteRenderer>();
		this.uiContainer.defaultEasingType = (int)EasingType.Cubic;
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
		XObjectHash hash = XObjectHash.New.Add("r",1f,0.56f).Add("g",1f,0.83f);
		this._tween = XTween.Tween(hash,UpdateColor,data.time,data.Easing);
		this._tween.Play();
	}

	void UpdateColor(XObjectHash hash)
	{
		Color color = sprite.color;
		color.r = hash.Now("r");
		color.g = hash.Now("g");
		sprite.color = color;
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
			"<color=#43C9B0>XObjectHash</color> hash = XObjectHash.New<color=#A7CE89>.Add(</color><color=#CE9178>\"r\"</color>,<color=#A7CE89>1f</color>,<color=#A7CE89>0.56f</color><color=#A7CE89>).Add(</color><color=#CE9178>\"g\"</color>,<color=#A7CE89>1f</color>,<color=#A7CE89>0.83f</color><color=#A7CE89>);</color>\n" +
			"XTween<color=#A7CE89>.Tween(</color>hash, UpdateColor, <color=#A7CE89>"+ data.time +"f,</color> "+ easing +"<color=#DCDC9D>).Play()</color>;\n" +
			"\n" +
			"<color=#3F9CD6>void</color> <color=#A7CE89>UpdateColor(</color><color=#43C9B0>XObjectHash</color> hash<color=#A7CE89>)\n" +
			"{</color>\n" +
			"\t\t<color=#43C9B0>Color</color> color = sprite.color;\n" +
			"\t\tcolor.r = hash<color=#A7CE89>.Now(</color><color=#CE9178>\"r\"</color>);\n" +
			"\t\tcolor.g = hash<color=#A7CE89>.Now(</color><color=#CE9178>\"g\"</color>);\n" +
			"\t\tsprite.color = color;\n" +
			"<color=#A7CE89>}</color>";
		this.textCode.text = input;
	}
}