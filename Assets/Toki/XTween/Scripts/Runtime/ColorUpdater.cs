using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class ColorUpdater<T> : AbstractUpdater, IUpdating
	{
		protected int _updateCount;
		protected T _target = default(T);
		protected string _propertyName;
		protected Color _sColor;
		protected Color _dColor;
		protected Color _col;
		protected XColorHash _start;
		protected XColorHash _finish;
		protected Action<T,Color> _updator;
		protected List<Action> _updateList;
			

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
			if (IsNullTarget() || _propertyName == null) return;

			Type type = typeof(T);
			PropertyInfo pInfo = type.GetProperty(_propertyName);
			_col = (Color)pInfo.GetValue(_target, null);
			_updator = (Action<T, Color>)Delegate.CreateDelegate
			(
				typeof(Action<T, Color>),
				typeof(T).GetProperty(_propertyName).GetSetMethod()
			);

			this._updateList = new List<Action>();
			
			if (_start.ContainRed)
			{
				_col.r = _start.Red;
			}
			if (_start.ContainGreen)
			{
				_col.g = _start.Green;
			}
			if (_start.ContainBlue)
			{
				_col.b = _start.Blue;
			}
			if (_start.ContainAlpha)
			{
				_col.a = _start.Alpha;
			}

			float red = _col.r;
			float green = _col.g;
			float blue = _col.b;
			float alpha = _col.a;

			if( _finish.ControlPointRed != null && !_finish.ContainRed ) _finish.Red = _col.r;
			if( _finish.ControlPointGreen != null && !_finish.ContainGreen ) _finish.Green = _col.g;
			if( _finish.ControlPointBlue != null && !_finish.ContainBlue ) _finish.Blue = _col.b;
			if( _finish.ControlPointAlpha != null && !_finish.ContainAlpha ) _finish.Alpha = _col.a;

			if (_finish.ContainRed)
			{
				if (red != _finish.Red || _finish.ControlPointRed != null || _finish.IsRelativeRed)
				{
					red = _finish.IsRelativeRed ? red + _finish.Red : _finish.Red;
					this._updateList.Add(GetUpdateRed());
				}
			}
			if (_finish.ContainGreen)
			{
				if (green != _finish.Green || _finish.ControlPointGreen != null || _finish.IsRelativeGreen)
				{
					green = _finish.IsRelativeGreen ? green + _finish.Green : _finish.Green;
					this._updateList.Add(GetUpdateGreen());
				}
			}
			if (_finish.ContainBlue)
			{
				if (blue != _finish.Blue || _finish.ControlPointBlue != null || _finish.IsRelativeBlue)
				{
					blue = _finish.IsRelativeBlue ? blue + _finish.Blue : _finish.Blue;
					this._updateList.Add(GetUpdateBlue());
				}
			}
			if (_finish.ContainAlpha)
			{
				if (alpha != _finish.Alpha || _finish.ControlPointAlpha != null || _finish.IsRelativeAlpha)
				{
					alpha = _finish.IsRelativeAlpha ? alpha + _finish.Alpha : _finish.Alpha;
					this._updateList.Add(GetUpdateAlpha());
				}
			}

			this._sColor = new Color(_col.r, _col.b, _col.g, _col.a);
			this._dColor = new Color(red, green, blue, alpha);
			this._updateList.Add(Updator);
			this._updateCount = this._updateList.Count;
		}

		protected override void UpdateObject()
		{
			if (IsNullTarget()) return;

			for (int i = 0; i < this._updateCount; ++i)
			{
				this._updateList[i].Invoke();
			}
		}

		private bool IsNullTarget()
		{
			if(EqualityComparer<T>.Default.Equals(_target, default(T))) 
			{
				if( _stopOnDestroyHandler != null )
					_stopOnDestroyHandler.Invoke();
				this._stopOnDestroyHandler = null;
				this._updateList.Clear();
				this._updateList = null;
				return true;
			}
			return false;
		}
		
		protected virtual Action GetUpdateRed() { return _finish.ControlPointRed == null ? (Action)this.UpdateRed : this.UpdateBezierRed; }
		protected virtual Action GetUpdateGreen() { return _finish.ControlPointGreen == null ? (Action)this.UpdateGreen : this.UpdateBezierGreen; }
		protected virtual Action GetUpdateBlue() { return _finish.ControlPointBlue == null ? (Action)this.UpdateBlue : this.UpdateBezierBlue; }
		protected virtual Action GetUpdateAlpha() { return _finish.ControlPointAlpha == null ? (Action)this.UpdateAlpha : this.UpdateBezierAlpha; }
		protected virtual void UpdateRed()
		{
			_col.r = _sColor.r * _invert + _dColor.r * _factor;
		}
		protected virtual void UpdateBezierRed()
		{
			_col.r = base.Calcurate( _finish.ControlPointRed, _sColor.r, _dColor.r );
		}
		protected virtual void UpdateGreen()
		{
			_col.g = _sColor.g * _invert + _dColor.g * _factor;
		}
		protected virtual void UpdateBezierGreen()
		{
			_col.g = base.Calcurate( _finish.ControlPointGreen, _sColor.g, _dColor.g );
		}
		protected virtual void UpdateBlue()
		{
			_col.b = _sColor.b * _invert + _dColor.b * _factor;
		}
		protected virtual void UpdateBezierBlue()
		{
			_col.b = base.Calcurate( _finish.ControlPointBlue, _sColor.b, _dColor.b );
		}
		protected virtual void UpdateAlpha()
		{
			_col.a = _sColor.a * _invert + _dColor.a * _factor;
		}
		protected virtual void UpdateBezierAlpha()
		{
			_col.a = base.Calcurate( _finish.ControlPointAlpha, _sColor.a, _dColor.a );
		}

		protected void Updator()
		{
			_updator(_target, _col);
		}
	}
}