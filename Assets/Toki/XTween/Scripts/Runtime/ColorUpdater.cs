using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

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
		if( _target == null || _propertyName == null )
		{
			if( this._stopOnDestroyHandler != null )
			{
				this._stopOnDestroyHandler.Invoke();
			}
			return;
		}

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
		if (_finish.ContainRed)
		{
			if (red != _finish.Red || _finish.IsRelativeRed)
			{
				red = _finish.IsRelativeRed ? red + _finish.Red : _finish.Red;
				this._updateList.Add(UpdateRed);
			}
		}
		if (_finish.ContainGreen)
		{
			if (green != _finish.Green || _finish.IsRelativeGreen)
			{
				green = _finish.IsRelativeGreen ? green + _finish.Green : _finish.Green;
				this._updateList.Add(UpdateGreen);
			}
		}
		if (_finish.ContainBlue)
		{
			if (blue != _finish.Blue || _finish.IsRelativeBlue)
			{
				blue = _finish.IsRelativeBlue ? blue + _finish.Blue : _finish.Blue;
				this._updateList.Add(UpdateBlue);
			}
		}
		if (_finish.ContainAlpha)
		{
			if (alpha != _finish.Alpha || _finish.IsRelativeAlpha)
			{
				alpha = _finish.IsRelativeAlpha ? alpha + _finish.Alpha : _finish.Alpha;
				this._updateList.Add(UpdateAlpha);
			}
		}

		this._sColor = new Color(_col.r, _col.b, _col.g, _col.a);
		this._dColor = new Color(red, green, blue, alpha);
		this._updateList.Add(Updator);
        this._updateCount = this._updateList.Count;
	}

	protected override void UpdateObject()
	{
        if (this._target == null)
        {
            this._stopOnDestroyHandler.Invoke();
            this.Dispose();
        }
        else
        {
            for (int i = 0; i < this._updateCount; ++i)
            {
                this._updateList[i].Invoke();
            }
        }
	}

    public void Dispose()
    {
		// Debug.Log ("Dispose");
        this._stopOnDestroyHandler = null;
        this._updateList.Clear();
        this._updateList = null;
    }
    protected virtual void UpdateRed()
	{
		_col.r = _sColor.r * _invert + _dColor.r * _factor;
	}
    protected virtual void UpdateGreen()
	{
		_col.g = _sColor.g * _invert + _dColor.g * _factor;
	}
    protected virtual void UpdateBlue()
	{
		_col.b = _sColor.b * _invert + _dColor.b * _factor;
	}
    protected virtual void UpdateAlpha()
	{
		_col.a = _sColor.a * _invert + _dColor.a * _factor;
	}

	protected void Updator()
	{
		_updator(_target, _col);
	}
}