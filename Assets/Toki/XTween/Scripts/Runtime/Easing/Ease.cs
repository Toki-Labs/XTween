using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Toki.Tween;

public static class Ease
{
	public static IEasing QuintIn { get{return Quint.easeIn;} }
	public static IEasing QuintOut { get{return Quint.easeOut;} }
	
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