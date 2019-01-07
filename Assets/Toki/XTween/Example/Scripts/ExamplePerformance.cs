/**********************************************************************************
/*		File Name 		: XTweenExporter.cs
/*		Author 			: Robin
/*		Description 	: 
/*		Created Date 	: 2016-7-27
/*		Modified Date 	: 
/**********************************************************************************/
// #define PERFORMANCE_TEST

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
#if PERFORMANCE_TEST
using DG.Tweening;
#endif

public class ObjectSet
{
	public GameObject obj;
	public Vector3 pos;

	public void SetDefault()
	{
		Transform trans = this.obj.transform;
		trans.localPosition = this.pos;
	}
}

public class ExamplePerformance : ExampleBase
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
	private bool _isBreak = false;
	private List<ObjectSet> _objList;
    
	/************************************************************************
	*	 	 	 	 	Protected Variable Declaration	 	 	 	 	 	*
	************************************************************************/
		

	/************************************************************************
	*	 	 	 	 	Public Variable Declaration	 	 	 	 	 		*
	************************************************************************/
	public Text textCode;
	public RectTransform rectUI;
		
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
		XTween.Initialize(1000);
		yield return null;
		this._position2D = this.target2D.transform.localPosition;
		this._position3D = this.target3D.transform.localPosition;
	}
    
	/************************************************************************
	*	 	 	 	 	Coroutine Declaration	 	  			 	 		*
	************************************************************************/
	//XTween 1000 - x,y,z - Start: 122B|2.7ms, End: 0B|1ms, Update: 0.8ms
	//DTween 1000 - x,y,z - Start: 0.6MB|1.8ms, End: 0B|1ms, Update: 0.8ms
	//iTween 1000 - x,y,z - Start: 4.7MB|158ms, End: 169.9KB|80ms, Update: 1.7ms

	protected override IEnumerator CoroutineStart()
	{
		if( this._tween != null )
		{
			this._tween.Stop();
			this._tween = null;
		}
		this.target2D.transform.localPosition = this._position2D;
		this.target3D.transform.localPosition = this._position3D;
		int length = 1000;

		if( this._objList == null )
		{
			this._objList = new List<ObjectSet>();
			for ( int i = 0; i < length; ++i )
			{
				GameObject target = GameObject.Instantiate(this.target3D);
				Transform trans = target.transform;
				trans.SetParent( this.target3D.transform.parent );
				Vector3 pos = new Vector3( UnityEngine.Random.Range(-1200f,1200f), UnityEngine.Random.Range(-500f,500f), -100f );
				trans.localPosition = pos;
				trans.localScale = Vector3.one * 100f;
				ObjectSet set = new ObjectSet();
				set.obj = target;
				set.pos = pos;
				this._objList.Add(set);
			}
		}
		else
		{
			for ( int i = 0; i < length; ++i )
			{
				this._objList[i].SetDefault();
			}
		}
		WaitForSeconds wait = new WaitForSeconds(0.1f);
		GC.Collect();
		yield return new WaitForSeconds(0.5f);
		TweenUIData data = this.uiContainer.Data;


		for ( int i = 0; i < length; ++i )
		{
			this.StartXTween(this._objList[i].obj);
			// this.StartiTween(this._objList[i].obj);
			// this.StartDOTween(this._objList[i].obj);
		}
	
		while( true )
		{
			if( _isBreak )
			{
				yield return wait;
				GC.Collect();
				yield return wait;
				Debug.Break();
				_isBreak = false;
				yield break;
			}
			yield return null;
		}
	}

	private void StartXTween(GameObject target)
	{
		XTween.Reverse(target.To(XHash.Position(0f,0f,-400f), 1f, Ease.ElasticOut)).Play();
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
	#if PERFORMANCE_TEST
		iTween.MoveTo(target, hash);
	#endif
	}

	private void StartDOTween(GameObject target)
	{
	#if PERFORMANCE_TEST
		target.transform.DOLocalMove(new Vector3(0f,0f,-200f), 1f).SetEase(DG.Tweening.Ease.OutElastic).Play();
	#endif
	}
	
	void OnComplete()
	{
		_isBreak = true;
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