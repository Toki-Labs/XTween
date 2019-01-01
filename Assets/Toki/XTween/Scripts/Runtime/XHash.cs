using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Toki.Tween;

namespace Toki.Tween
{
	public struct UIRect
	{
		public float left, right, top, bottom;
		public UIRect( float left, float top, float right, float bottom )
		{
			this.left = left;
			this.top = top;
			this.right = right;
			this.bottom = bottom;
		}

		public override string ToString()
		{
			return "(" + this.left.ToString() + ", " + this.top.ToString() + ", " + 
					this.right.ToString() + ", " + this.bottom.ToString() + ")";
		}
	}
}

public struct XHash : IClassicHandlable
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

	/*********************************** Position **********************************/
    public bool IsRelativeX { get; set; }
    public bool IsRelativeY { get; set; }
    public bool IsRelativeZ { get; set; }
    private bool _containX, _containY, _containZ;
	public bool ContainX { get{ return this._containX; } }
	public bool ContainY { get{ return this._containY; } }
	public bool ContainZ { get{ return this._containZ; } }
	private float _x, _y, _z;
	private float[] _controlX, _controlY, _controlZ;
	public float X
	{
		get { return _x; }
		set 
		{ 
			_containX = true;
			_x = value; 
		}
	}
	public float[] ControlPointX { get{ return _controlX; } set{ _controlX = value; } }
	public float Y 
	{ 
		get{ return _y; }
		set 
		{ 
			_containY = true;
			_y = value; 
		}
	}
	public float[] ControlPointY { get{ return _controlY; } set{ _controlY = value; } }
	public float Z
	{
		get { return _z; }
		set 
		{ 
			_containZ = true;
			_z = value; 
		}
	}
	public float[] ControlPointZ { get{ return _controlZ; } set{ _controlZ = value; } }
	public XHash Position( float x, float y, bool isRelative = false )
	{
		this.AddX( x, isRelative );
		this.AddY( y, isRelative );
		return this;
	}
	public XHash Position( float x, float y, float z, bool isRelative = false )
    {
        this.AddX( x, isRelative );
        this.AddY( y, isRelative );
        this.AddZ( z, isRelative );
        return this;
    }
	public XHash Position( Vector3 position, bool isRelative = false )
	{
		this.AddX( position.x, isRelative );
		this.AddY( position.y, isRelative );
		this.AddZ( position.z, isRelative );
		return this;
	}
	public XHash Position( Vector3 start, Vector3 end, bool isRelative = false )
	{
		this._start = this.GetStart().Position(start, isRelative);
		this.AddX( end.x, isRelative );
		this.AddY( end.y, isRelative );
		this.AddZ( end.z, isRelative );
		return this;
	}

	/*********************************** Position **********************************/
    public bool IsRelativeLeft { get; set; }
    public bool IsRelativeTop { get; set; }
    public bool IsRelativeRight { get; set; }
	public bool IsRelativeBottom { get; set; }
    private bool _containLeft, _containTop, _containRight, _containBottom;
	public bool ContainLeft { get{ return this._containLeft; } }
	public bool ContainTop { get{ return this._containTop; } }
	public bool ContainRight { get{ return this._containRight; } }
	public bool ContainBottom { get{ return this._containBottom; } }
	private float _left, _top, _right, _bottom;
	private float[] _controlLeft, _controlTop, _controlRight, _controlBottom;
	public float Left
	{
		get { return _left; }
		set 
		{ 
			_containLeft = true;
			_left = value; 
		}
	}
	public float[] ControlPointLeft { get{ return _controlLeft; } set{ _controlLeft = value; } }
	public float Top
	{
		get { return _top; }
		set 
		{ 
			_containTop = true;
			_top = value;
		}
	}
	public float[] ControlPointTop { get{ return _controlTop; } set{ _controlTop = value; } }
	public float Right
	{
		get { return _right; }
		set 
		{ 
			_containRight = true;
			_right = value; 
		}
	}
	public float[] ControlPointRight { get{ return _controlRight; } set{ _controlRight = value; } }
	public float Bottom
	{
		get { return _bottom; }
		set 
		{ 
			_containBottom = true;
			_bottom = value; 
		}
	}
	public float[] ControlPointBottom { get{ return _controlBottom; } set{ _controlBottom = value; } }
	public XHash Rect( float left, float top, float right, float bottom, bool isRelative = false )
    {
        this.AddLeft( left, isRelative );
        this.AddTop( top, isRelative );
		this.AddRight( right, isRelative );
        this.AddBottom( bottom, isRelative );
        return this;
    }
	public XHash Rect( Toki.Tween.UIRect rect, bool isRelative = false )
	{
		this.AddLeft( rect.left, isRelative );
        this.AddTop( rect.top, isRelative );
		this.AddRight( rect.right, isRelative );
        this.AddBottom( rect.bottom, isRelative );
		return this;
	}
	public XHash Rect( Toki.Tween.UIRect start, Toki.Tween.UIRect end, bool isRelative = false )
	{
		this._start = this.GetStart().Rect(start, isRelative);
		this.AddLeft( end.left, isRelative );
        this.AddTop( end.top, isRelative );
		this.AddRight( end.right, isRelative );
        this.AddBottom( end.bottom, isRelative );
		return this;
	}

	/*********************************** Size **********************************/
    public bool IsRelativeWidth { get; set; }
    public bool IsRelativeHeight { get; set; }
    private bool _containWidth, _containHeight;
	public bool ContainWidth { get{ return this._containWidth; } }
	public bool ContainHeight { get{ return this._containHeight; } }
	private float _width, _height;
	private float[] _controlWidth, _controlHeight;
	public float Width
	{
		get { return _width; }
		set 
		{ 
			_containWidth = true;
			_width = value;
		}
	}
	public float[] ControlPointWidth { get{ return _controlWidth; } set{ _controlWidth = value; } }
	public float Height
	{
		get { return _height; }
		set 
		{ 
			_containHeight = true;
			_height = value;
		}
	}
	public float[] ControlPointHeight { get{ return _controlHeight; } set{ _controlHeight = value; } }
	public XHash SizeDelta( float width, float height, bool isRelative = false )
	{
		this.AddWidth( width, isRelative );
		this.AddHeight( height, isRelative );
		return this;
	}
	public XHash SizeDelta( Vector2 sizeDelta, bool isRelative = false )
	{
		this.AddWidth( sizeDelta.x, isRelative );
		this.AddHeight( sizeDelta.y, isRelative );
		return this;
	}
	public XHash SizeDelta( Vector2 start, Vector2 end, bool isRelative = false )
	{
		this._start = this.GetStart().SizeDelta(start, isRelative);
		this.AddWidth( end.x, isRelative );
		this.AddHeight( end.y, isRelative );
		return this;
	}

	/*********************************** Scale **********************************/
    public bool IsRelativeScaleX { get; set; }
    public bool IsRelativeScaleY { get; set; }
    public bool IsRelativeScaleZ { get; set; }
    private bool _containScaleX, _containScaleY, _containScaleZ;
	public bool ContainScaleX { get{ return this._containScaleX; } }
	public bool ContainScaleY { get{ return this._containScaleY; } }
	public bool ContainScaleZ { get{ return this._containScaleZ; } }
	private float _scaleX, _scaleY, _scaleZ;
	private float[] _controlScaleX, _controlScaleY, _controlScaleZ;
	public float ScaleX
	{
		get { return _scaleX; }
		set 
		{ 
			_containScaleX = true;
			_scaleX = value; 
		}
	}
	public float[] ControlPointScaleX { get{ return _controlScaleX; } set{ _controlScaleX = value; } }
	public float ScaleY
	{
		get { return _scaleY; }
		set 
		{ 
			_containScaleY = true;
			_scaleY = value; 
		}
	}
	public float[] ControlPointScaleY { get{ return _controlScaleY; } set{ _controlScaleY = value; } }
	public float ScaleZ
	{
		get { return _scaleZ; }
		set 
		{ 
			_containScaleZ = true;
			_scaleZ = value; 
		}
	}
	public float[] ControlPointScaleZ { get{ return _controlScaleZ; } set{ _controlScaleZ = value; } }
	public XHash Scale( float x, float y, bool isRelative = false )
	{
		this.AddScaleX( x, isRelative );
		this.AddScaleY( y, isRelative );
		return this;
	}
	public XHash Scale( float x, float y, float z, bool isRelative = false )
    {
        this.AddScaleX( x, isRelative );
		this.AddScaleY( y, isRelative );
		this.AddScaleZ( z, isRelative );
        return this;
    }
	public XHash Scale( Vector3 scale, bool isRelative = false )
	{
		this.AddScaleX( scale.x, isRelative );
		this.AddScaleY( scale.y, isRelative );
		this.AddScaleZ( scale.z, isRelative );
		return this;
	}

	public XHash Scale( Vector3 start, Vector3 end, bool isRelative = false )
	{
		this._start = this.GetStart().Scale(start, isRelative);
		this.AddScaleX( end.x, isRelative );
		this.AddScaleY( end.y, isRelative );
		this.AddScaleZ( end.z, isRelative );
		return this;
	}

	/*********************************** Rotation **********************************/
    public bool IsRelativeRotateX { get; set; }
    public bool IsRelativeRotateY { get; set; }
    public bool IsRelativeRotateZ { get; set; }
    private bool _containRotationX, _containRotationY, _containRotationZ;
	public bool ContainRotationX { get{ return this._containRotationX; } }
	public bool ContainRotationY { get{ return this._containRotationY; } }
	public bool ContainRotationZ { get{ return this._containRotationZ; } }
	private bool _rotateXClockwise, _rotateYClockwise, _rotateZClockwise;
	private float _rotationX, _rotationY, _rotationZ;
	private float[] _controlRotationX, _controlRotationY, _controlRotationZ;
	public float RotationX
	{
		get { return _rotationX; }
		set 
		{ 
			_containRotationX = true;
			_rotationX = value; 
		}
	}
	public float[] ControlPointRotationX { get{ return _controlRotationX; } set{ _controlRotationX = value; } }
	public float RotationY
	{
		get { return _rotationY; }
		set 
		{ 
			_containRotationY = true;
			_rotationY = value; 
		}
	}
	public float[] ControlPointRotationY { get{ return _controlRotationY; } set{ _controlRotationY = value; } }
	public float RotationZ
	{
		get { return _rotationZ; }
		set 
		{ 
			_containRotationZ = true;
			_rotationZ = value; 
		}
	}
	public float[] ControlPointRotationZ { get{ return _controlRotationZ; } set{ _controlRotationZ = value; } }
	public bool RotateXClockwise
	{
		get { return this._rotateXClockwise; }
		set
		{
			this._rotateXClockwise = value;
		}
	}
	public bool RotateYClockwise
	{
		get { return this._rotateYClockwise; }
		set
		{
			this._rotateYClockwise = value;
		}
	}
	public bool RotateZClockwise
	{
		get { return this._rotateZClockwise; }
		set
		{
			this._rotateZClockwise = value;
		}
	}
	public XHash Rotation( float x, float y, float z, bool isRelative = false )
    {
        this.AddRotationX( x, isRelative );
		this.AddRotationY( y, isRelative );
		this.AddRotationZ( z, isRelative );
        return this;
    }
    public XHash Rotation( Vector3 rotation, bool isRelative = false )
    {
        this.AddRotationX( rotation.x, isRelative );
		this.AddRotationY( rotation.y, isRelative );
		this.AddRotationZ( rotation.z, isRelative );
        return this;
    }
	public XHash Rotation( Vector3 start, Vector3 end, bool isRelative = false )
    {
		this._start = this.GetStart().Rotation(start, isRelative);
        this.AddRotationX( end.x, isRelative );
		this.AddRotationY( end.y, isRelative );
		this.AddRotationZ( end.z, isRelative );
        return this;
    }

	/*********************************** Rotation Count **********************************/
	private int _rotateXCount, _rotateYCount, _rotateZCount;
	public int RotateXCount { get{ return _rotateXCount; } }
	public int RotateYCount { get{ return _rotateYCount; } }
	public int RotateZCount { get{ return _rotateZCount; } }

	/*********************************** Create Instance **********************************/
    public static XHash New
    {
        get
        {
            return new XHash();
        }
    }

	
	/*********************************** Add Methods **********************************/
    public XHash AddX( float end, bool isRelative = false )
    {
        this.X = end;
        this.IsRelativeX = isRelative;
        return this;
    }
	public XHash AddX( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.X = start;
		this._start = hash;
		return AddX( end, isRelative );
	}
	public XHash AddControlPointX( params float[] values )
	{
		this._controlX = values;
		return this;
	}
    public XHash AddY( float end, bool isRelative = false )
    {
        this.Y = end;
        this.IsRelativeY = isRelative;
        return this;
    }
	public XHash AddY( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.Y = start;
		this._start = hash;
		return AddY( end, isRelative );
	}
	public XHash AddControlPointY( params float[] values )
	{
		this._controlY = values;
		return this;
	}
    public XHash AddZ( float end, bool isRelative = false )
    {
        this.Z = end;
        this.IsRelativeZ = isRelative;
        return this;
    }
	public XHash AddZ( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.Z = start;
		this._start = hash;
		return AddZ( end, isRelative );
	}
	public XHash AddControlPointZ( params float[] values )
	{
		this._controlZ = values;
		return this;
	}
	public XHash AddLeft( float end, bool isRelative = false )
    {
        this.Left = end;
        this.IsRelativeLeft = isRelative;
        return this;
    }
	public XHash AddLeft( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.Left = start;
		this._start = hash;
		return AddLeft( end, isRelative );
	}
	public XHash AddControlPointLeft( params float[] values )
	{
		this._controlLeft = values;
		return this;
	}
	public XHash AddTop( float end, bool isRelative = false )
    {
        this.Top = end;
        this.IsRelativeTop = isRelative;
        return this;
    }
	public XHash AddTop( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.Top = start;
		this._start = hash;
		return AddTop( end, isRelative );
	}
	public XHash AddControlPointTop( params float[] values )
	{
		this._controlTop = values;
		return this;
	}
	public XHash AddRight( float end, bool isRelative = false )
    {
        this.Right = end;
        this.IsRelativeRight = isRelative;
        return this;
    }
	public XHash AddRight( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.Right = start;
		this._start = hash;
		return AddRight( end, isRelative );
	}
	public XHash AddControlPointRight( params float[] values )
	{
		this._controlRight = values;
		return this;
	}
	public XHash AddBottom( float end, bool isRelative = false )
    {
        this.Bottom = end;
        this.IsRelativeBottom = isRelative;
        return this;
    }
	public XHash AddBottom( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.Bottom = start;
		this._start = hash;
		return AddBottom( end, isRelative );
	}
	public XHash AddControlPointBottom( params float[] values )
	{
		this._controlBottom = values;
		return this;
	}
	public XHash AddWidth( float end, bool isRelative = false )
    {
        this.Width = end;
        this.IsRelativeWidth = isRelative;
        return this;
    }
	public XHash AddWidth( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.Width = start;
		this._start = hash;
		return AddWidth( end, isRelative );
	}
	public XHash AddControlPointWidth( params float[] values )
	{
		this._controlWidth = values;
		return this;
	}
	public XHash AddHeight( float end, bool isRelative = false )
    {
        this.Height = end;
        this.IsRelativeHeight = isRelative;
        return this;
    }
	public XHash AddHeight( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.Height = start;
		this._start = hash;
		return AddHeight( end, isRelative );
	}
	public XHash AddControlPointHeight( params float[] values )
	{
		this._controlHeight = values;
		return this;
	}
    public XHash AddScaleX( float end, bool isRelative = false )
    {
        this.ScaleX = end;
        this.IsRelativeScaleX = isRelative;
        return this;
    }
	public XHash AddScaleX( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.ScaleX = start;
		this._start = hash;
		return AddScaleX( end, isRelative );
	}
	public XHash AddControlScaleX( params float[] values )
	{
		this._controlScaleX = values;
		return this;
	}
    public XHash AddScaleY( float end, bool isRelative = false )
    {
        this.ScaleY = end;
        this.IsRelativeScaleY = isRelative;
        return this;
    }
	public XHash AddScaleY( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.ScaleY = start;
		this._start = hash;
		return AddScaleY( end, isRelative );
	}
	public XHash AddControlScaleY( params float[] values )
	{
		this._controlScaleY = values;
		return this;
	}
    public XHash AddScaleZ( float end, bool isRelative = false )
    {
        this.ScaleZ = end;
        this.IsRelativeScaleZ = isRelative;
        return this;
    }
	public XHash AddScaleZ( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.ScaleZ = start;
		this._start = hash;
		return AddScaleZ( end, isRelative );
	}
	public XHash AddControlScaleZ( params float[] values )
	{
		this._controlScaleZ = values;
		return this;
	}
    public XHash AddRotationX( float end, bool isRelative = false  )
    {
        this.RotationX = end;
        this.IsRelativeRotateX = isRelative;
        return this;
    }
    public XHash AddRotationX( float end, bool clockwise, int rotateCount = 0, bool isRelative = false )
    {
        this.RotationX = end;
		this._rotateXClockwise = clockwise;
		this._rotateXCount = rotateCount;
        this.IsRelativeRotateX = isRelative;
        return this;
    }
	public XHash AddRotationX( float start, float end, bool clockwise, int rotateCount = 0, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.RotationX = start;
		this._start = hash;
		return AddRotationX( end, clockwise, rotateCount, isRelative );
	}
	public XHash AddControlRotationX( params float[] values )
	{
		this._controlRotationX = values;
		return this;
	}
    public XHash AddRotationY( float end, bool isRelative = false  )
    {
        this.RotationY = end;
        this.IsRelativeRotateY = isRelative;
        return this;
    }
	public XHash AddRotationY( float end, bool clockwise, int rotateCount = 0, bool isRelative = false )
    {
        this.RotationY = end;
		this._rotateYClockwise = clockwise;
		this._rotateYCount = rotateCount;
        this.IsRelativeRotateY = isRelative;
        return this;
    }
	public XHash AddRotationY( float start, float end, bool clockwise, int rotateCount = 0, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.RotationY = start;
		this._start = hash;
		return AddRotationY( end, clockwise, rotateCount, isRelative );
	}
	public XHash AddControlRotationY( params float[] values )
	{
		this._controlRotationY = values;
		return this;
	}
    public XHash AddRotationZ( float end, bool isRelative = false  )
    {
        this.RotationZ = end;
        this.IsRelativeRotateZ = isRelative;
        return this;
    }
    public XHash AddRotationZ( float end, bool clockwise, int rotateCount = 0, bool isRelative = false )
    {
        this.RotationZ = end;
		this._rotateZClockwise = clockwise;
		this._rotateZCount = rotateCount;
        this.IsRelativeRotateZ = isRelative;
        return this;
    }
	public XHash AddRotationZ( float start, float end, bool clockwise, int rotateCount = 0, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.RotationZ = start;
		this._start = hash;
		return AddRotationZ( end, clockwise, rotateCount, isRelative );
	}
	public XHash AddControlRotationZ( params float[] values )
	{
		this._controlRotationZ = values;
		return this;
	}
    public XHash AddOnPlay( IExecutable value )
	{
		this.OnPlay = value;
		return this;
	}
	public XHash AddOnUpdate( IExecutable value )
	{
		this.OnUpdate = value;
		return this;
	}
	public XHash AddOnStop( IExecutable value )
	{
		this.OnStop = value;
		return this;
	}
	public XHash AddOnComplete( IExecutable value )
	{
		this.OnComplete = value;
		return this;
	}

	public XHash GetStart()
	{
		if( this._start == null ) this._start = new XHash();
		return (XHash)this._start;
	}
}