using UnityEngine;
using System;
using System.Collections;

public abstract class AbstractUpdater : IUpdating
{
	protected float _invert;
    protected float _factor = 0f;
	protected Action _stopOnDestroyHandler;
	public virtual Action StopOnDestroyHandler
	{
		set { _stopOnDestroyHandler = value; }
	}
	public abstract IClassicHandlable Start {set;}
	public abstract IClassicHandlable Finish {set;}
    public virtual void Updating( float factor )
	{
		_invert = 1.0f - factor;
        _factor = factor;
		UpdateObject();
	}
		
	public abstract void ResolveValues();
	protected abstract void UpdateObject();
}

