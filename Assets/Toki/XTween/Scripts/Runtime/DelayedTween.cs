using UnityEngine;
using System.Collections;

public class DelayedTween : TweenDecorator
{
	public DelayedTween( IIAni baseTween, float preDelay, float postDelay ) : base(baseTween, 0)
	{
		_duration = preDelay + baseTween.Duration + postDelay;
		_preDelay = preDelay;
		_postDelay = postDelay;
	}
		
	private float _preDelay;
	private float _postDelay;
		
	public float preDelay
	{
		get { return _preDelay; }
	}
		
	public float postDelay
	{
		get { return _postDelay; }
	}
		
	protected override void InternalUpdate( float time )
	{
		_baseTween.UpdateTween(time - _preDelay);
	}
		
	protected override AbstractTween NewInstance()
	{
		return new DelayedTween(_baseTween.Clone() as IIAni, _preDelay, _postDelay);
	}
}
