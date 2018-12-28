using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public struct XObjectHash : IClassicHandlable
{
    private Dictionary<string, XObjectValues> _values;
    private string[] _keys;
    private int _keyLength;
    
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

public struct XObjectHash<T> : IClassicHandlable
{
    private class XObjectSet
    {
        public Action<float,float> updator;
        public XObjectValues value;
    }
    private Dictionary<string, XObjectSet> _values;
    private string[] _keys;
    private int _keyLength;
    
    public XObjectHash<T> Add( T target, string propertyName, float end )
    {
        Type type = typeof(T);
        PropertyInfo pInfo = type.GetProperty(propertyName);
        float start = (float)pInfo.GetValue(target, null);
        XObjectValues value = new XObjectValues( start, end );
        if( _values == null )
        {
            this._values = new Dictionary<string, XObjectSet>();
        }
        Dictionary<string, XObjectSet> dic = this._values;
        XObjectSet objSet = new XObjectSet();
        objSet.value = value;
        Action<T,float> setter = (Action<T, float>)Delegate.CreateDelegate(
            typeof(Action<T, float>),
            typeof(T).GetProperty(propertyName).GetSetMethod()
    	);
        objSet.updator = delegate( float invert, float factor )
        {
            XObjectValues valueUpdate = dic[propertyName].value;
            float current =  valueUpdate.start * invert + valueUpdate.end * factor;
            valueUpdate.current = current;
            dic[propertyName].value = valueUpdate;
            setter(target, current);
        };

        this._values.Add( propertyName, objSet );
        return this;
    }

    public XObjectHash<T> Add( T target, string propertyName, int end )
    {
        Type type = typeof(T);
        PropertyInfo pInfo = type.GetProperty(propertyName);
        float start = (int)pInfo.GetValue(target, null);
        XObjectValues value = new XObjectValues( start, Convert.ToSingle(end) );
        if( _values == null )
        {
            this._values = new Dictionary<string, XObjectSet>();
        }
        Dictionary<string, XObjectSet> dic = this._values;
        XObjectSet objSet = new XObjectSet();
        objSet.value = value;
        Action<T,int> setter = (Action<T, int>)Delegate.CreateDelegate(
            typeof(Action<T, int>),
            typeof(T).GetProperty(propertyName).GetSetMethod()
    	);
        objSet.updator = delegate( float invert, float factor )
        {
            XObjectValues valueUpdate = dic[propertyName].value;
            int current = Mathf.RoundToInt(valueUpdate.start * invert + valueUpdate.end * factor);
            if( current != (int)valueUpdate.current )
            {
                valueUpdate.current = current;
                dic[propertyName].value = valueUpdate;
                setter(target, current);
            }
        };

        this._values.Add( propertyName, objSet );
        return this;
    }

    public void ResolveValues()
    {
        this._keys = XTween.GetArrayFromCollection<string>(this._values.Keys);
        this._keyLength = this._keys.Length;
    }

    public XObjectHash<T> Update( float invert, float factor )
    {
        foreach ( var item in this._values )
        {
            item.Value.updator( invert, factor );
        }
        return this;
    }

    public float Now( string key )
    {
        return 0f;
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

    public static XObjectHash<T> New
    {
        get
        {
            return new XObjectHash<T>();
        }
    }
}

internal struct XObjectValues
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

public class TestMono : MonoBehaviour
{

}