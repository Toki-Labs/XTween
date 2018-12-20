using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

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

    private bool _isRelativeX;
    private bool _isRelativeY;
    private bool _isRelativeZ;
    public bool isRelativeX
    {
        get { return _isRelativeX; }
        set { _isRelativeX = value; }
    }
    public bool isRelativeY
    {
        get { return _isRelativeY; }
        set { _isRelativeY = value; }
    }
    public bool isRelativeZ
    {
        get { return _isRelativeZ; }
        set { _isRelativeZ = value; }
    }
    private bool _containX;
	private bool _containY;
	private bool _containZ;
	public bool containX { get{ return this._containX; } }
	public bool containY { get{ return this._containY; } }
	public bool containZ { get{ return this._containZ; } }
	private float _x;
	private float _y;
	private float _z;
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

    private bool _isRelativeScaleX;
    private bool _isRelativeScaleY;
    private bool _isRelativeScaleZ;
    public bool isRelativeScaleX
    {
        get { return _isRelativeScaleX; }
        set { _isRelativeScaleX = value; }
    }
    public bool isRelativeScaleY
    {
        get { return _isRelativeScaleY; }
        set { _isRelativeScaleY = value; }
    }
    public bool isRelativeScaleZ
    {
        get { return _isRelativeScaleZ; }
        set { _isRelativeScaleZ = value; }
    }
    private bool _containScaleX;
	private bool _containScaleY;
	private bool _containScaleZ;
	public bool containScaleX { get{ return this._containScaleX; } }
	public bool containScaleY { get{ return this._containScaleY; } }
	public bool containScaleZ { get{ return this._containScaleZ; } }
	private float _scaleX;
	private float _scaleY;
	private float _scaleZ;
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

    private bool _isRelativeRotateX;
    private bool _isRelativeRotateY;
    private bool _isRelativeRotateZ;
    public bool isRelativeRotateX
    {
        get { return _isRelativeRotateX; }
        set { _isRelativeRotateX = value; }
    }
    public bool isRelativeRotateY
    {
        get { return _isRelativeRotateY; }
        set { _isRelativeRotateY = value; }
    }
    public bool isRelativeRotateZ
    {
        get { return _isRelativeRotateZ; }
        set { _isRelativeRotateZ = value; }
    }
    private bool _containRotationX;
	private bool _containRotationY;
	private bool _containRotationZ;
	public bool containRotationX { get{ return this._containRotationX; } }
	public bool containRotationY { get{ return this._containRotationY; } }
	public bool containRotationZ { get{ return this._containRotationZ; } }
	private bool _rotateXRight;
	private bool _rotateYRight;
	private bool _rotateZRight;
	private float _rotationX;
	private float _rotationY;
	private float _rotationZ;
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

    private bool _isRelativeRed;
    private bool _isRelativeGreen;
    private bool _isRelativeBlue;
    private bool _isRelativeAlpha;
    public bool isRelativeRed
    {
        get { return this._isRelativeRed; }
        set { this._isRelativeRed = value; }
    }
    public bool isRelativeGreen
    {
        get { return this._isRelativeGreen; }
        set { this._isRelativeGreen = value; }
    }
    public bool isRelativeBlue
    {
        get { return this._isRelativeBlue; }
        set { this._isRelativeBlue = value; }
    }
    public bool isRelativeAlpha
    {
        get { return this._isRelativeAlpha; }
        set { this._isRelativeAlpha = value; }
    }
    private bool _containRed;
	private bool _containGreen;
	private bool _containBlue;
	private bool _containAlpha;
	public bool containRed { get{ return this._containRed; } }
	public bool containGreen { get{ return this._containGreen; } }
	public bool containBlue { get{ return this._containBlue; } }
	public bool containAlpha { get{ return this._containAlpha; } }
	private float _red;
	private float _green;
	private float _blue;
	private float _alpha;
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

	private int _rotateXCount;
	private int _rotateYCount;
	private int _rotateZCount;
	public int rotateXCount { get{ return _rotateXCount; } }
	public int rotateYCount { get{ return _rotateYCount; } }
	public int rotateZCount { get{ return _rotateZCount; } }

    public bool containColor { get {  return (this._containRed || this._containGreen || this._containBlue || this._containAlpha); } }

    private bool _containType;
    private Type _type;
    public bool containColorComponentType { get { return this._containType; } }
    public Type colorComponentType
    {
        get { return _type; }
        set
        {
            _containType = true;
            _type = value;
        }
    }

	public static XHash New
    {
        get
        {
            XHash hash = new XHash();
            return hash;
        }
    }

	public XHash Position( float x, float y, bool isRelative = false )
	{
		this.x = x;
		this.y = y;
		return this;
	}
	
	public XHash Position( float x, float y, float z, bool isRelative = false )
    {
        this.x = x;
        this.y = y;
        this.z = z;
        return this;
    }

	public XHash Position( Vector3 position, bool isRelative = false )
	{
		this.x = position.x;
		this.y = position.y;
		this.z = position.z;
		return this;
	}
	
	public XHash Scale( float x, float y, bool isRelative = false )
	{
		this.scaleX = x;
		this.scaleY = y;
		return this;
	}
	
	public XHash Scale( float x, float y, float z, bool isRelative = false )
    {
        this.scaleX = x;
        this.scaleY = y;
        this.scaleZ = z;
        return this;
    }

	public XHash Scale( Vector3 scale, bool isRelative = false )
	{
		this.scaleX = scale.x;
		this.scaleY = scale.y;
		this.scaleZ = scale.z;
		return this;
	}
	
	public XHash Rotation( float x, float y, float z, bool isRelative = false )
    {
        this.rotationX = x;
        this.rotationY = y;
        this.rotationZ = z;
        return this;
    }

    public XHash Rotation( Vector3 rotation, bool isRelative = false )
    {
        this.rotationX = rotation.x;
        this.rotationY = rotation.y;
        this.rotationZ = rotation.z;
        return this;
    }

    public XHash AddX( float end, bool isRelative = false )
    {
        this.x = end;
        this._isRelativeX = isRelative;
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
        this._isRelativeY = isRelative;
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
        this._isRelativeZ = isRelative;
        return this;
    }
	public XHash AddZ( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.z = start;
		this._start = hash;
		return AddZ( end, isRelative );
	}
    public XHash AddScaleX( float end, bool isRelative = false )
    {
        this.scaleX = end;
        this._isRelativeScaleX = isRelative;
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
        this._isRelativeScaleY = isRelative;
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
        this._isRelativeScaleZ = isRelative;
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
        this._isRelativeRotateX = isRelative;
        return this;
    }
    public XHash AddRotationX( float end, bool rotateRight, int rotateCount = 0, bool isRelative = false )
    {
        this.rotationX = end;
		this._rotateXRight = rotateRight;
		this._rotateXCount = rotateCount;
        this._isRelativeRotateX = isRelative;
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
        this._isRelativeRotateY = isRelative;
        return this;
    }
	public XHash AddRotationY( float end, bool rotateRight, int rotateCount = 0, bool isRelative = false )
    {
        this.rotationY = end;
		this._rotateYRight = rotateRight;
		this._rotateYCount = rotateCount;
        this._isRelativeRotateY = isRelative;
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
        this._isRelativeRotateZ = isRelative;
        return this;
    }
    public XHash AddRotationZ( float end, bool rotateRight, int rotateCount = 0, bool isRelative = false )
    {
        this.rotationZ = end;
		this._rotateZRight = rotateRight;
		this._rotateZCount = rotateCount;
        this._isRelativeRotateZ = isRelative;
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
        this._isRelativeRed = isRelative;
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
        this._isRelativeGreen = isRelative;
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
        this._isRelativeBlue = isRelative;
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
        this._isRelativeAlpha = isRelative;
        return this;
    }
	public XHash AddAlpha( float start, float end, bool isRelative = false )
	{
		XHash hash = this.GetStart();
		hash.alpha = start;
		this._start = hash;
		return AddAlpha( end, isRelative );
	}
    public XHash AddColorComponentType( Type type )
    {
        this.colorComponentType = type;
        return this;
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
		if( this._start == null )
		{
			this._start = new XHash();
		}
		return (XHash)this._start;
	}
}