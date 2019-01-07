using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class UpdaterVector2 : IDisposable
	{
		protected Vector2 _sSize;
		protected Vector2 _dSize;
		protected float[] _controlX;
		protected float[] _controlY;
		public bool initialized = false;
		protected bool _updateX;
		protected bool _updateY;
		protected int _updateXIndex = 0;
		protected int _updateYIndex = 0;

		public void Initialize( Vector3 sPos, Vector3 dPos,
								float[] controlX, float[] controlY,
								bool updateX, bool updateY )
		{
			this._sSize = sPos;
			this._dSize = dPos;
			this._controlX = controlX;
			this._controlY = controlY;
			this._updateX = updateX;
			this._updateY = updateY;
			this._updateXIndex = _updateX ? _controlX == null ? 1 : 2 : 0;
			this._updateYIndex = _updateY ? _controlY == null ? 1 : 2 : 0;
			this.initialized = true;
		}

		public Vector2 Update( float invert, float factor, Vector2 vect )
		{
			if(_updateXIndex == 1)
				vect.x = AbstractUpdater.Calcurate(_sSize.x, _dSize.x, invert, factor);
			else if(_updateXIndex == 2)
				vect.x = AbstractUpdater.Calcurate(_controlX, _sSize.x, _dSize.x, invert, factor);

			if(_updateYIndex == 1)
				vect.y = AbstractUpdater.Calcurate(_sSize.y, _dSize.y, invert, factor);
			else if(_updateYIndex == 2)
				vect.y = AbstractUpdater.Calcurate(_controlY, _sSize.y, _dSize.y, invert, factor);

			return vect;
		}
		
		public void Dispose()
		{
			this._sSize = default(Vector3);
			this._dSize = default(Vector3);
			this._controlX = null;
			this._controlY = null;
			this._updateX = false;
			this._updateY = false;
			this.initialized = false;
		}
	}
}