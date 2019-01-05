using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class ReversedTween : TweenDecorator
	{
		public ReversedTween( IIXTween baseTween, float position ) : base( baseTween, position )
		{
			_duration = baseTween.Duration;
		}
		
		protected override void InternalUpdate( float time )
		{
			_baseTween.UpdateTween(_duration - time);
		}

		protected override AbstractTween NewInstance()
		{
			return new ReversedTween(_baseTween.Clone() as IIXTween, 0);
		}
	}
}