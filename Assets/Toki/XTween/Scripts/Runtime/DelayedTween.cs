using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class DelayedTween : TweenDecorator
	{
		public void Initialize( IIXTween baseTween, float preDelay, float postDelay )
		{
			base.Initialize(baseTween, 0);
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
			DelayedTween tween = new DelayedTween();
			tween.Initialize(_baseTween.Clone() as IIXTween, _preDelay, _postDelay);
			return tween;
		}
	}
}
