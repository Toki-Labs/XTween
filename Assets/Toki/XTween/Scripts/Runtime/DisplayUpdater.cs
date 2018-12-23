using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum XProperty
{
	x,
	y,
	z,
	scaleX,
	scaleY,
	scaleZ,
	rotationX,
	rotationY,
	rotationZ,
	red,
	blue,
	green,
	alpha
}
	
public class DisplayUpdater : AbstractUpdater, IUpdating
{
	protected float _invert;
    protected float _factor = 0f;
    protected int _updateCount;
	protected GameObject _target = null;
	protected Transform _transform;
    protected Vector3 _sPos;
    protected Vector3 _dPos;
    protected Vector3 _sSca;
    protected Vector3 _dSca;
    protected Vector3 _sRot;
    protected Vector3 _dRot;
    protected Color _sColor;
    protected Color _dColor;
	protected Vector3 _pos;
	protected Vector3 _rot;
	protected Vector3 _sca;
	protected Color _col;
	protected XHash _start;
	protected XHash _finish;
    protected IColorUpdatable _colorUpdater;
	protected Action _stopOnDestroyHandler;
	protected List<Action> _updateList;
		

    public override GameObject target
	{
		get { return _target; }
		set 
		{ 
			_target = value;
		}
	}
		
	public override IClassicHandlable start
	{
		set { _start = (XHash)value; }
	}
		
	public override IClassicHandlable finish
	{
		set { _finish = (XHash)value; }
	}
		
	public Action stopOnDestroyHandler
	{
		set { _stopOnDestroyHandler = value; }
	}
		
	protected virtual void FindColorObject()
	{
        if( this._finish.containColor )
        {
            if( this._finish.containColorComponentType )
            {
                Type type = this._finish.colorComponentType;
                this._colorUpdater = ColorUpdatorFactory.Create( type,target );
            }
            else
            {
                this._colorUpdater = ColorUpdatorFactory.Find( target );
            }
        }
	}
		
	//source set
	public override void ResolveValues()
	{
		if( _target == null )
		{
			if( this._stopOnDestroyHandler != null )
			{
				this._stopOnDestroyHandler.Invoke();
			}
			return;
		}

		this._transform = this._target.transform;
		this.FindColorObject();
			
		_pos = this._transform.localPosition;
		_rot = this._transform.localEulerAngles;
		_sca = this._transform.localScale;
        
		//if exist source, set values
		if( _start.containX )
		{
			_pos.x = _start.x;
		}
		if( _start.containY )
		{
			_pos.y = _start.y;
		}
		if( _start.containZ )
		{
			_pos.z = _start.z;
		}
		if( _start.containScaleX )
		{
			_sca.x = _start.scaleX;
		}
		if( _start.containScaleY )
		{
			_sca.y = _start.scaleY;
		}
		if( _start.containScaleZ )
		{
			_sca.z = _start.scaleZ;
		}
		if( _start.containRotationX )
		{
			_rot.x = _start.rotationX;
		}
		if( _start.containRotationY )
		{
			_rot.y = _start.rotationY;
		}
		if( _start.containRotationZ )
		{
			_rot.z = _start.rotationZ;
		}
        
        bool changedPos = false;
		bool changedSca = false;
		bool changedRot = false;
		bool changedCol = false;
        this._updateList = new List<Action>();
			
		float x = _pos.x;
		float y = _pos.y;
		float z = _pos.z;
		bool addForce = false;
		if( this is BezierUpdater )
		{
			addForce = _finish.containX || _finish.containY || _finish.containZ;
		}
		if( _finish.containX )
		{
            if( x != _finish.x || _finish.isRelativeX || addForce )
            { 
			    changedPos = true;
			    x = _finish.isRelativeX ? x + _finish.x : _finish.x;
                this._updateList.Add(GetUpdateX());
            }
		}
		if( _finish.containY )
		{
            if( y != _finish.y || _finish.isRelativeY || addForce )
            {
			    changedPos = true;;
			    y = _finish.isRelativeY ? y + _finish.y : _finish.y;
                this._updateList.Add(GetUpdateY());
            }
		}
		if( _finish.containZ )
		{
            if( z != _finish.z || _finish.isRelativeZ || addForce )
            {
			    changedPos = true;
			    z = _finish.isRelativeZ ? z + _finish.z : _finish.z;
                this._updateList.Add(GetUpdateZ());
            }
		}
		float scaleX = _sca.x;
		float scaleY = _sca.y;
		float scaleZ = _sca.z;
		if( _finish.containScaleX )
		{
            if( scaleX != _finish.scaleX || _finish.isRelativeScaleX )
            {
			    changedSca = true;
			    scaleX = _finish.isRelativeScaleX ? scaleX + _finish.scaleX : _finish.scaleX;
                this._updateList.Add(GetUpdateScaleX());
            }
		}
		if( _finish.containScaleY )
		{
            if( scaleY != _finish.scaleY || _finish.isRelativeScaleY )
            {
			    changedSca = true;
			    scaleY = _finish.isRelativeScaleY ? scaleY + _finish.scaleY : _finish.scaleY;
                this._updateList.Add(GetUpdateScaleY());
            }
		}
		if( _finish.containScaleZ )
		{
            if( scaleZ != _finish.scaleZ || _finish.isRelativeScaleZ )
            {
			    changedSca = true;
			    scaleZ = _finish.isRelativeScaleZ ? scaleZ + _finish.scaleZ : _finish.scaleZ;
                this._updateList.Add(GetUpdateScaleZ());
            }
		}
		float rotationX = _rot.x;
		float rotationY = _rot.y;
		float rotationZ = _rot.z;
		if( _finish.containRotationX )
		{
			if( rotationX != _finish.rotationX || _finish.rotateXCount > 0 || _finish.isRelativeRotateX )
            {
			    changedRot = true;
				rotationX = _finish.isRelativeRotateX ? rotationX + _finish.rotationX : this.GetRotation( _rot.x, _finish.rotationX, _finish.rotateXRight, _finish.rotateXCount );
                this._updateList.Add(GetUpdateRotationX());
            }
		}
		if( _finish.containRotationY )
		{
			if( rotationY != _finish.rotationY || _finish.rotateYCount > 0 || _finish.isRelativeRotateY )
            {
			    changedRot = true;
				rotationY = _finish.isRelativeRotateY ? rotationY + _finish.rotationY : this.GetRotation( _rot.y, _finish.rotationY, _finish.rotateYRight, _finish.rotateYCount );
                this._updateList.Add(GetUpdateRotationY());
            }
		}
		if( _finish.containRotationZ )
		{
            if( rotationZ != _finish.rotationZ || _finish.rotateZCount > 0 || _finish.isRelativeRotateZ )
            {
			    changedRot = true;
				rotationZ = _finish.isRelativeRotateZ ? rotationZ + _finish.rotationZ : this.GetRotation( _rot.z, _finish.rotationZ, _finish.rotateZRight, _finish.rotateZCount );
                this._updateList.Add(GetUpdateRotationZ());
            }
		}

		if( changedPos )
		{
            this._transform.localPosition = _pos;
			this._sPos = new Vector3( _pos.x, _pos.y, _pos.z );
			this._dPos = new Vector3( x, y, z );
			this.UpdatePosition();
            this._updateList.Add(UpdatePosition);
		}
		if( changedSca )
		{
            this._transform.localScale = _sca;
			this._sSca = new Vector3( _sca.x, _sca.y, _sca.z );
			this._dSca = new Vector3( scaleX, scaleY, scaleZ );
			this.UpdateScale();
            this._updateList.Add(UpdateScale);
		}
		if( changedRot )
		{
            this._transform.localEulerAngles = _rot;
			this._sRot = new Vector3( _rot.x, _rot.y, _rot.z );
			this._dRot = new Vector3( rotationX, rotationY, rotationZ );
			this.UpdateRotation();
            this._updateList.Add(UpdateRotation);
		}

        if ( this._colorUpdater != null )
        {
            _col = this._colorUpdater.GetColor();
            if (_start.containRed)
            {
                _col.r = _start.red;
            }
            if (_start.containGreen)
            {
                _col.g = _start.green;
            }
            if (_start.containBlue)
            {
                _col.b = _start.blue;
            }
            if (_start.containAlpha)
            {
                _col.a = _start.alpha;
            }

            float red = _col.r;
            float green = _col.g;
            float blue = _col.b;
            float alpha = _col.a;
            if (_finish.containRed)
            {
                if (red != _finish.red || _finish.isRelativeRed)
                {
                    changedCol = true;
                    red = _finish.isRelativeRed ? red + _finish.red : _finish.red;
                    this._updateList.Add(UpdateColorRed);
                }
            }
            if (_finish.containGreen)
            {
                if (green != _finish.green || _finish.isRelativeGreen)
                {
                    changedCol = true;
                    green = _finish.isRelativeGreen ? green + _finish.green : _finish.green;
                    this._updateList.Add(UpdateColorGreen);
                }
            }
            if (_finish.containBlue)
            {
                if (blue != _finish.blue || _finish.isRelativeBlue)
                {
                    changedCol = true;
                    blue = _finish.isRelativeBlue ? blue + _finish.blue : _finish.blue;
                    this._updateList.Add(UpdateColorBlue);
                }
            }
            if (_finish.containAlpha)
            {
                if (alpha != _finish.alpha || _finish.isRelativeAlpha)
                {
                    changedCol = true;
                    alpha = _finish.isRelativeAlpha ? alpha + _finish.alpha : _finish.alpha;
                    this._updateList.Add(UpdateColorAlpha);
                }
            }
            if (changedCol)
            {
                this.UpdateColor();
                this._sColor = new Color(_col.r, _col.b, _col.g, _col.a);
                this._dColor = new Color(red, green, blue, alpha);
				this.UpdateColor();
				this._updateList.Add(UpdateColor);
            }
        }

        this._updateCount = this._updateList.Count;
	}

	private float GetRotation( float start, float finish, bool rotateRight, int rotateCount )
	{
		if( rotateRight )
		{
			if( start < finish )
			{
				while ( start < finish )
				{
					finish -= 360f;
				}
			}
			if( rotateCount > 0 )
			{
				float value = ( finish - start ) % 360;
				finish = start + value - 360 * rotateCount;
			}
		}
		else
		{
			if( start > finish )
			{
				while ( start > finish )
				{
					finish += 360f;
				}
			}
			if( rotateCount > 0 )
			{
				float value = ( finish - start ) % 360;
				finish = start + value + 360 * rotateCount;
			}
		}
		return finish;
	}
		
	public override void Updating( float factor )
	{
        _invert = 1.0f - factor;
        _factor = factor;
        if (this._target == null)
        {
            this._stopOnDestroyHandler.Invoke();
            this.Dispose();
        }
        else
        {
            for (int i = 0; i < this._updateCount; ++i)
            {
                this._updateList[i].Invoke();
            }
        }
	}

    public void Dispose()
    {
		Debug.Log ("Dispose");
        this._stopOnDestroyHandler = null;
        this._updateList.Clear();
        this._updateList = null;
        this._transform = null;
    }

	protected virtual Action GetUpdateX() { return this.UpdateX; }
	protected virtual Action GetUpdateY() { return this.UpdateY; }
	protected virtual Action GetUpdateZ() { return this.UpdateZ; }
	protected virtual Action GetUpdateScaleX() { return this.UpdateScaleX; }
	protected virtual Action GetUpdateScaleY() { return this.UpdateScaleY; }
	protected virtual Action GetUpdateScaleZ() { return this.UpdateScaleZ; }
	protected virtual Action GetUpdateRotationX() { return this.UpdateRotationX; }
	protected virtual Action GetUpdateRotationY() { return this.UpdateRotationY; }
	protected virtual Action GetUpdateRotationZ() { return this.UpdateRotationZ; }

    protected virtual void UpdateX()
	{
		_pos.x = _sPos.x * _invert + _dPos.x * _factor;
	}
    protected virtual void UpdateY()
	{
		_pos.y = _sPos.y * _invert + _dPos.y * _factor;
	}
    protected virtual void UpdateZ()
	{
		_pos.z = _sPos.z * _invert + _dPos.z * _factor;
	}
    protected virtual void UpdateScaleX()
	{
		_sca.x = _sSca.x * _invert + _dSca.x * _factor;
	}
    protected virtual void UpdateScaleY()
	{
		_sca.y = _sSca.y * _invert + _dSca.y * _factor;
	}
    protected virtual void UpdateScaleZ()
	{
		_sca.z = _sSca.z * _invert + _dSca.z * _factor;
	}
    protected virtual void UpdateRotationX()
	{
		_rot.x = _sRot.x * _invert + _dRot.x * _factor;
	}
    protected virtual void UpdateRotationY()
	{
		_rot.y = _sRot.y * _invert + _dRot.y * _factor;
	}
    protected virtual void UpdateRotationZ()
	{
		_rot.z = _sRot.z * _invert + _dRot.z * _factor;
	}
    protected virtual void UpdateColorRed()
	{
		_col.r = _sColor.r * _invert + _dColor.r * _factor;
	}
    protected virtual void UpdateColorGreen()
	{
		_col.g = _sColor.g * _invert + _dColor.g * _factor;
	}
    protected virtual void UpdateColorBlue()
	{
		_col.b = _sColor.b * _invert + _dColor.b * _factor;
	}
    protected virtual void UpdateColorAlpha()
	{
		_col.a = _sColor.a * _invert + _dColor.a * _factor;
	}
		
	//update transform
	private void UpdatePosition()
	{
		this._transform.localPosition = _pos;
	}
	private void UpdateScale()
	{
		this._transform.localScale = _sca;
	}
	private void UpdateRotation()
	{
		this._transform.localEulerAngles = _rot;
	}
    private void UpdateColor()
    {
        this._colorUpdater.SetColor(_col);
    }
		
	public override IUpdating Clone()
	{
		DisplayUpdater instance = new DisplayUpdater();
        instance.start = this._start;
        instance.finish = this._finish;
		return instance;
	}
}