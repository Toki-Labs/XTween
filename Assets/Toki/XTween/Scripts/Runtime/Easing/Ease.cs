using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Toki.Tween;

public class Ease
{
	public static IEasing Get(EaseName name)
	{
		EaseCustom data = XTweenEditorData.Instance.GetEasingData(name);
		if( data == null ) return new EaseCustom();
		else return data;
	}
}

namespace Toki.Tween
{
	public class EaseCustom : IEasing
	{
		private AnimationCurve _curve;
		public EaseCustom()
		{
			this._curve = new AnimationCurve (new Keyframe (0f, 0f, 0f, 1f), new Keyframe (1f, 1f, 1f, 0f));
		}

		public EaseCustom(AnimationCurve curve)
		{
			this._curve = curve;
		}

		public float Calculate( float arg0, float arg1, float arg2, float arg3 )
		{
			return _curve.Evaluate(arg0);
		}
	}
}