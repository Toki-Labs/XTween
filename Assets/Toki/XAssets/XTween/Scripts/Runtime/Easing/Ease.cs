using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Toki.Tween;

public static class Ease
{
	//Quint
	public static IEasing QuintIn { get{return Quint.easeIn;} }
	public static IEasing QuintOut { get{return Quint.easeOut;} }
	public static IEasing QuintInOut { get{return Quint.easeInOut;} }
	public static IEasing QuintOutIn { get{return Quint.easeOutIn;} }

	//Back
	public static IEasing BackIn { get{return Back.easeIn;} }
	public static IEasing BackOut { get{return Back.easeOut;} }
	public static IEasing BackInOut { get{return Back.easeInOut;} }
	public static IEasing BackOutIn { get{return Back.easeOutIn;} }
	public static IEasing BackInWith(float overshot) { return Back.easeInWith(overshot); }
	public static IEasing BackOutWith(float overshot) { return Back.easeOutWith(overshot); }
	public static IEasing BackInOutWith(float overshot) { return Back.easeInOutWith(overshot); }
	public static IEasing BackOutInWith(float overshot) { return Back.easeOutInWith(overshot); }

	//Back
	public static IEasing BounceIn { get{return Bounce.easeIn;} }
	public static IEasing BounceOut { get{return Bounce.easeOut;} }
	public static IEasing BounceInOut { get{return Bounce.easeInOut;} }
	public static IEasing BounceOutIn { get{return Bounce.easeOutIn;} }

	//Circ
	public static IEasing CircIn { get{return Circ.easeIn;} }
	public static IEasing CircOut { get{return Circ.easeOut;} }
	public static IEasing CircInOut { get{return Circ.easeInOut;} }
	public static IEasing CircOutIn { get{return Circ.easeOutIn;} }

	//Cubic
	public static IEasing CubicIn { get{return Cubic.easeIn;} }
	public static IEasing CubicOut { get{return Cubic.easeOut;} }
	public static IEasing CubicInOut { get{return Cubic.easeInOut;} }
	public static IEasing CubicOutIn { get{return Cubic.easeOutIn;} }

	//Elastic
	public static IEasing ElasticIn { get{return Elastic.easeIn;} }
	public static IEasing ElasticOut { get{return Elastic.easeOut;} }
	public static IEasing ElasticInOut { get{return Elastic.easeInOut;} }
	public static IEasing ElasticOutIn { get{return Elastic.easeOutIn;} }
	public static IEasing ElasticInWith(float amplitude=0, float period=0) { return Elastic.easeInWith(amplitude,period); }
	public static IEasing ElasticOutWith(float amplitude=0, float period=0) { return Elastic.easeOutWith(amplitude,period); }
	public static IEasing ElasticInOutWith(float amplitude=0, float period=0) { return Elastic.easeInOutWith(amplitude,period); }
	public static IEasing ElasticOutInWith(float amplitude=0, float period=0) { return Elastic.easeOutInWith(amplitude,period); }
	
	//Expo
	public static IEasing ExpoIn { get{return Expo.easeIn;} }
	public static IEasing ExpoOut { get{return Expo.easeOut;} }
	public static IEasing ExpoInOut { get{return Expo.easeInOut;} }
	public static IEasing ExpoOutIn { get{return Expo.easeOutIn;} }

	//Linear
	public static IEasing Linear { get{return Toki.Tween.Linear.linear;} }

	//Quad
	public static IEasing QuadIn { get{return Quad.easeIn;} }
	public static IEasing QuadOut { get{return Quad.easeOut;} }
	public static IEasing QuadInOut { get{return Quad.easeInOut;} }
	public static IEasing QuadOutIn { get{return Quad.easeOutIn;} }

	//Quart
	public static IEasing QuartIn { get{return Quart.easeIn;} }
	public static IEasing QuartOut { get{return Quart.easeOut;} }
	public static IEasing QuartInOut { get{return Quart.easeInOut;} }
	public static IEasing QuartOutIn { get{return Quart.easeOutIn;} }

	//Sine
	public static IEasing SineIn { get{return Sine.easeIn;} }
	public static IEasing SineOut { get{return Sine.easeOut;} }
	public static IEasing SineInOut { get{return Sine.easeInOut;} }
	public static IEasing SineOutIn { get{return Sine.easeOutIn;} }


	public static IEasing Custom(EaseCustom name)
	{
		EaseCustomData data = XTweenEditorData.Instance.GetEasingData(name);
		if( data == null ) return new EaseCustomData();
		else return data;
	}
}

namespace Toki.Tween
{
	public class EaseCustomData : IEasing
	{
		private AnimationCurve _curve;
		public EaseCustomData()
		{
			this._curve = new AnimationCurve (new Keyframe (0f, 0f, 0f, 1f), new Keyframe (1f, 1f, 1f, 0f));
		}

		public EaseCustomData(AnimationCurve curve)
		{
			this._curve = curve;
		}

		public float Calculate( float time, float arg1, float arg2, float dest )
		{
			return _curve.Evaluate(time * 1f / dest);
		}
	}
}