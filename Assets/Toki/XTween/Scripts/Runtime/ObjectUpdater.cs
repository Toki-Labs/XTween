using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class ObjectUpdater : AbstractUpdater
{
	protected Dictionary<string, XObjectSet> _valueDic;
	protected string[] _keys;
	protected int _keyLength;
	protected XObjectHash _source;
	protected Action<XObjectHash> _updateHandler;
	protected Action _StopOnDestroyHandler;
		
	public override Action StopOnDestroyHandler
	{
		set { _StopOnDestroyHandler = value; }
	}
		
	public override IClassicHandlable Start { set{}	}
	public override IClassicHandlable Finish
	{
		set
		{
            this._source = (XObjectHash)value;
		}
	}
		
	public Action<XObjectHash> UpdateHandler
	{
		set
		{
			this._updateHandler = value;
		}
	}

	protected void ComposeDic()
	{
		this._valueDic = _source.ObjectSet;
		this._keys = XTween.GetArrayFromCollection<string>(this._valueDic.Keys);
		this._keyLength = this._keys.Length;
	}

    public override void ResolveValues()
    {
		this.ComposeDic();

		foreach ( var item in this._valueDic )
		{
			XObjectValues objValue = item.Value.value;
			item.Value.updator = objValue.controlPoint == null ? 
			(Action<float,float>)delegate( float invert, float factor )
			{
				objValue.current = objValue.start * invert + objValue.end * factor;
				item.Value.value = objValue;
			} :
			delegate( float invert, float factor )
			{
				objValue.current = Calcurate(objValue.controlPoint, objValue.start, objValue.end);
				item.Value.value = objValue;
			};
			item.Value.value = objValue;
		}
    }
		
	protected override void UpdateObject()
	{
		for (int i = 0; i < this._keyLength; ++i)
		{
			this._valueDic[this._keys[i]].updator(_invert, _factor);
		}
		if( this._updateHandler != null ) this._updateHandler(_source);
	}
}

public class ObjectUpdater<T> : ObjectUpdater
{
	protected T _target;
	public T Target
	{
		get { return _target; }
		set
		{
			_target = value;
		}
	}
    public override void ResolveValues()
    {
		if( _target == null )
		{
			if( this._stopOnDestroyHandler != null )
			{
				this._stopOnDestroyHandler.Invoke();
			}
			return;
		}
		base.ComposeDic();

        Type type = typeof(T);
		foreach ( var item in this._valueDic )
		{
			XObjectValues objValue = item.Value.value;
			Action<T,float> setter = (Action<T, float>)Delegate.CreateDelegate(
				typeof(Action<T, float>),
				typeof(T).GetProperty(item.Key).GetSetMethod()
			);
        	PropertyInfo pInfo = type.GetProperty(item.Key);
			if( objValue.ContainStart )
			{
				setter(_target, objValue.start);
			}
			else
			{
	        	objValue.start = (float)pInfo.GetValue(_target, null);
			}
			item.Value.updator = objValue.controlPoint == null ? 
			(Action<float,float>)delegate( float invert, float factor )
			{
				objValue.current = objValue.start * invert + objValue.end * factor;
				item.Value.value = objValue;
				setter(_target, objValue.current);
			} :
			delegate( float invert, float factor )
			{
				objValue.current = Calcurate(objValue.controlPoint, objValue.start, objValue.end);
				item.Value.value = objValue;
				setter(_target, objValue.current);
			};
		}
		
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
			base.UpdateObject();
        }
	}

	public void Dispose()
    {
        this._stopOnDestroyHandler = null;
        this._valueDic.Clear();
        this._target = default(T);
    }
}