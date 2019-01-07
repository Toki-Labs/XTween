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
	private float _defaultFieldOfView;
	
	/************************************************************************
	*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
	************************************************************************/
		
	/************************************************************************
	*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	************************************************************************/
	public Text textCode;
	public RectTransform transInputCode;
	public Camera camera3D;
		
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
		this._defaultFieldOfView = this.camera3D.fieldOfView;
	}

	protected override IEnumerator StartExample()
	{
		yield return null;
		this.uiContainer.dropdownContainer.value = 1;
		// this.uiContainer.dropdownContainer.gameObject.SetActive(false);
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
		this.camera3D.fieldOfView = this._defaultFieldOfView;
		yield return new WaitForSeconds(0.5f);
		TweenUIData data = this.uiContainer.Data;
		if( this.container2D.activeSelf )
		{
			XObjectHash hash = XObjectHash.New.Add("r",1f,0.56f).Add("g",1f,0.83f);
			this._tween = XTween.ToValueMulti(hash,UpdateColor,data.time,data.Easing);
			this._tween.Play();
		}
		else
		{
			XObjectHash hash = XObjectHash.New.Add("fieldOfView", 6f);
			this._tween = XTween.ToPropertyMulti<Camera>(this.camera3D,hash,data.time,data.Easing);
			this._tween.Play();
		}
	}

	private void UpdateColor(XObjectHash hash)
	{
		if( sprite == null )
		{
			this._tween.Stop();
			return;
		}
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
		RectTransform trans = this.transInputCode;
		TweenUIData data = this.uiContainer.Data;
		string easing = data.easingType.ToString() + ".ease" + data.inOutType.ToString();
		string input = this.container2D.activeSelf ?
			"<color=#43C9B0>XObjectHash</color> hash = XObjectHash.New<color=#A7CE89>.Add(</color><color=#CE9178>\"r\"</color>,<color=#A7CE89>1f</color>,<color=#A7CE89>0.56f</color><color=#A7CE89>).Add(</color><color=#CE9178>\"g\"</color>,<color=#A7CE89>1f</color>,<color=#A7CE89>0.83f</color><color=#A7CE89>);</color>\n" +
			"XTween<color=#DCDC9D>.ValueTo(</color>hash, UpdateColor, <color=#A7CE89>"+ data.time +"f,</color> "+ easing +"<color=#DCDC9D>).Play()</color>;\n" +
			"\n" +
			"<color=#3F9CD6>void</color> <color=#A7CE89>UpdateColor(</color><color=#43C9B0>XObjectHash</color> hash<color=#A7CE89>)\n" +
			"{</color>\n" +
			"\t\t<color=#43C9B0>Color</color> color = sprite.color;\n" +
			"\t\tcolor.r = hash<color=#DCDC9D>.Now(</color><color=#CE9178>\"r\"</color>);\n" +
			"\t\tcolor.g = hash<color=#DCDC9D>.Now(</color><color=#CE9178>\"g\"</color>);\n" +
			"\t\tsprite.color = color;\n" +
			"<color=#A7CE89>}</color>" :
			"<color=#43C9B0>XObjectHash</color> hash = XObjectHash.New<color=#A7CE89>.Add</color>(<color=#CE9178>\"fieldOfView\"</color>, <color=#A7CE89>6f</color>);\n" +
			"XTween<color=#A7CE89>.ValueTo</color><<color=#43C9B0>Camera</color>>(camera3D, hash, <color=#A7CE89>"+ data.time +"f,</color> "+ easing +"<color=#DCDC9D>).Play()</color>;";

		Vector2 size = trans.sizeDelta;
		size.y = this.container2D.activeSelf ? 510f : 170f;
		trans.sizeDelta = size;
		this.textCode.text = input;
	}
}