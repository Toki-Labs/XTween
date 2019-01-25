using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Toki.Tween;

public class XEventHash : IClassicHandlable
{
	protected IExecutable _onPlay;
    protected IExecutable _onStop;
    protected IExecutable _onUpdate;
    protected IExecutable _onComplete;

    public IExecutable OnPlay
    {
        get
        {
            return this._onPlay;
        }
        set
        {
            this._onPlay = value;
        }
    }

    public IExecutable OnStop
    {
        get
        {
            return this._onStop;
        }
        set
        {
            this._onStop = value;
        }
    }

    public IExecutable OnUpdate
    {
        get
        {
            return this._onUpdate;
        }
        set
        {
            this._onUpdate = value;
        }
    }

    public IExecutable OnComplete
    {
        get
        {
            return this._onComplete;
        }
        set
        {
            this._onComplete = value;
        }
    }

    public void CopyFrom( IClassicHandlable source )
    {
        this._onPlay = source.OnPlay;
        this._onStop = source.OnStop;
        this._onUpdate = source.OnUpdate;
        this._onComplete = source.OnComplete;
    }

	public virtual void Dispose()
	{
        this._onPlay = null;
        this._onStop = null;
        this._onUpdate = null;
        this._onComplete = null;
	}
}