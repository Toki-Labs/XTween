using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class UpdaterVector3 : IDisposable
	{
		protected Vector3 _sPos;
		protected Vector3 _dPos;
		protected float[] _controlX;
		protected float[] _controlY;
		protected float[] _controlZ;
		public bool initialized = false;
		protected bool _updateX;
		protected bool _updateY;
		protected bool _updateZ;
		protected int _updateXIndex = 0;
		protected int _updateYIndex = 0;
		protected int _updateZIndex = 0;

		public void Initialize( Vector3 sPos, Vector3 dPos,
								float[] controlX, float[] controlY, float[] controlZ,
								bool updateX, bool updateY, bool updateZ )
		{
			this._sPos = sPos;
			this._dPos = dPos;
			this._controlX = controlX;
			this._controlY = controlY;
			this._controlZ = controlZ;
			this._updateX = updateX;
			this._updateY = updateY;
			this._updateZ = updateZ;
			this._updateXIndex = _updateX ? _controlX == null ? 1 : 2 : 0;
			this._updateYIndex = _updateY ? _controlY == null ? 1 : 2 : 0;
			this._updateZIndex = _updateZ ? _controlZ == null ? 1 : 2 : 0;
			this.initialized = true;
		}

		public Vector3 Update( float invert, float factor, Vector3 vect )
		{
			if(_updateXIndex == 1)
				vect.x = AbstractUpdater.Calcurate(_sPos.x, _dPos.x, invert, factor);
			else if(_updateXIndex == 2)
				vect.x = AbstractUpdater.Calcurate(_controlX, _sPos.x, _dPos.x, invert, factor);

			if(_updateYIndex == 1)
				vect.y = AbstractUpdater.Calcurate(_sPos.y, _dPos.y, invert, factor);
			else if(_updateYIndex == 2)
				vect.y = AbstractUpdater.Calcurate(_controlY, _sPos.y, _dPos.y, invert, factor);

			if(_updateZIndex == 1)
				vect.z = AbstractUpdater.Calcurate(_sPos.z, _dPos.z, invert, factor);
			else if(_updateZIndex == 2)
				vect.z = AbstractUpdater.Calcurate(_controlZ, _sPos.z, _dPos.z, invert, factor);
			return vect;
		}
		
		public void Dispose()
		{
			this._sPos = default(Vector3);
			this._dPos = default(Vector3);
			this._controlX = null;
			this._controlY = null;
			this._controlZ = null;
			this._updateX = false;
			this._updateY = false;
			this._updateZ = false;
			this.initialized = false;
		}
	}
}