using System;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class TweenDecorator : AbstractTween
	{
		public virtual void Initialize( IIXTween baseTween, float position )
		{
			base.Initialize(baseTween.Ticker, position);
			_baseTween = baseTween;
			_baseTween.DecoratorStopOnDestroy = this.StopOnDestroy;
			_duration = baseTween.Duration;
		}
			
		protected IIXTween _baseTween;
			
		public IIXTween baseTween
		{
			get { return _baseTween; }
		}

		public override Action DecoratorStopOnDestroy
		{
			set
			{
				_baseTween.DecoratorStopOnDestroy = value;
			}
		}

		public override IXTween Play()
		{
			if (!_isPlaying) 
			{
				_baseTween.StartPlay();
				base.Play();
			}
			return this;
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
}
