using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct XObjectHash : IClassicHandlable
{
    private Dictionary<string, XObjectValues> _values;
    private string[] _keys;
    private int _keyLength;
    
    private struct XObjectValues
    {
        public float start;
        public float current;
        public float end;

        public XObjectValues( float start, float end )
        {
            this.start = start;
            this.current = start;
            this.end = end;
        }
    }

    public XObjectHash Add( string key, float start, float end )
    {
        XObjectValues value = new XObjectValues( start, end );
        if( _values == null )
        {
            this._values = new Dictionary<string, XObjectValues>();
        }
        this._values.Add( key, value );
        return this;
    }

    public void ResolveValues()
    {
        this._keys = XTween.GetArrayFromCollection<string>(this._values.Keys);
        this._keyLength = this._keys.Length;
    }

    public XObjectHash Update( float invert, float factor )
    {
        for ( int i = 0; i < this._keyLength; ++i )
        {
            XObjectValues value = this._values[this._keys[i]];
            value.current = value.start * invert + value.end * factor;
            this._values[this._keys[i]] = value;
        }
        return this;
    }

    public float Now( string key )
    {
        return this._values[key].current;
    }

    private IExecutable _onPlay;
    private IExecutable _onStop;
    private IExecutable _onUpdate;
    private IExecutable _onComplete;
    
    public IExecutable onPlay
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

    public IExecutable onStop
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

    public IExecutable onUpdate
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

    public IExecutable onComplete
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
        this._onPlay = source.onPlay;
        this._onStop = source.onStop;
        this._onUpdate = source.onUpdate;
        this._onComplete = source.onComplete;
    }

    public static XObjectHash New
    {
        get
        {
            return new XObjectHash();
        }
    }
}