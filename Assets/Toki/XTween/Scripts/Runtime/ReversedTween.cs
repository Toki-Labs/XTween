using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class ReversedTween : TweenDecorator
	{
		public override void Initialize( IIXTween baseTween, float position )
		{
			base.Initialize( baseTween, position );
			_duration = baseTween.Duration;
		}
		
		protected override void InternalUpdate( float time )
		{
			_baseTween.UpdateTween(_duration - time);
		}

		protected override AbstractTween NewInstance()
		{
			ReversedTween tween = new ReversedTween();
			tween.Initialize(_baseTween.Clone() as IIXTween, 0);
			return tween;
		}
	}
}