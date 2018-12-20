using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ObjectUpdater : IUpdating
{
	protected XObjectHash _source;
	protected float invert;
	protected Action _stopHandler;
	protected Action<XObjectHash> _updateHandler;
		
	public string targetName
	{
		get;
		set;
	}
		
	public GameObject target
	{
		get;
		set;
	}
		
	public Action stopHandler
	{
		set { _stopHandler = value; }
	}
		
	public IClassicHandlable start
	{
		set
		{
            
		}
	}
		
	public IClassicHandlable finish
	{
		set
		{
            this._source = (XObjectHash)value;
		}
	}
		
	public Action<XObjectHash> updateHandler
	{
		set
		{
			this._updateHandler = value;
		}
	}

    public void ResolveValues()
    {
        this._source.ResolveValues();
    }
		
	public void Updating( float factor )
	{
        invert = 1.0f - factor;

        this._updateHandler( this._source.Update( invert, factor ) );
	}
		
	protected void CopyFrom( ObjectUpdater source )
	{
        source._source = this._source;
        source._updateHandler = this._updateHandler;
	}
		
	public IUpdating Clone()
	{
		ObjectUpdater instance = new ObjectUpdater();
		instance.CopyFrom(this);
		return instance;
	}
}