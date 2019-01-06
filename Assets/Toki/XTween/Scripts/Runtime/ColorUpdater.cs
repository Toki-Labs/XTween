using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class ColorUpdater<T> : AbstractUpdater
	{
		protected int _updateCount;
		protected T _target = default(T);
		protected string _propertyName;
		protected UpdaterColor _updaterColor = new UpdaterColor();
		protected Color _col;
		protected XColorHash _start;
		protected XColorHash _finish;
		protected Action<T,Color> _updater;
			

		public T Target
		{
			get { return _target; }
			set 
			{ 
				_target = value;
			}
		}

		public string PropertyName
		{
			set
			{
				this._propertyName = value;
			}
		}
			
		public override IClassicHandlable Start
		{
			set { _start = (XColorHash)value; }
		}
			
		public override IClassicHandlable Finish
		{
			set { _finish = (XColorHash)value; }
		}

		//source set
		public override void ResolveValues()
		{
			if (_resolvedValues) return;
			if (IsNullTarget() || _propertyName == null)
				throw new System.NullReferenceException("Tweener target or propertyName is Null at start point");

			Type type = typeof(T);
			PropertyInfo pInfo = type.GetProperty(_propertyName);
			Color col = (Color)pInfo.GetValue(_target, null);
			_updater = (Action<T, Color>)Delegate.CreateDelegate
			(
				typeof(Action<T, Color>),
				typeof(T).GetProperty(_propertyName).GetSetMethod()
			);

			if (_start.ContainRed)
			{
				col.r = _start.Red;
			}
			if (_start.ContainGreen)
			{
				col.g = _start.Green;
			}
			if (_start.ContainBlue)
			{
				col.b = _start.Blue;
			}
			if (_start.ContainAlpha)
			{
				col.a = _start.Alpha;
			}

			bool changeRed = false;
			bool changeGreen = false;
			bool changeBlue = false;
			bool changeAlpha = false;

			float red = col.r;
			float green = col.g;
			float blue = col.b;
			float alpha = col.a;

			if( _finish.ControlPointRed != null && !_finish.ContainRed ) _finish.Red = col.r;
			if( _finish.ControlPointGreen != null && !_finish.ContainGreen ) _finish.Green = col.g;
			if( _finish.ControlPointBlue != null && !_finish.ContainBlue ) _finish.Blue = col.b;
			if( _finish.ControlPointAlpha != null && !_finish.ContainAlpha ) _finish.Alpha = col.a;

			if (_finish.ContainRed)
			{
				if (red != _finish.Red || _finish.ControlPointRed != null || _finish.IsRelativeRed)
				{
					changeRed = true;
					red = _finish.IsRelativeRed ? red + _finish.Red : _finish.Red;
				}
			}
			if (_finish.ContainGreen)
			{
				if (green != _finish.Green || _finish.ControlPointGreen != null || _finish.IsRelativeGreen)
				{
					changeGreen = true;
					green = _finish.IsRelativeGreen ? green + _finish.Green : _finish.Green;
				}
			}
			if (_finish.ContainBlue)
			{
				if (blue != _finish.Blue || _finish.ControlPointBlue != null || _finish.IsRelativeBlue)
				{
					changeBlue = true;
					blue = _finish.IsRelativeBlue ? blue + _finish.Blue : _finish.Blue;
				}
			}
			if (_finish.ContainAlpha)
			{
				if (alpha != _finish.Alpha || _finish.ControlPointAlpha != null || _finish.IsRelativeAlpha)
				{
					changeAlpha = true;
					alpha = _finish.IsRelativeAlpha ? alpha + _finish.Alpha : _finish.Alpha;
				}
			}

			_col = col;
			Color sColor = new Color(col.r, col.b, col.g, col.a);
			Color dColor = new Color(red, green, blue, alpha);
			this._updaterColor.Initialize(sColor, dColor, 
				_finish.ControlPointRed, _finish.ControlPointGreen, _finish.ControlPointBlue, _finish.ControlPointAlpha, 
				changeRed, changeGreen, changeBlue, changeAlpha);
			this._resolvedValues = true;
		}

		protected override void UpdateObject()
		{
			if (IsNullTarget()) return;

			_updater(_target, this._updaterColor.Update(_invert, _factor, _col));
		}

		private bool IsNullTarget()
		{
			if(EqualityComparer<T>.Default.Equals(_target, default(T))) 
			{
				this._tweener.StopOnDestroy();
				return true;
			}
			return false;
		}

		public override void Release()
		{
			if( this._start != null ) this._start.PoolPush();
			if( this._finish != null ) this._finish.PoolPush();
			this.PoolPush();
		}

		public override void Dispose()
		{
			base.Dispose();
			this._updaterColor.Dispose();
			this._updateCount = 0;
			this._target = default(T);
			this._propertyName = null;
			this._col = default(Color);
			this._start = null;
			this._finish = null;
			this._updater = null;
		}
	}
}