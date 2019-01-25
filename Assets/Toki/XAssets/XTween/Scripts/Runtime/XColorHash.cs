using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Toki;
using Toki.Tween;

public class XColorHash : XEventHash
{
	private XColorHash _start;

	/*********************************** Color **********************************/
    public bool IsRelativeRed { get; set; }
    public bool IsRelativeGreen { get; set; }
    public bool IsRelativeBlue { get; set; }
    public bool IsRelativeAlpha { get; set; }
    private bool _containRed, _containGreen, _containBlue, _containAlpha;
	public bool ContainRed { get{ return this._containRed; } }
	public bool ContainGreen { get{ return this._containGreen; } }
	public bool ContainBlue { get{ return this._containBlue; } }
	public bool ContainAlpha { get{ return this._containAlpha; } }
	private float _red, _green, _blue, _alpha;
	private float[] _controlRed, _controlGreen, _controlBlue, _controlAlpha;
	public float Red
	{
		get { return _red; }
		set 
		{ 
			_containRed = true;
			_red = value; 
		}
	}
	public float[] ControlPointRed { get{ return _controlRed; } set{ _controlRed = value; } }
	public float Green
	{
		get { return _green; }
		set 
		{ 
			_containGreen = true;
			_green = value; 
		}
	}
	public float[] ControlPointGreen { get{ return _controlGreen; } set{ _controlGreen = value; } }
	public float Blue
	{
		get { return _blue; }
		set 
		{ 
			_containBlue = true;
			_blue = value; 
		}
	}
	public float[] ControlPointBlue { get{ return _controlBlue; } set{ _controlBlue = value; } }
	public float Alpha
	{
		get { return _alpha; }
		set 
		{ 
			_containAlpha = true;
			_alpha = value; 
		}
	}
	public float[] ControlPointAlpha { get{ return _controlAlpha; } set{ _controlAlpha = value; } }
	public static XColorHash Color( float? r, float? g, float? b, float? a, bool isRelative = false )
    {
		XColorHash hash = XColorHash.New;
		if( r != null ) hash.AddRed((float)r, isRelative);
		if( g != null ) hash.AddGreen((float)g, isRelative);
		if( b != null ) hash.AddBlue((float)b, isRelative);
		if( a != null ) hash.AddAlpha((float)a, isRelative);
        return hash;
    }
    public static XColorHash Color( Color color, bool isRelative = false )
    {
		XColorHash hash = XColorHash.New;
        hash.AddColor( color, isRelative );
        return hash;
    }
	public static XColorHash Color( Color start, Color end, bool isRelative = false )
    {
		XColorHash hash = XColorHash.New;
		hash.AddColor(start, end, isRelative);
        return hash;
    }

	/*********************************** Create Instance **********************************/
    public static XColorHash New
    {
        get
        {
            return Pool<XColorHash>.Pop();
        }
    }

	
	/*********************************** Add Methods **********************************/
    public XColorHash AddRed( float end, bool isRelative = false )
    {
        this.Red = end;
        this.IsRelativeRed = isRelative;
        return this;
    }
	public XColorHash AddRed( float start, float end, bool isRelative = false )
	{
		XColorHash hash = this.GetStart();
		hash.Red = start;
		this._start = hash;
		return AddRed( end, isRelative );
	}
	public XColorHash AddControlPointRed( params float[] values )
	{
		this._controlRed = values;
		return this;
	}
    public XColorHash AddGreen( float end, bool isRelative = false )
    {
        this.Green = end;
        this.IsRelativeGreen = isRelative;
        return this;
    }
	public XColorHash AddGreen( float start, float end, bool isRelative = false )
	{
		XColorHash hash = this.GetStart();
		hash.Green = start;
		this._start = hash;
		return AddGreen( end, isRelative );
	}
	public XColorHash AddColor( Color end, bool isRelative = false )
	{
		XColorHash hash = this.GetStart();
		this.AddRed(end.r, isRelative);
		this.AddGreen(end.g, isRelative);
		this.AddBlue(end.b, isRelative);
		this.AddAlpha(end.a, isRelative);
		return this;
	}
	public XColorHash AddColor( Color start, Color end, bool isRelative = false )
	{
		XColorHash hash = this.GetStart();
		this.AddRed(start.r, end.r, isRelative);
		this.AddGreen(start.g, end.g, isRelative);
		this.AddBlue(start.b, end.b, isRelative);
		this.AddAlpha(start.a, end.a, isRelative);
		return this;
	}
	public XColorHash AddControlPointGreen( params float[] values )
	{
		this._controlGreen = values;
		return this;
	}
    public XColorHash AddBlue( float end, bool isRelative = false )
    {
        this.Blue = end;
        this.IsRelativeBlue = isRelative;
        return this;
    }
	public XColorHash AddBlue( float start, float end, bool isRelative = false )
	{
		XColorHash hash = this.GetStart();
		hash.Blue = start;
		this._start = hash;
		return AddBlue( end, isRelative );
	}
	public XColorHash AddControlPointBlue( params float[] values )
	{
		this._controlBlue = values;
		return this;
	}
    public XColorHash AddAlpha( float end, bool isRelative = false )
    {
        this.Alpha = end;
        this.IsRelativeAlpha = isRelative;
        return this;
    }
	public XColorHash AddAlpha( float start, float end, bool isRelative = false )
	{
		XColorHash hash = this.GetStart();
		hash.Alpha = start;
		this._start = hash;
		return AddAlpha( end, isRelative );
	}
	public XColorHash AddControlPointAlpha( params float[] values )
	{
		this._controlAlpha = values;
		return this;
	}
    public XColorHash AddOnPlay( IExecutable value )
	{
		this.OnPlay = value;
		return this;
	}
	public XColorHash AddOnUpdate( IExecutable value )
	{
		this.OnUpdate = value;
		return this;
	}
	public XColorHash AddOnStop( IExecutable value )
	{
		this.OnStop = value;
		return this;
	}
	public XColorHash AddOnComplete( IExecutable value )
	{
		this.OnComplete = value;
		return this;
	}

	public XColorHash GetStart()
	{
		if( this._start == null ) 
			this._start = Pool<XColorHash>.Pop();
			
		return this._start;
	}

	public override void Dispose()
	{
		base.Dispose();
		this.IsRelativeRed = this.IsRelativeGreen = this.IsRelativeBlue = this.IsRelativeAlpha = false;
		this._containRed = this._containGreen = this._containBlue = this._containAlpha = false;
		this._red = this._green = this._blue = this._alpha = 0f;
		this._controlRed = this._controlGreen = this._controlBlue = this._controlAlpha = null;
		
	}
}