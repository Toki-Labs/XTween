using UnityEngine;
using System.Collections;

public class ScaledTween : TweenDecorator
{
	public ScaledTween( IIAni baseTween, float scale ) : base(baseTween, 0)
	{
		_duration = baseTween.duration * scale;
		_scale = scale;
	}
		
	private float _scale;
		
	public float scale
	{
		get { return _scale; }
	}
		
	protected override void InternalUpdate( float time )
	{
		_baseTween.UpdateTween(time / scale);
	}
		
	protected override AbstractTween NewInstance()
	{
		return new ScaledTween(_baseTween.Clone() as IIAni, _scale);
	}
}
