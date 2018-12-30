using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class XObjectSet
{
    public Action<float,float> updator;
    public XObjectValues value;
}

public struct XObjectValues
{
    private bool _containStart;
    public bool ContainStart { get{return _containStart;} }
    public float start;
    public float current;
    public float end;

    public XObjectValues( float end )
    {
        this._containStart = false;
        this.start = this.current = 0f;
        this.end = end;
    }
    public XObjectValues( float start, float end )
    {
        this._containStart = true;
        this.start = start;
        this.current = start;
        this.end = end;
    }
}

public struct XObjectHash : IClassicHandlable
{
    private Dictionary<string, XObjectSet> _objectSet;
    public Dictionary<string, XObjectSet> ObjectSet { get{return this._objectSet;} }
    
    public XObjectHash Add( string key, float start, float end )
    {
        return AddValue(key, new XObjectValues( start, end ));
    }

    public XObjectHash Add( string key, float end )
    {
        return AddValue(key, new XObjectValues( end ));
    }

    private XObjectHash AddValue( string key, XObjectValues value )
    {
        if( this._objectSet == null ) this._objectSet = new Dictionary<string, XObjectSet>();
        XObjectSet set = new XObjectSet();
        set.value = value;
        this._objectSet.Add(key, set);
        return this;
    }

    public float Now( string key )
    {
        return _objectSet[key].value.current;
    }

    private IExecutable _onPlay;
    private IExecutable _onStop;
    private IExecutable _onUpdate;
    private IExecutable _onComplete;
    
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

    public static XObjectHash New
    {
        get
        {
            return new XObjectHash();
        }
    }
}