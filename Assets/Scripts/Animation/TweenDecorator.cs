using System;
using System.Collections.Generic;

public class TweenDecorator : AbstractTween
{
	public TweenDecorator( IIAni baseTween, float position ) : base( baseTween.ticker, position )
	{
		_baseTween = baseTween;
        _baseTween.decoratorStopOnDestroy = this.StopOnDestroy;
		_duration = baseTween.duration;
	}
		
	protected IIAni _baseTween;
		
	public IIAni baseTween
	{
		get { return _baseTween; }
	}

    public override Action decoratorStopOnDestroy
    {
        set
        {
            _baseTween.decoratorStopOnDestroy = value;
        }
    }

    public override void Play()
	{
		if (!_isPlaying) {
			_baseTween.StartPlay();
			base.Play();
		}
	}
		
	public override void StartPlay()
	{
		base.StartPlay();
		_baseTween.StartPlay();
	}
		
	public override void Stop()
	{
		if (_isPlaying) {
			base.Stop();
			_baseTween.StartStop();
		}
	}
		
	public override void StartStop()
	{
		base.StartStop();
		_baseTween.StartStop();
	}

    public override void ResolveValues()
    {
        _baseTween.ResolveValues();
    }
		
	protected override void InternalUpdate( float time )
	{
		_baseTween.UpdateTween(time);
	}
}
