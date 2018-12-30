using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public struct XColorHash : IClassicHandlable
{
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
	private IClassicHandlable _start;

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
    public XColorHash Color( Color color, bool isRelative = false )
    {
        this.AddRed( color.r, isRelative );
		this.AddGreen( color.g, isRelative );
		this.AddBlue( color.b, isRelative );
        return this;
    }
	public XColorHash Color( Color start, Color end, bool isRelative = false )
    {
		this._start = this.GetStart().Color(start, isRelative);
        this.AddRed( end.r, isRelative );
		this.AddGreen( end.g, isRelative );
		this.AddBlue( end.b, isRelative );
        return this;
    }

	/*********************************** Create Instance **********************************/
    public static XColorHash New
    {
        get
        {
            return new XColorHash();
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
		if( this._start == null ) this._start = new XColorHash();
		return (XColorHash)this._start;
	}
}