using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XTweenExampleManager : MonoBehaviour 
{
	private Vector3 _defaultPos;
	private Vector3 _defaultScale;
	private Vector3 _defaultAngle;
	public GameObject moveObj;
	public Text text;

	void Start () 
	{
		Transform trans = this.moveObj.transform;
		this._defaultPos = trans.localPosition;
		this._defaultScale = trans.localScale;
		this._defaultAngle = trans.localEulerAngles;
	}

	private void Reset()
	{
		Transform trans = this.moveObj.transform;
		trans.localPosition = this._defaultPos;
		trans.localScale = this._defaultScale;
		trans.localEulerAngles = this._defaultAngle;
	}

	public void ButtonMoveClickHandler()
	{
		this.Test();
	}

	void Test()
	{
		XTween.Tween(XObjectHash.New.Add("value", 10f, 200f), UpdateValue).Play();
	}

	private void UpdateValue( XObjectHash hash )
	{
		Debug.Log(hash.Now("value"));
	}
	
	public void Receiver( string message )
	{
		this.text.text = message;
	}
}
