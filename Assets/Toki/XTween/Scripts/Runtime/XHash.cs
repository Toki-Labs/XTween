using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

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

public struct XHash : IClassicHandlable
{
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
	private IClassicHandlable _start;

	/*********************************** Position **********************************/
    public bool isRelativeX { get; set; }
    public bool isRelativeY { get; set; }
    public bool isRelativeZ { get; set; }
    private bool _containX, _containY, _containZ;
	public bool containX { get{ return this._containX; } }
	public bool containY { get{ return this._containY; } }
	public bool containZ { get{ return this._containZ; } }
	private float _x, _y, _z;
	public float x
	{
		get { return _x; }
		set 
		{ 
			_containX = true;
			_x = value; 
		}
	}
	public float y
	{
		get { return _y; }
		set 
		{ 
			_containY = true;
			_y = value; 
		}
	}
	public float z
	{
		get { return _z; }
		set 
		{ 
			_containZ = true;
			_z = value; 
		}
	}
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
	public float Left
	{
		get { return _left; }
		set 
		{ 
			_containLeft = true;
			_left = value; 
		}
	}
	public float Top
	{
		get { return _top; }
		set 
		{ 
			_containTop = true;
			_top = value;
		}
	}
	public float Right
	{
		get { return _right; }
		set 
		{ 
			_containRight = true;
			_right = value; 
		}
	}
	public float Bottom
	{
		get { return _bottom; }
		set 
		{ 
			_containBottom = true;
			_bottom = value; 
		}
	}
	public XHash Rect( float left, float top, float right, float bottom, bool isRelative = false )
    {
        this.AddLeft( left, isRelative );
        this.AddTop( top, isRelative );
		this.AddRight( right, isRelative );
        this.AddBottom( bottom, isRelative );
        return this;
    }
	public XHash Rect( UIRect rect, bool isRelative = false )
	{
		this.AddLeft( rect.left, isRelative );
        this.AddTop( rect.top, isRelative );
		this.AddRight( rect.right, isRelative );
        this.AddBottom( rect.bottom, isRelative );
		return this;
	}

	/*********************************** Size **********************************/
    public bool IsRelativeWidth { get; set; }
    public bool IsRelativeHeight { get; set; }
    private bool _containWidth, _containHeight;
	public bool ContainWidth { get{ return this._containWidth; } }
	public bool ContainHeight { get{ return this._containHeight; } }
	private float _width, _height;
	public float Width
	{
		get { return _width; }
		set 
		{ 
			_containWidth = true;
			_width = value;
		}
	}
	public float Height
	{
		get { return _height; }
		set 
		{ 
			_containHeight = true;
			_height = value;
		}
	}
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

	/*********************************** Scale **********************************/
    public bool isRelativeScaleX { get; set; }
    public bool isRelativeScaleY { get; set; }
    public bool isRelativeScaleZ { get; set; }
    private bool _containScaleX, _containScaleY, _containScaleZ;
	public bool containScaleX { get{ return this._containScaleX; } }
	public bool containScaleY { get{ return this._containScaleY; } }
	public bool containScaleZ { get{ return this._containScaleZ; } }
	private float _scaleX, _scaleY, _scaleZ;
	public float scaleX
	{
		get { return _scaleX; }
		set 
		{ 
			_containScaleX = true;
			_scaleX = value; 
		}
	}
	public float scaleY
	{
		get { return _scaleY; }
		set 
		{ 
			_containScaleY = true;
			_scaleY = value; 
		}
	}
	public float scaleZ
	{
		get { return _scaleZ; }
		set 
		{ 
			_containScaleZ = true;
			_scaleZ = value; 
		}
	}
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
		this.AddScaleY( scale.y, isRelative );
		return this;
	}

	/*********************************** Rotation **********************************/
    public bool isRelativeRotateX { get; set; }
    public bool isRelativeRotateY { get; set; }
    public bool isRelativeRotateZ { get; set; }
    private bool _containRotationX, _containRotationY, _containRotationZ;
	public bool containRotationX { get{ return this._containRotationX; } }
	public bool containRotationY { get{ return this._containRotationY; } }
	public bool containRotationZ { get{ return this._containRotationZ; } }
	private bool _rotateXRight, _rotateYRight, _rotateZRight;
	private float _rotationX, _rotationY, _rotationZ;
	public float rotationX
	{
		get { return _rotationX; }
		set 
		{ 
			_containRotationX = true;
			_rotationX = value; 
		}
	}
	public float rotationY
	{
		get { return _rotationY; }
		set 
		{ 
			_containRotationY = true;
			_rotationY = value; 
		}
	}
	public float rotationZ
	{
		get { return _rotationZ; }
		set 
		{ 
			_containRotationZ = true;
			_rotationZ = value; 
		}
	}
	public bool rotateXRight
	{
		get { return this._rotateXRight; }
		set
		{
			this._rotateXRight = value;
		}
	}
	public bool rotateYRight
	{
		get { return this._rotateYRight; }
		set
		{
			this._rotateYRight = value;
		}
	}
	public bool rotateZRight
	{
		get { return this._rotateZRight; }
		set
		{
			this._rotateZRight = value;
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

	/*********************************** Color **********************************/
    public bool isRelativeRed { get; set; }
    public bool isRelativeGreen { get; set; }
    public bool isRelativeBlue { get; set; }
    public bool isRelativeAlpha { get; set; }
    private bool _containRed, _containGreen, _containBlue, _containAlpha;
	public bool containRed { get{ return this._containRed; } }
	public bool containGreen { get{ return this._containGreen; } }
	public bool containBlue { get{ return this._containBlue; } }
	public bool containAlpha { get{ return this._containAlpha; } }
	private float _red, _green, _blue, _alpha;
	public float red
	{
		get { return _red; }
		set 
		{ 
			_containRed = true;
			_red = value; 
		}
	}
	public float green
	{
		get { return _green; }
		set 
		{ 
			_containGreen = true;
			_green = value; 
		}
	}
	public float blue
	{
		get { return _blue; }
		set 
		{ 
			_containBlue = true;
			_blue = value; 
		}
	}
	public float alpha
	{
		get { return _alpha; }
		set 
		{ 
			_containAlpha = true;
			_alpha = value; 
		}
	}

	/*********************************** Rotation Count **********************************/
	private int _rotateXCount, _rotateYCount, _rotateZCount;
	public int rotateXCount { get{ return _rotateXCount; } }
	public int rotateYCount { get{ return _rotateYCount; } }
	public int rotateZCount { get{ return _rotateZCount; } }
    public bool containColor { get {  return (this._containRed || this._containGreen || this._containBlue || this._containAlpha); } }

	/*********************************** Create Instance **********************************/
    public static XHash New
    {
        get
        {
            XHash hash = new XHash();
            return hash;
        }
    }

	
	/*********************************** Add Methods **********************************/
    public XHash AddX( float end, bool isRelative = false )
    {
        this.x = end;
        this.isRelativeX = isRelative;
        return this;
    }
	public XHash AddX( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.x = start;
		this._start = hash;
		return AddX( end, isRelative );
	}
    public XHash AddY( float end, bool isRelative = false )
    {
        this.y = end;
        this.isRelativeY = isRelative;
        return this;
    }
	public XHash AddY( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.y = start;
		this._start = hash;
		return AddY( end, isRelative );
	}
    public XHash AddZ( float end, bool isRelative = false )
    {
        this.z = end;
        this.isRelativeZ = isRelative;
        return this;
    }
	public XHash AddZ( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.z = start;
		this._start = hash;
		return AddZ( end, isRelative );
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
    public XHash AddScaleX( float end, bool isRelative = false )
    {
        this.scaleX = end;
        this.isRelativeScaleX = isRelative;
        return this;
    }
	public XHash AddScaleX( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.scaleX = start;
		this._start = hash;
		return AddScaleX( end, isRelative );
	}
    public XHash AddScaleY( float end, bool isRelative = false )
    {
        this.scaleY = end;
        this.isRelativeScaleY = isRelative;
        return this;
    }
	public XHash AddScaleY( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.scaleY = start;
		this._start = hash;
		return AddScaleY( end, isRelative );
	}
    public XHash AddScaleZ( float end, bool isRelative = false )
    {
        this.scaleZ = end;
        this.isRelativeScaleZ = isRelative;
        return this;
    }
	public XHash AddScaleZ( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.scaleZ = start;
		this._start = hash;
		return AddScaleZ( end, isRelative );
	}
    public XHash AddRotationX( float end, bool isRelative = false  )
    {
        this.rotationX = end;
        this.isRelativeRotateX = isRelative;
        return this;
    }
    public XHash AddRotationX( float end, bool rotateRight, int rotateCount = 0, bool isRelative = false )
    {
        this.rotationX = end;
		this._rotateXRight = rotateRight;
		this._rotateXCount = rotateCount;
        this.isRelativeRotateX = isRelative;
        return this;
    }
	public XHash AddRotationX( float start, float end, bool rotateRight, int rotateCount = 0, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.rotationX = start;
		this._start = hash;
		return AddRotationX( end, rotateRight, rotateCount, isRelative );
	}
    public XHash AddRotationY( float end, bool isRelative = false  )
    {
        this.rotationY = end;
        this.isRelativeRotateY = isRelative;
        return this;
    }
	public XHash AddRotationY( float end, bool rotateRight, int rotateCount = 0, bool isRelative = false )
    {
        this.rotationY = end;
		this._rotateYRight = rotateRight;
		this._rotateYCount = rotateCount;
        this.isRelativeRotateY = isRelative;
        return this;
    }
	public XHash AddRotationY( float start, float end, bool rotateRight, int rotateCount = 0, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.rotationY = start;
		this._start = hash;
		return AddRotationY( end, rotateRight, rotateCount, isRelative );
	}
    public XHash AddRotationZ( float end, bool isRelative = false  )
    {
        this.rotationZ = end;
        this.isRelativeRotateZ = isRelative;
        return this;
    }
    public XHash AddRotationZ( float end, bool rotateRight, int rotateCount = 0, bool isRelative = false )
    {
        this.rotationZ = end;
		this._rotateZRight = rotateRight;
		this._rotateZCount = rotateCount;
        this.isRelativeRotateZ = isRelative;
        return this;
    }
	public XHash AddRotationZ( float start, float end, bool rotateRight, int rotateCount = 0, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.rotationZ = start;
		this._start = hash;
		return AddRotationZ( end, rotateRight, rotateCount, isRelative );
	}
    public XHash AddRed( float end, bool isRelative = false )
    {
        this.red = end;
        this.isRelativeRed = isRelative;
        return this;
    }
	public XHash AddRed( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.red = start;
		this._start = hash;
		return AddRed( end, isRelative );
	}
    public XHash AddGreen( float end, bool isRelative = false )
    {
        this.green = end;
        this.isRelativeGreen = isRelative;
        return this;
    }
	public XHash AddGreen( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.green = start;
		this._start = hash;
		return AddGreen( end, isRelative );
	}
    public XHash AddBlue( float end, bool isRelative = false )
    {
        this.blue = end;
        this.isRelativeBlue = isRelative;
        return this;
    }
	public XHash AddBlue( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.blue = start;
		this._start = hash;
		return AddBlue( end, isRelative );
	}
    public XHash AddAlpha( float end, bool isRelative = false )
    {
        this.alpha = end;
        this.isRelativeAlpha = isRelative;
        return this;
    }
	public XHash AddAlpha( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.alpha = start;
		this._start = hash;
		return AddAlpha( end, isRelative );
	}
    public XHash AddOnPlay( IExecutable value )
	{
		this.onPlay = value;
		return this;
	}
	public XHash AddOnUpdate( IExecutable value )
	{
		this.onUpdate = value;
		return this;
	}
	public XHash AddOnStop( IExecutable value )
	{
		this.onStop = value;
		return this;
	}
	public XHash AddOnComplete( IExecutable value )
	{
		this.onComplete = value;
		return this;
	}

	public XHash GetStart()
	{
		if( this._start == null ) this._start = new XHash();
		return (XHash)this._start;
	}
}