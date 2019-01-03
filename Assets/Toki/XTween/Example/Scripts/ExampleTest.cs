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
// using DG.Tweening;

public class ExampleTest : ExampleBase
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
	public Camera camera3D;
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
		yield return new WaitForSeconds(0.5f);
		TweenUIData data = this.uiContainer.Data;
		if( this.container2D.activeSelf )
		{
			this._tween = XTween.To(this.target2D, XHash.New.AddX(800f).AddY(300f), data.time, data.Easing);
			this._tween.Play();
		}
		else
		{
			// this._tween = XTween.To(this.target3D, hash, data.time, data.Easing);
			// XObjectHash hash = XObjectHash.New.Add("fieldOfView", 6f);
			// this._tween = XTween.ValueTo<Camera>(this.camera3D,hash,data.time,data.Easing);
			// this._tween.OnComplete = Executor.New(() => this.StartCoroutine(this.Test()));
			// yield return this._tween.WaitForGotoAndPlay(0.2f);
			yield return XTween.SerialTweens(false, 
				this.target3D.To(XHash.New.Scale(300f,100f,400f), data.time),
				this.target3D.To(XHash.New.Position(200f,50f,-1500f), data.time)).WaitForPlay();
			// this.target3D.transform.DOLocalMove(new Vector3(200f,50f,-1500f), data.time).Play();
			Debug.Log("Test");
		}
	}

	private IEnumerator Test()
	{
		yield return new WaitForSeconds(1f);
		yield return this._tween.WaitForGotoAndPlay(0.3f);
	}

	private void StartiTween(GameObject target)
	{
		Hashtable hash = new Hashtable();
		hash.Add("x", 0f);
		hash.Add("y", 0f);
		hash.Add("z", -200f);
		hash.Add("time", 1f);
		hash.Add("islocal", true);
		hash.Add("easetype", "easeOutElastic");
		hash.Add("oncomplete", "OnComplete");
		hash.Add("oncompletetarget", this.gameObject);
		// iTween.MoveTo(target, hash);
	}
	
	void OnComplete()
	{
		// _isBreak = true;
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