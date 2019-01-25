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
			// this.target3D.transform.localPosition = this._position3D;
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
			IEasing ease = Ease.ElasticOut;
			yield return wait;
			
			if( this._tween == null )
			{
				this._tween = target3D.ToPosition3D(200, 50, -1500, 3f).SetLock().SetReverse().SetRepeat(2).SetScale(2f).SetDelay(0.5f,1f);
				this._tween.AddOnStop(()=>Debug.Log("Stop"));
				this.isPlay = true;
				yield return this._tween.WaitForPlay();
			}
			else
			{
				if( isPlay )
				{
					isPlay = false;
					this._tween.Stop();
				}
				else
				{
					isPlay = true;
					yield return this._tween.WaitForPlay();
				}
			}
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

	bool isPlay = false;
	private void StartXTween(GameObject target)
	{
		// XHash hash = XHash.New.AddX(200f).AddY(50f).AddZ(-1500f);
		// XHash hash = XHash.New.AddRotationZ(360, true, 1);
		// hash.GetStart().AddRotationZ( -100f );

		// XObjectHash oHash = XObjectHash.New.Add("fieldOfView", 80f);
		// this.camera3D.ToColor("backgroundColor", XColorHash.Color(0.5f, 0.5f, 0.5f, 0.5f), 1f).Play();
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