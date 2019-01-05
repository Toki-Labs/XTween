using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class ObjectTween : AbstractTween, IIXTweenObject
	{
		public ObjectTween( ITimer ticker ) : base( ticker, 0 )
		{
				
		}
			
		protected IEasing _easing;
		protected IUpdating _updater;
			
		public float Time
		{
			get { return _duration; }
			set { _duration = value; }
		}
			
		public IEasing Easing
		{
			get { return _easing; }
			set { _easing = value; }
		}
			
		public IUpdating Updater
		{
			get { return _updater; }
			set 
			{ 
				_updater = value;
					
				if( _updater != null )
				{
					_updater.StopOnDestroyHandler = this.StopOnDestroy;
				}
			}
		}

		public override void ResolveValues()
		{
			this._updater.ResolveValues();
		}
			
		protected override void InternalUpdate( float time )
		{
			float factor = 0f;

			if (time > 0f) 
			{
				if (time < _duration) 
				{
					factor = _easing.Calculate(time, 0.0f, 1.0f, _duration);
				}
				else 
				{
					factor = 1.0f;
				}
			}
			_updater.Updating(factor);
		}
			
		protected override AbstractTween NewInstance()
		{
			return new ObjectTween(_ticker);
		}
	}
}