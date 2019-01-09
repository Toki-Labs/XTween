using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class DisplayUpdater : AbstractUpdater
	{
		protected GameObject _target = null;
		protected Transform _transform;
		protected RectTransform _transformRect;
		protected XHash _start;
		protected XHash _finish;
		protected UpdaterVector3 _updaterPosition = new UpdaterVector3();
		protected UpdaterVector3 _updaterAnchoredPosition = new UpdaterVector3();
		protected UpdaterVector2 _updaterSizeDelta = new UpdaterVector2();
		protected UpdaterUIRect _updaterUIRect = new UpdaterUIRect();
		protected UpdaterVector3 _updaterScale = new UpdaterVector3();
		protected UpdaterVector3 _updaterRotation = new UpdaterVector3();

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
			if( _resolvedValues ) return;
			if( _target == null )
			{
				this._tweener.StopOnDestroy();
				throw new System.NullReferenceException("Tweener target is Null at start point");
			}

			this._transform = this._target.transform;
			this._transformRect = this._target.transform as RectTransform;
			
			Vector3 pos;
			Vector2 size = Vector3.zero;
			Vector3 rot;
			Vector3 sca;
			Rect rect = Rect.zero;
			if( this._transformRect == null )
			{
				pos = this._transform.localPosition;
			}
			else
			{
				pos = this._transformRect.anchoredPosition3D;
				size = this._transformRect.sizeDelta;
				rect = new Rect( this._transformRect.offsetMin.x, 
									-this._transformRect.offsetMax.y,
									-this._transformRect.offsetMax.x,
									this._transformRect.offsetMin.y);
			}
			rot = this._transform.localEulerAngles;
			sca = this._transform.localScale;
			
			//if exist source, set values
			if( _start.ContainX )
			{
				pos.x = _start.X;
			}
			if( _start.ContainY )
			{
				pos.y = _start.Y;
			}
			if( _start.ContainZ )
			{
				pos.z = _start.Z;
			}
			if( _start.ContainLeft )
			{
				rect.x = _start.Left;
			}
			if( _start.ContainTop )
			{
				rect.y = _start.Top;
			}
			if( _start.ContainRight )
			{
				rect.width = _start.Right;
			}
			if( _start.ContainBottom )
			{
				rect.height = _start.Bottom;
			}
			if( _start.ContainWidth )
			{
				size.x = _start.Width;
			}
			if( _start.ContainHeight )
			{
				size.x = _start.Height;
			}
			if( _start.ContainScaleX )
			{
				sca.x = _start.ScaleX;
			}
			if( _start.ContainScaleY )
			{
				sca.y = _start.ScaleY;
			}
			if( _start.ContainScaleZ )
			{
				sca.z = _start.ScaleZ;
			}
			if( _start.ContainRotationX )
			{
				rot.x = _start.RotationX;
			}
			if( _start.ContainRotationY )
			{
				rot.y = _start.RotationY;
			}
			if( _start.ContainRotationZ )
			{
				rot.z = _start.RotationZ;
			}
			
			bool changedPos = false;
			bool changeX = false;
			bool changeY = false;
			bool changeZ = false;
			bool changeRect = false;
			bool changeRectLeft = false;
			bool changeRectTop = false;
			bool changeRectRight = false;
			bool changeRectBottom = false;
			bool changeSize = false;
			bool changeSizeX = false;
			bool changeSizeY = false;
			bool changeSca = false;
			bool changeScaX = false;
			bool changeScaY = false;
			bool changeScaZ = false;
			bool changeRot = false;
			bool changeRotX = false;
			bool changeRotY = false;
			bool changeRotZ = false;
				
			float x = pos.x;
			float y = pos.y;
			float z = pos.z;

			if( _finish.ControlPointX != null && !_finish.ContainX ) _finish.X = pos.x;
			if( _finish.ControlPointY != null && !_finish.ContainY ) _finish.Y = pos.y;
			if( _finish.ControlPointZ != null && !_finish.ContainZ ) _finish.Z = pos.z;
			
			if( _finish.ContainX )
			{
				if( x != _finish.X || _finish.ControlPointX != null || _finish.IsRelativeX )
				{ 
					changedPos = true;
					changeX = true;
					x = _finish.IsRelativeX ? x + _finish.X : _finish.X;
				}
			}
			if( _finish.ContainY )
			{
				if( y != _finish.Y || _finish.ControlPointY != null || _finish.IsRelativeY )
				{
					changedPos = true;
					changeY = true;
					y = _finish.IsRelativeY ? y + _finish.Y : _finish.Y;
				}
			}
			if( _finish.ContainZ )
			{
				if( z != _finish.Z || _finish.ControlPointZ != null || _finish.IsRelativeZ )
				{
					changedPos = true;
					changeZ = true;
					z = _finish.IsRelativeZ ? z + _finish.Z : _finish.Z;
				}
			}
			float left = rect.x;
			float top = rect.y;
			float right = rect.width;
			float bottom = rect.height;

			if( _finish.ControlPointLeft != null && !_finish.ContainLeft ) _finish.Left = rect.x;
			if( _finish.ControlPointRight != null && !_finish.ContainRight ) _finish.Right = rect.width;
			if( _finish.ControlPointTop != null && !_finish.ContainTop ) _finish.Top = rect.y;
			if( _finish.ControlPointBottom != null && !_finish.ContainBottom ) _finish.Bottom = rect.height;

			if( _finish.ContainLeft )
			{
				if( left != _finish.Left || _finish.ControlPointLeft != null || _finish.IsRelativeLeft )
				{ 
					changeRect = true;
					changeRectLeft = true;
					left = _finish.IsRelativeLeft ? left + _finish.Left : _finish.Left;
				}
			}
			if( _finish.ContainRight )
			{
				if( right != _finish.Right || _finish.ControlPointRight != null || _finish.IsRelativeRight )
				{ 
					changeRect = true;
					changeRectRight = true;
					right = _finish.IsRelativeRight ? right + _finish.Right : _finish.Right;
				}
			}
			if( _finish.ContainTop )
			{
				if( top != _finish.Top || _finish.ControlPointTop != null || _finish.IsRelativeTop )
				{ 
					changeRect = true;
					changeRectTop = true;
					top = _finish.IsRelativeTop ? top + _finish.Top : _finish.Top;
				}
			}
			if( _finish.ContainBottom )
			{
				if( bottom != _finish.Bottom || _finish.ControlPointBottom != null || _finish.IsRelativeBottom )
				{ 
					changeRect = true;
					changeRectBottom = true;
					bottom = _finish.IsRelativeBottom ? bottom + _finish.Bottom : _finish.Bottom;
				}
			}
			float width = size.x;
			float height = size.y;

			if( _finish.ControlPointWidth != null && !_finish.ContainWidth ) _finish.Width = size.x;
			if( _finish.ControlPointHeight != null && !_finish.ContainHeight ) _finish.Height = size.y;

			if( _finish.ContainWidth )
			{
				if( width != _finish.Width || _finish.ControlPointWidth != null || _finish.IsRelativeWidth )
				{
					changeSize = true;
					changeSizeX = true;
					width = _finish.IsRelativeWidth ? width + _finish.Width : _finish.Width;
				}
			}
			if( _finish.ContainHeight )
			{
				if( height != _finish.Height || _finish.ControlPointHeight != null || _finish.IsRelativeHeight )
				{
					changeSize = true;
					changeSizeY = true;
					height = _finish.IsRelativeHeight ? height + _finish.Height : _finish.Height;
				}
			}
			float scaleX = sca.x;
			float scaleY = sca.y;
			float scaleZ = sca.z;

			if( _finish.ControlPointScaleX != null && !_finish.ContainScaleX ) _finish.ScaleX = sca.x;
			if( _finish.ControlPointScaleY != null && !_finish.ContainScaleY ) _finish.ScaleY = sca.y;
			if( _finish.ControlPointScaleZ != null && !_finish.ContainScaleZ ) _finish.ScaleZ = sca.z;

			if( _finish.ContainScaleX )
			{
				if( scaleX != _finish.ScaleX || _finish.ControlPointScaleX != null || _finish.IsRelativeScaleX )
				{
					changeSca = true;
					changeScaX = true;
					scaleX = _finish.IsRelativeScaleX ? scaleX + _finish.ScaleX : _finish.ScaleX;
				}
			}
			if( _finish.ContainScaleY )
			{
				if( scaleY != _finish.ScaleY || _finish.ControlPointScaleY != null || _finish.IsRelativeScaleY )
				{
					changeSca = true;
					changeScaY = true;
					scaleY = _finish.IsRelativeScaleY ? scaleY + _finish.ScaleY : _finish.ScaleY;
				}
			}
			if( _finish.ContainScaleZ )
			{
				if( scaleZ != _finish.ScaleZ || _finish.ControlPointScaleZ != null || _finish.IsRelativeScaleZ )
				{
					changeSca = true;
					changeScaZ = true;
					scaleZ = _finish.IsRelativeScaleZ ? scaleZ + _finish.ScaleZ : _finish.ScaleZ;
				}
			}
			float rotationX = rot.x;
			float rotationY = rot.y;
			float rotationZ = rot.z;

			if( _finish.ControlPointRotationX != null && !_finish.ContainRotationX ) _finish.RotationX = rot.x;
			if( _finish.ControlPointRotationY != null && !_finish.ContainRotationY ) _finish.RotationY = rot.y;
			if( _finish.ControlPointRotationZ != null && !_finish.ContainRotationZ ) _finish.RotationZ = rot.z;

			if( _finish.ContainRotationX )
			{
				if( rotationX != _finish.RotationX || _finish.ControlPointRotationX != null || 
					_finish.RotateXCount > 0 || _finish.IsRelativeRotateX )
				{
					changeRot = true;
					changeRotX = true;
					rotationX = _finish.IsRelativeRotateX ? rotationX + _finish.RotationX : this.GetRotation( rot.x, _finish.RotationX, _finish.RotateXClockwise, _finish.RotateXCount );
				}
			}
			if( _finish.ContainRotationY )
			{
				if( rotationY != _finish.RotationY || _finish.ControlPointRotationY != null || 
					_finish.RotateYCount > 0 || _finish.IsRelativeRotateY )
				{
					changeRot = true;
					changeRotY = true;
					rotationY = _finish.IsRelativeRotateY ? rotationY + _finish.RotationY : this.GetRotation( rot.y, _finish.RotationY, _finish.RotateYClockwise, _finish.RotateYCount );
				}
			}
			if( _finish.ContainRotationZ )
			{
				if( rotationZ != _finish.RotationZ || _finish.ControlPointRotationZ != null || 
					_finish.RotateZCount > 0 || _finish.IsRelativeRotateZ )
				{
					changeRot = true;
					changeRotZ = true;
					rotationZ = _finish.IsRelativeRotateZ ? rotationZ + _finish.RotationZ : this.GetRotation( rot.z, _finish.RotationZ, _finish.RotateZClockwise, _finish.RotateZCount );
				}
			}

			if( (changeRect || changeSize) && this._transformRect == null )
			{
				throw new Exception("'Width,Height,Left,Right,Top,Bottom' properties, can use only in UI object");
			}

			if( changedPos )
			{
				this._transform.localPosition = pos;
				Vector3 sPos = new Vector3( pos.x, pos.y, pos.z );
				Vector3 dPos = new Vector3( x, y, z );
				if( _transformRect == null )
					this._updaterPosition.Initialize(sPos, dPos, 
						_finish.ControlPointX, _finish.ControlPointY, _finish.ControlPointZ, 
						changeX, changeY, changeZ);
				else
					this._updaterAnchoredPosition.Initialize(sPos, dPos, 
						_finish.ControlPointX, _finish.ControlPointY, _finish.ControlPointZ, 
						changeX, changeY, changeZ);
			}
			if( changeRect )
			{
				this._transformRect.offsetMin = new Vector2(rect.x, rect.height);
				this._transformRect.offsetMax = new Vector2(rect.width, rect.y) * -1f;
				Rect sRect = new Rect( rect.x, rect.y, rect.width, rect.height );
				Rect dRect = new Rect( left, top, right, bottom );
				this._updaterUIRect.Initialize(sRect, dRect, 
					_finish.ControlPointLeft, _finish.ControlPointRight, _finish.ControlPointTop, _finish.ControlPointBottom,
					changeRectLeft, changeRectRight, changeRectTop, changeRectBottom);
			}
			if( changeSize )
			{
				this._transformRect.sizeDelta = size;
				Vector2 sSize = new Vector2( size.x, size.y );
				Vector2 dSize = new Vector2( width, height );
				this._updaterSizeDelta.Initialize(sSize, dSize, 
					_finish.ControlPointWidth, _finish.ControlPointHeight, changeSizeX, changeSizeY);
			}
			if( changeSca )
			{
				this._transform.localScale = sca;
				Vector3 sSca = new Vector3( sca.x, sca.y, sca.z );
				Vector3 dSca = new Vector3( scaleX, scaleY, scaleZ );
				this._updaterScale.Initialize(sSca, dSca, 
					_finish.ControlPointScaleX, _finish.ControlPointScaleY, _finish.ControlPointScaleZ, 
					changeScaX, changeScaY, changeScaZ);
			}
			if( changeRot )
			{
				this._transform.localEulerAngles = rot;
				Vector3 sRot = new Vector3( rot.x, rot.y, rot.z );
				Vector3 dRot = new Vector3( rotationX, rotationY, rotationZ );
				this._updaterRotation.Initialize(sRot, dRot, 
					_finish.ControlPointRotationX, _finish.ControlPointRotationY, _finish.ControlPointRotationZ, 
					changeRotX, changeRotY, changeRotZ);
			}

			this._resolvedValues = true;
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
				this._tweener.StopOnDestroy();
			}
			else
			{
				if( _updaterPosition.initialized ) 
					_transform.localPosition = _updaterPosition.Update(_invert, _factor, _transform.localPosition);
				if( _updaterAnchoredPosition.initialized )
					_transformRect.anchoredPosition3D = _updaterAnchoredPosition.Update(_invert, _factor, _transformRect.anchoredPosition3D);
				if( _updaterUIRect.initialized )
					_updaterUIRect.Update(_invert, _factor, _transformRect);
				if( _updaterScale.initialized ) 
					_transform.localScale = _updaterScale.Update(_invert, _factor, _transform.localScale);
				if( _updaterSizeDelta.initialized )
					_transformRect.sizeDelta = _updaterSizeDelta.Update(_invert, _factor, _transformRect.sizeDelta);
				if( _updaterRotation.initialized ) 
				{
					Debug.Log("Rotate!");
					_transform.localEulerAngles = _updaterRotation.Update(_invert, _factor, _transform.localEulerAngles);
				}
			}
		}

		public override void Release()
		{
			if( this._start != null ) this._start.PoolPush();
			if( this._finish != null ) this._finish.PoolPush();
			this.PoolPush();
		}

		public override void Dispose()
		{
			base.Dispose();
			this._updaterPosition.Dispose();
			this._updaterAnchoredPosition.Dispose();
			this._updaterSizeDelta.Dispose();
			this._updaterUIRect.Dispose();
			this._updaterScale.Dispose();
			this._updaterRotation.Dispose();
			this._target = null;
			this._transform = null;
			this._transformRect = null;
		}
	}
}