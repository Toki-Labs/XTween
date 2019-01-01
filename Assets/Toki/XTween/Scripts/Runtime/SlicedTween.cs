using UnityEngine;
using System.Collections;

namespace Toki.Tween
{
	public class SlicedTween : TweenDecorator
	{
		public SlicedTween( IIAni baseTween, float begin, float end ) : base(baseTween, 0)
		{
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
			
		protected override AbstractTween NewInstance()
		{
			return new SlicedTween(_baseTween.Clone() as IIAni, _begin, _end);
		}
	}
}