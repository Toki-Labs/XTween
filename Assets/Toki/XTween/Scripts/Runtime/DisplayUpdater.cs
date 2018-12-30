using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DisplayUpdater : AbstractUpdater, IUpdating
{
    protected int _updateCount;
	protected GameObject _target = null;
	protected Transform _transform;
	protected RectTransform _transformRect;
    protected Vector3 _sPos;
    protected Vector3 _dPos;
	protected UIRect _sRect;
	protected UIRect _dRect;
	protected Vector2 _sSize;
	protected Vector2 _dSize;
    protected Vector3 _sSca;
    protected Vector3 _dSca;
    protected Vector3 _sRot;
    protected Vector3 _dRot;
    protected Vector3 _pos;
	protected UIRect _rect;
	protected Vector2 _size;
	protected Vector3 _rot;
	protected Vector3 _sca;
	protected XHash _start;
	protected XHash _finish;
	protected List<Action> _updateList;
    public GameObject Target
	{
		get { return _target; }
		set 
		{ 
			_target = value;
		}
	}
		
	public override IClassicHandlable Start
	{
		set { _start = (XHash)value; }
	}
		
	public override IClassicHandlable Finish
	{
		set { _finish = (XHash)value; }
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
		this._transformRect = this._target.transform as RectTransform;
			
		if( this._transformRect == null )
		{
			_pos = this._transform.localPosition;
		}
		else
		{
			_pos = this._transformRect.anchoredPosition3D;
			_size = this._transformRect.sizeDelta;
			_rect = new UIRect( this._transformRect.offsetMin.x, 
								-this._transformRect.offsetMax.y,
								-this._transformRect.offsetMax.x,
								this._transformRect.offsetMin.y);
		}
		_rot = this._transform.localEulerAngles;
		_sca = this._transform.localScale;
        
		//if exist source, set values
		if( _start.ContainX )
		{
			_pos.x = _start.X;
		}
		if( _start.ContainY )
		{
			_pos.y = _start.Y;
		}
		if( _start.ContainZ )
		{
			_pos.z = _start.Z;
		}
		if( _start.ContainLeft )
		{
			_rect.left = _start.Left;
		}
		if( _start.ContainTop )
		{
			_rect.top = _start.Top;
		}
		if( _start.ContainRight )
		{
			_rect.right = _start.Right;
		}
		if( _start.ContainBottom )
		{
			_rect.bottom = _start.Bottom;
		}
		if( _start.ContainWidth )
		{
			_size.x = _start.Width;
		}
		if( _start.ContainHeight )
		{
			_size.x = _start.Height;
		}
		if( _start.ContainScaleX )
		{
			_sca.x = _start.ScaleX;
		}
		if( _start.ContainScaleY )
		{
			_sca.y = _start.ScaleY;
		}
		if( _start.ContainScaleZ )
		{
			_sca.z = _start.ScaleZ;
		}
		if( _start.ContainRotationX )
		{
			_rot.x = _start.RotationX;
		}
		if( _start.ContainRotationY )
		{
			_rot.y = _start.RotationY;
		}
		if( _start.ContainRotationZ )
		{
			_rot.z = _start.RotationZ;
		}
        
        bool changedPos = false;
		bool changedRect = false;
		bool changedSize = false;
		bool changedSca = false;
		bool changedRot = false;
        this._updateList = new List<Action>();
			
		float x = _pos.x;
		float y = _pos.y;
		float z = _pos.z;

		if( _finish.ControlPointX != null && !_finish.ContainX ) _finish.X = _pos.x;
		if( _finish.ControlPointY != null && !_finish.ContainY ) _finish.Y = _pos.y;
		if( _finish.ControlPointZ != null && !_finish.ContainZ ) _finish.Z = _pos.z;
		
		if( _finish.ContainX )
		{
            if( x != _finish.X || _finish.ControlPointX != null || _finish.IsRelativeX )
            { 
			    changedPos = true;
			    x = _finish.IsRelativeX ? x + _finish.X : _finish.X;
                this._updateList.Add(GetUpdateX());
            }
		}
		if( _finish.ContainY )
		{
            if( y != _finish.Y || _finish.ControlPointY != null || _finish.IsRelativeY )
            {
			    changedPos = true;;
			    y = _finish.IsRelativeY ? y + _finish.Y : _finish.Y;
                this._updateList.Add(GetUpdateY());
            }
		}
		if( _finish.ContainZ )
		{
            if( z != _finish.Z || _finish.ControlPointZ != null || _finish.IsRelativeZ )
            {
			    changedPos = true;
			    z = _finish.IsRelativeZ ? z + _finish.Z : _finish.Z;
                this._updateList.Add(GetUpdateZ());
            }
		}
		float left = _rect.left;
		float top = _rect.top;
		float right = _rect.right;
		float bottom = _rect.bottom;

		if( _finish.ControlPointLeft != null && !_finish.ContainLeft ) _finish.Left = _rect.left;
		if( _finish.ControlPointRight != null && !_finish.ContainRight ) _finish.Right = _rect.right;
		if( _finish.ControlPointTop != null && !_finish.ContainTop ) _finish.Top = _rect.top;
		if( _finish.ControlPointBottom != null && !_finish.ContainBottom ) _finish.Bottom = _rect.bottom;

		if( _finish.ContainLeft )
		{
            if( left != _finish.Left || _finish.ControlPointLeft != null || _finish.IsRelativeLeft )
            { 
			    changedRect = true;
			    left = _finish.IsRelativeLeft ? left + _finish.Left : _finish.Left;
                this._updateList.Add(GetUpdateLeft());
            }
		}
		if( _finish.ContainRight )
		{
            if( right != _finish.Right || _finish.ControlPointRight != null || _finish.IsRelativeRight )
            { 
			    changedRect = true;
			    right = _finish.IsRelativeRight ? right + _finish.Right : _finish.Right;
                this._updateList.Add(GetUpdateRight());
            }
		}
		if( _finish.ContainTop )
		{
            if( top != _finish.Top || _finish.ControlPointTop != null || _finish.IsRelativeTop )
            { 
			    changedRect = true;
			    top = _finish.IsRelativeTop ? top + _finish.Top : _finish.Top;
                this._updateList.Add(GetUpdateTop());
            }
		}
		if( _finish.ContainBottom )
		{
            if( bottom != _finish.Bottom || _finish.ControlPointBottom != null || _finish.IsRelativeBottom )
            { 
			    changedRect = true;
			    bottom = _finish.IsRelativeBottom ? bottom + _finish.Bottom : _finish.Bottom;
                this._updateList.Add(GetUpdateBottom());
            }
		}
		float width = _size.x;
		float height = _size.y;

		if( _finish.ControlPointWidth != null && !_finish.ContainWidth ) _finish.Width = _size.x;
		if( _finish.ControlPointHeight != null && !_finish.ContainHeight ) _finish.Height = _size.y;

		if( _finish.ContainWidth )
		{
            if( width != _finish.Width || _finish.ControlPointWidth != null || _finish.IsRelativeWidth )
            {
			    changedSize = true;
			    width = _finish.IsRelativeWidth ? width + _finish.Width : _finish.Width;
                this._updateList.Add(GetUpdateWidth());
            }
		}
		if( _finish.ContainHeight )
		{
            if( height != _finish.Height || _finish.ControlPointHeight != null || _finish.IsRelativeHeight )
            {
			    changedSize = true;
			    height = _finish.IsRelativeHeight ? height + _finish.Height : _finish.Height;
                this._updateList.Add(GetUpdateHeight());
            }
		}
		float scaleX = _sca.x;
		float scaleY = _sca.y;
		float scaleZ = _sca.z;

		if( _finish.ControlPointScaleX != null && !_finish.ContainScaleX ) _finish.ScaleX = _sca.x;
		if( _finish.ControlPointScaleY != null && !_finish.ContainScaleY ) _finish.ScaleY = _sca.y;
		if( _finish.ControlPointScaleZ != null && !_finish.ContainScaleZ ) _finish.ScaleZ = _sca.z;

		if( _finish.ContainScaleX )
		{
            if( scaleX != _finish.ScaleX || _finish.ControlPointScaleX != null || _finish.IsRelativeScaleX )
            {
			    changedSca = true;
			    scaleX = _finish.IsRelativeScaleX ? scaleX + _finish.ScaleX : _finish.ScaleX;
                this._updateList.Add(GetUpdateScaleX());
            }
		}
		if( _finish.ContainScaleY )
		{
            if( scaleY != _finish.ScaleY || _finish.ControlPointScaleY != null || _finish.IsRelativeScaleY )
            {
			    changedSca = true;
			    scaleY = _finish.IsRelativeScaleY ? scaleY + _finish.ScaleY : _finish.ScaleY;
                this._updateList.Add(GetUpdateScaleY());
            }
		}
		if( _finish.ContainScaleZ )
		{
            if( scaleZ != _finish.ScaleZ || _finish.ControlPointScaleZ != null || _finish.IsRelativeScaleZ )
            {
			    changedSca = true;
			    scaleZ = _finish.IsRelativeScaleZ ? scaleZ + _finish.ScaleZ : _finish.ScaleZ;
                this._updateList.Add(GetUpdateScaleZ());
            }
		}
		float rotationX = _rot.x;
		float rotationY = _rot.y;
		float rotationZ = _rot.z;

		if( _finish.ControlPointRotationX != null && !_finish.ContainRotationX ) _finish.RotationX = _rot.x;
		if( _finish.ControlPointRotationY != null && !_finish.ContainRotationY ) _finish.RotationY = _rot.y;
		if( _finish.ControlPointRotationZ != null && !_finish.ContainRotationZ ) _finish.RotationZ = _rot.z;

		if( _finish.ContainRotationX )
		{
			if( rotationX != _finish.RotationX || _finish.ControlPointRotationX != null || 
				_finish.RotateXCount > 0 || _finish.IsRelativeRotateX )
            {
			    changedRot = true;
				rotationX = _finish.IsRelativeRotateX ? rotationX + _finish.RotationX : this.GetRotation( _rot.x, _finish.RotationX, _finish.RotateXClockwise, _finish.RotateXCount );
                this._updateList.Add(GetUpdateRotationX());
            }
		}
		if( _finish.ContainRotationY )
		{
			if( rotationY != _finish.RotationY || _finish.ControlPointRotationY != null || 
				_finish.RotateYCount > 0 || _finish.IsRelativeRotateY )
            {
			    changedRot = true;
				rotationY = _finish.IsRelativeRotateY ? rotationY + _finish.RotationY : this.GetRotation( _rot.y, _finish.RotationY, _finish.RotateYClockwise, _finish.RotateYCount );
                this._updateList.Add(GetUpdateRotationY());
            }
		}
		if( _finish.ContainRotationZ )
		{
            if( rotationZ != _finish.RotationZ || _finish.ControlPointRotationZ != null || 
				_finish.RotateZCount > 0 || _finish.IsRelativeRotateZ )
            {
			    changedRot = true;
				rotationZ = _finish.IsRelativeRotateZ ? rotationZ + _finish.RotationZ : this.GetRotation( _rot.z, _finish.RotationZ, _finish.RotateZClockwise, _finish.RotateZCount );
                this._updateList.Add(GetUpdateRotationZ());
            }
		}

		if( (changedRect || changedSize) && this._transformRect == null )
		{
			throw new Exception("'Width,Height,Left,Right,Top,Bottom' properties, can use only in UI object");
		}

		if( changedPos )
		{
            this._transform.localPosition = _pos;
			this._sPos = new Vector3( _pos.x, _pos.y, _pos.z );
			this._dPos = new Vector3( x, y, z );
			Action positionUpdator = this._transformRect == null ?
				(Action)this.UpdatePosition : (Action)this.UpdateAnchoredPosition;
			positionUpdator();
            this._updateList.Add(positionUpdator);
		}
		if( changedRect )
		{
            this._transformRect.offsetMin = new Vector2(_rect.left, _rect.bottom);
			this._transformRect.offsetMax = new Vector2(_rect.right, _rect.top) * -1f;
			this._sRect = new UIRect( _rect.left, _rect.top, _rect.right, _rect.bottom );
			this._dRect = new UIRect( left, top, right, bottom );
			this.UpdateRect();
            this._updateList.Add(UpdateRect);
		}
		if( changedSize )
		{
            this._transformRect.sizeDelta = _size;
			this._sSize = new Vector2( _size.x, _size.y );
			this._dSize = new Vector2( width, height );
			this.UpdateSize();
            this._updateList.Add(UpdateSize);
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
		
	protected override void UpdateObject()
	{
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
		// Debug.Log ("Dispose");
        this._stopOnDestroyHandler = null;
        this._updateList.Clear();
        this._updateList = null;
        this._transform = null;
    }

	protected virtual Action GetUpdateX() { return _finish.ControlPointX == null ? (Action)this.UpdateX : this.UpdateBezierX; }
	protected virtual Action GetUpdateY() { return _finish.ControlPointY == null ? (Action)this.UpdateY : this.UpdateBezierY; }
	protected virtual Action GetUpdateZ() { return _finish.ControlPointZ == null ? (Action)this.UpdateZ : this.UpdateBezierZ; }
	protected virtual Action GetUpdateLeft() { return _finish.ControlPointLeft == null ? (Action)this.UpdateLeft : this.UpdateBezierLeft; }
	protected virtual Action GetUpdateTop() { return _finish.ControlPointTop == null ? (Action)this.UpdateTop : this.UpdateBezierTop; }
	protected virtual Action GetUpdateRight() { return _finish.ControlPointRight == null ? (Action)this.UpdateRight : this.UpdateBezierRight; }
	protected virtual Action GetUpdateBottom() { return _finish.ControlPointBottom == null ? (Action)this.UpdateBottom : this.UpdateBezierBottom; }
	protected virtual Action GetUpdateWidth() { return _finish.ControlPointWidth == null ? (Action)this.UpdateWidth : this.UpdateBezierWidth; }
	protected virtual Action GetUpdateHeight() { return _finish.ControlPointHeight == null ? (Action)this.UpdateHeight : this.UpdateBezierHeight; }
	protected virtual Action GetUpdateScaleX() { return _finish.ControlPointScaleX == null ? (Action)this.UpdateScaleX : this.UpdateBezierScaleX; }
	protected virtual Action GetUpdateScaleY() { return _finish.ControlPointScaleY == null ? (Action)this.UpdateScaleY : this.UpdateBezierScaleY; }
	protected virtual Action GetUpdateScaleZ() { return _finish.ControlPointScaleZ == null ? (Action)this.UpdateScaleZ : this.UpdateBezierScaleZ; }
	protected virtual Action GetUpdateRotationX() { return _finish.ControlPointRotationX == null ? (Action)this.UpdateRotationX : this.UpdateBezierRotationX; }
	protected virtual Action GetUpdateRotationY() { return _finish.ControlPointRotationY == null ? (Action)this.UpdateRotationY : this.UpdateBezierRotationY; }
	protected virtual Action GetUpdateRotationZ() { return _finish.ControlPointRotationZ == null ? (Action)this.UpdateRotationZ : this.UpdateBezierRotationZ; }

    protected virtual void UpdateX()
	{
		_pos.x = _sPos.x * _invert + _dPos.x * _factor;
	}
	protected virtual void UpdateBezierX()
	{
		_pos.x = base.Calcurate( _finish.ControlPointX, _sPos.x, _dPos.x );
	}
    protected virtual void UpdateY()
	{
		_pos.y = _sPos.y * _invert + _dPos.y * _factor;
	}
	protected virtual void UpdateBezierY()
	{
		_pos.y = base.Calcurate( _finish.ControlPointY, _sPos.y, _dPos.y );
	}
    protected virtual void UpdateZ()
	{
		_pos.z = _sPos.z * _invert + _dPos.z * _factor;
	}
	protected virtual void UpdateBezierZ()
	{
		_pos.z = base.Calcurate( _finish.ControlPointZ, _sPos.z, _dPos.z );
	}
	protected virtual void UpdateLeft()
	{
		_rect.left = _sRect.left * _invert + _dRect.left * _factor;
	}
	protected virtual void UpdateBezierLeft()
	{
		_rect.left = base.Calcurate( _finish.ControlPointLeft, _sRect.left, _dRect.left );
	}
	protected virtual void UpdateTop()
	{
		_rect.top = _sRect.top * _invert + _dRect.top * _factor;
	}
	protected virtual void UpdateBezierTop()
	{
		_rect.top = base.Calcurate( _finish.ControlPointTop, _sRect.top, _dRect.top );
	}
	protected virtual void UpdateRight()
	{
		_rect.right = _sRect.right * _invert + _dRect.right * _factor;
	}
	protected virtual void UpdateBezierRight()
	{
		_rect.right = base.Calcurate( _finish.ControlPointRight, _sRect.right, _dRect.right );
	}
	protected virtual void UpdateBottom()
	{
		_rect.bottom = _sRect.bottom * _invert + _dRect.bottom * _factor;
	}
	protected virtual void UpdateBezierBottom()
	{
		_rect.bottom = base.Calcurate( _finish.ControlPointBottom, _sRect.bottom, _dRect.bottom );
	}
	protected virtual void UpdateWidth()
	{
		_size.x = _sSize.x * _invert + _dSize.x * _factor;
	}
	protected virtual void UpdateBezierWidth()
	{
		_size.x = base.Calcurate( _finish.ControlPointWidth, _sSize.x, _dSize.x );
	}
	protected virtual void UpdateHeight()
	{
		_size.y = _sSize.y * _invert + _dSize.y * _factor;
	}
	protected virtual void UpdateBezierHeight()
	{
		_size.y = base.Calcurate( _finish.ControlPointHeight, _sSize.y, _dSize.y );
	}
    protected virtual void UpdateScaleX()
	{
		_sca.x = _sSca.x * _invert + _dSca.x * _factor;
	}
	protected virtual void UpdateBezierScaleX()
	{
		_sca.x = base.Calcurate( _finish.ControlPointScaleX, _sSca.x, _dSca.x );
	}
    protected virtual void UpdateScaleY()
	{
		_sca.y = _sSca.y * _invert + _dSca.y * _factor;
	}
	protected virtual void UpdateBezierScaleY()
	{
		_sca.y = base.Calcurate( _finish.ControlPointScaleY, _sSca.y, _dSca.y );
	}
    protected virtual void UpdateScaleZ()
	{
		_sca.z = _sSca.z * _invert + _dSca.z * _factor;
	}
	protected virtual void UpdateBezierScaleZ()
	{
		_sca.z = base.Calcurate( _finish.ControlPointScaleZ, _sSca.z, _dSca.z );
	}
    protected virtual void UpdateRotationX()
	{
		_rot.x = _sRot.x * _invert + _dRot.x * _factor;
	}
	protected virtual void UpdateBezierRotationX()
	{
		_rot.x = base.Calcurate( _finish.ControlPointRotationX, _sRot.x, _dRot.x );
	}
    protected virtual void UpdateRotationY()
	{
		_rot.y = _sRot.y * _invert + _dRot.y * _factor;
	}
	protected virtual void UpdateBezierRotationY()
	{
		_rot.y = base.Calcurate( _finish.ControlPointRotationY, _sRot.y, _dRot.y );
	}
    protected virtual void UpdateRotationZ()
	{
		_rot.z = _sRot.z * _invert + _dRot.z * _factor;
	}
	protected virtual void UpdateBezierRotationZ()
	{
		_rot.z = base.Calcurate( _finish.ControlPointRotationZ, _sRot.z, _dRot.z );
	}
	//update transform
	private void UpdatePosition()
	{
		this._transform.localPosition = _pos;
	}
	private void UpdateAnchoredPosition()
	{
		this._transformRect.anchoredPosition3D = _pos;
	}
	private void UpdateRect()
	{
		Vector2 offset = this._transformRect.offsetMin;
		if(_finish.ContainLeft) offset.x = _rect.left;
		if(_finish.ContainBottom) offset.y = _rect.bottom;
		this._transformRect.offsetMin = offset;
		offset = this._transformRect.offsetMax;
		if(_finish.ContainRight) offset.x = -_rect.right;
		if(_finish.ContainTop) offset.y = -_rect.top;
		this._transformRect.offsetMax = offset;
	}
	private void UpdateSize()
	{
		Vector2 size = this._transformRect.sizeDelta;
		if(_finish.ContainWidth) size.x = _size.x;
		if(_finish.ContainHeight) size.y = _size.y;
		this._transformRect.sizeDelta = size;
	}
	private void UpdateScale()
	{
		this._transform.localScale = _sca;
	}
	private void UpdateRotation()
	{
		this._transform.localEulerAngles = _rot;
	}
}