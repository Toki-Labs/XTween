using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Toki;
using Toki.Tween;

namespace Toki.Tween
{
    public class XObjectSet : IDisposable
    {
        public Action<float,float> updater;
        public XObjectValues value;

        public void Dispose()
        {
            this.updater = null;
            this.value = default(XObjectValues);
        }
    }

    public struct XObjectValues
    {
        private bool _containStart;
        public bool ContainStart { get{return _containStart;} }
        public float[] controlPoint;
        public float start;
        public float current;
        public float end;

        public XObjectValues( float end )
        {
            this._containStart = false;
            this.start = this.current = 0f;
            this.end = end;
            this.controlPoint = null;
        }
        public XObjectValues( float start, float end )
        {
            this._containStart = true;
            this.start = start;
            this.current = start;
            this.end = end;
            this.controlPoint = null;
        }
    }
}

public class XObjectHash : XEventHash
{
    private Dictionary<string, XObjectSet> _objectSet = new Dictionary<string, XObjectSet>();
    public Dictionary<string, XObjectSet> ObjectSet { get{return this._objectSet;} }
    
    public XObjectHash Add( string key, float start, float end )
    {
        return AddValue(key, new XObjectValues( start, end ));
    }

    public XObjectHash AddControlPointWithStartEnd( string key, float start, float end, params float[] values )
    {
        AddValue(key, new XObjectValues( start, end ));
        AddControlPoint(key, values);
        return this;
    }

    public XObjectHash Add( string key, float end )
    {
        return AddValue(key, new XObjectValues( end ));
    }

    public XObjectHash AddControlPointWithEnd( string key, float end, params float[] values )
    {
        AddValue(key, new XObjectValues( end ));
        AddControlPoint(key, values);
        return this;
    }

    public XObjectHash AddControlPoint( string key, params float[] values )
    {
        if( values == null ) return this;
        if( !this._objectSet.ContainsKey(key) ) Add(key, 0f);
        XObjectSet objSet = this._objectSet[key];
        XObjectValues value = objSet.value;
        value.controlPoint = values;
        objSet.value = value;
        return this;
    }

    private XObjectHash AddValue( string key, XObjectValues value )
    {
        XObjectSet set = Pool<XObjectSet>.Pop();
        set.value = value;
        this._objectSet.Add(key, set);
        return this;
    }

    public float Now( string key )
    {
        return _objectSet[key].value.current;
    }

    public static XObjectHash New
    {
        get
        {
            return Pool<XObjectHash>.Pop();
        }
    }

    public override void Dispose()
    {
        base.Dispose();
        foreach ( var item in this._objectSet )
            item.Value.PoolPush();
        
        this._objectSet.Clear();
    }
}