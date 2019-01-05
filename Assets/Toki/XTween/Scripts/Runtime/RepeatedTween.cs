using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class RepeatedTween : TweenDecorator
	{
		public void Initialize( IIXTween baseTween, int repeatCount )
		{
			base.Initialize( baseTween, 0f );
			_baseDuration = baseTween.Duration;
			_repeatCount = repeatCount;
			_duration = _baseDuration * repeatCount;
		}
			
		private float _baseDuration;
		private int _repeatCount;
			
		public int repeatCount
		{
			get { return _repeatCount; }
		}
			
		protected override void InternalUpdate( float time )
		{
			if (time >= 0) {
				float reduce = ( time < _duration ) ? _baseDuration * (int)(time / _baseDuration) : _duration - _baseDuration;
				time -= reduce;
			}
				
			_baseTween.UpdateTween(time);
		}
			
		protected override AbstractTween NewInstance()
		{
			RepeatedTween tween = new RepeatedTween();
			tween.Initialize(_baseTween.Clone() as IIXTween, repeatCount);
			return tween;
		}
	}
}