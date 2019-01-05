using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class ScaledTween : TweenDecorator
	{
		public override void Initialize( IIXTween baseTween, float scale )
		{
			base.Initialize(baseTween, 0);
			_duration = baseTween.Duration * scale;
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
			ScaledTween tween = new ScaledTween();
			tween.Initialize(_baseTween.Clone() as IIXTween, _scale);
			return tween;
		}
	}
}
