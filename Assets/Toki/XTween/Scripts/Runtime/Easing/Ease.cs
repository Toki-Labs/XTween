using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Toki.Tween;

public class Ease
{
	public static IEasing Get()
	{
		XTweenEditorData data = Resources.Load<GameObject>("XTweenData").GetComponent<XTweenEditorData>();
		return new EaseCustom(data.easingDataList[0].animationCurve);
	}
}

public class EaseCustom : IEasing
{
	private AnimationCurve _curve;

	public EaseCustom(AnimationCurve curve)
	{
		this._curve = curve;
	}

	public float Calculate( float arg0, float arg1, float arg2, float arg3 )
	{
		return _curve.Evaluate(arg0);
	}
}