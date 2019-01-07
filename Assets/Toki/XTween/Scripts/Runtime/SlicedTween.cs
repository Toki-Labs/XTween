using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class SlicedTween : TweenDecorator
	{
		public void Initialize( IIXTween baseTween, float begin, float end )
		{
			base.Initialize(baseTween, 0);
			_duration = end - begin;
			_begin = begin;
			_end = end;
		}
			
		private float _begin;
		private float _end;
			
		public float begin
		{
			get { return _begin; }
		}
			
		public float end
		{
			get { return _end; }
		}
			
		protected override void InternalUpdate( float time )
		{
			if (time > 0) {
				if (time < _duration) {
					_baseTween.UpdateTween(time + _begin);
				}
				else {
					_baseTween.UpdateTween(_end);
				}
			}
			else {
				_baseTween.UpdateTween(_begin);
			}
		}

		protected override void InternalRelease()
		{
			if( this._autoDispose ) this.PoolPush();
		}

		public override void Dispose()
		{
			base.Dispose();
			this._begin = 0f;
			this._end = 0f;
		}
			
		protected override AbstractTween NewInstance()
		{
			SlicedTween tween = new SlicedTween();
			tween.Initialize(_baseTween.Clone() as IIXTween, _begin, _end);
			return tween;
		}
	}
}