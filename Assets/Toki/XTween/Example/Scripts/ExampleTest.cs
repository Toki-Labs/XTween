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
		WaitForSeconds wait = new WaitForSeconds(0.1f);
		if( this._tween != null )
		{
			// this._tween.Stop();
			// this._tween = null;
		}

		if( this.target3D != null )
		{
			this.target2D.transform.localPosition = this._position2D;
			this.target3D.transform.localPosition = this._position3D;
		}
		yield return new WaitForSeconds(0.5f);
		TweenUIData data = this.uiContainer.Data;
		if( this.container2D.activeSelf )
		{
			this._tween = XTween.To(this.target2D, XHash.New.AddX(800f).AddY(300f), data.time, data.Easing);
			this._tween.Play();
		}
		else
		{
			Debug.Log("Start");
			IEasing ease = Ease.ElasticOut;
			yield return wait;
			
			// if( this._tween == null ) this._tween = this.camera3D.ToProperty("fieldOfView", 6f, 1f, Ease.ExpoOut).Lock();
			// else this._tween.Reset();
			// yield return this._tween.WaitForPlay();
			// yield return wait;
			// Debug.Break();

			// yield return target3D.To(XHash.New.AddX(200f).AddY(50f).AddZ(-1500f), 1f, Ease.ElasticOut).WaitForPlay();
					// .AddOnComplete(()=>Debug.Break()).Play();

			// XObjectHash hash = XObjectHash.New.Add("fieldOfView", 6f);
			// this._tween = XTween.ValueTo<Camera>(this.camera3D,hash,data.time,data.Easing);
			// this._tween.OnComplete = Executor.New(() => this.StartCoroutine(this.Test()));
			// yield return this._tween.WaitForGotoAndPlay(0.2f);

			// yield return XTween.SerialTweens(false, 
			// 	this.target3D.To(XHash.Scale(300f,100f,400f), data.time).AddOnUpdate(()=>Debug.Log("Tween1")),
			// 	this.target3D.To(XHash.Position(200f,50f,-1500f), data.time).AddOnComplete(()=>Debug.Log("Tween2"))).WaitForPlay();

			// this.target3D.transform.DOLocalMove(new Vector3(200f,50f,-1500f), data.time).Play();

			// Debug.Log("Test");
			// yield return wait;
			// Debug.Break();

			// this.StartiTween(this.target3D);
			this.StartXTween(this.target3D);

		}
	}

	private bool _isBreak = false;
	
	private void StartiTween(GameObject target)
	{
		Hashtable hash = new Hashtable();
		hash.Add("x", 1000f);
		hash.Add("y", 5f);
		hash.Add("z", -1500f);
		hash.Add("time", 1f);
		hash.Add("islocal", true);
		hash.Add("easetype", "easeOutElastic");
		hash.Add("oncomplete", "OnComplete");
		hash.Add("oncompletetarget", this.gameObject);
		// iTween.MoveTo(target, hash);
	}

	private void StartXTween(GameObject target)
	{
		// XHash hash = XHash.New.AddX(200f).AddY(50f).AddZ(-1500f);
		XHash hash = XHash.New.AddRotationZ(360, true, 1);
		hash.GetStart().AddRotationZ( -100f );
		// target.ToPosition(new Vector3(200,50,-1500), 1f).Play();

		XObjectHash oHash = XObjectHash.New.Add("fieldOfView", 80f);
		this.camera3D.ToColor("backgroundColor", XColorHash.Color(0.5f, 0.5f, 0.5f, 0.5f), 1f).Play();
		// target.transform.DOLocalRotate(new Vector3(100,55,-30),1).Play();
		// Debug.Log(target.transform.localEulerAngles);
	}
	
	void OnComplete()
	{
		Debug.Break();
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