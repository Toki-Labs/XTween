using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class GetSetUpdater : AbstractUpdater
	{
		protected XObjectHash _source;
		protected Action _StopOnDestroyHandler;
		protected Action _updater;
		protected Action<float> _setter;
		protected float[] _controlPoints;
		protected float _startValue;
		protected float _endValue;
			
		public override Action StopOnDestroyHandler
		{
			set { _StopOnDestroyHandler = value; }
		}

		public float StartValue { set{this._startValue = value;} }
		public float EndValue { set{this._endValue = value;} }
		public float[] ControlPoints { set{this._controlPoints = value;} }
		public Action<float> Setter { set{this._setter = value;} }
			
		public override IClassicHandlable Start { set{}	}
		public override IClassicHandlable Finish { set{} }
		
		public override void ResolveValues()
		{
			if( _resolvedValues ) return;
			_updater = _controlPoints == null ? (Action)UpdateValue : UpdateBezierValue;
			this._resolvedValues = true;
		}
			
		protected override void UpdateObject()
		{
			_updater.Invoke();
		}

		protected void UpdateValue()
		{
			_setter( _startValue * _invert + _endValue * _factor );
		}

		protected void UpdateBezierValue()
		{
			_setter( Calcurate(_controlPoints, _startValue, _endValue) );
		}
	}
}