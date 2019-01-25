using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class UpdaterUIRect : IDisposable
	{
		protected Rect _sRect;
		protected Rect _dRect;
		protected float[] _controlLeft;
		protected float[] _controlRight;
		protected float[] _controlTop;
		protected float[] _controlBottom;
		public bool initialized = false;
		protected bool _updateLeft;
		protected bool _updateRight;
		protected bool _updateTop;
		protected bool _updateBottom;
		protected int _updateLeftIndex = 0;
		protected int _updateRightIndex = 0;
		protected int _updateTopIndex = 0;
		protected int _updateBottomIndex = 0;

		public void Initialize( Rect sRect, Rect dRect,
								float[] controlLeft, float[] controlRight, float[] controlTop, float[] controlBottom,
								bool updateLeft, bool updateRight, bool updateTop, bool updateBottom )
		{
			this._sRect = sRect;
			this._dRect = dRect;
			this._controlLeft = controlLeft;
			this._controlRight = controlRight;
			this._controlTop = controlTop;
			this._controlBottom = controlBottom;
			this._updateLeft = updateLeft;
			this._updateRight = updateRight;
			this._updateTop = updateTop;
			this._updateBottom = updateBottom;
			this._updateLeftIndex = _updateLeft ? _controlLeft == null ? 1 : 2 : 0;
			this._updateRightIndex = _updateRight ? _controlRight == null ? 1 : 2 : 0;
			this._updateTopIndex = _updateTop ? _controlTop == null ? 1 : 2 : 0;
			this._updateBottomIndex = _updateBottom ? _controlBottom == null ? 1 : 2 : 0;
			this.initialized = true;
		}

		public void Update( float invert, float factor, RectTransform transform )
		{
			Vector2 offsetMin = transform.offsetMin;
			Vector2 offsetMax = transform.offsetMax;
			if(_updateLeftIndex == 1)
				offsetMin.x = AbstractUpdater.Calcurate(_sRect.x, _dRect.x, invert, factor);
			else if(_updateLeftIndex == 2)
				offsetMin.x = AbstractUpdater.Calcurate(_controlLeft, _sRect.x, _dRect.x, invert, factor);

			if(_updateBottom) offsetMin.y = _sRect.height * invert + _dRect.height * factor;

			if(_updateBottomIndex == 1)
				offsetMin.y = AbstractUpdater.Calcurate(_sRect.height, _dRect.height, invert, factor);
			else if(_updateBottomIndex == 2)
				offsetMin.y = AbstractUpdater.Calcurate(_controlBottom, _sRect.height, _dRect.height, invert, factor);

			if(_updateRightIndex == 1)
				offsetMax.x = -AbstractUpdater.Calcurate(_sRect.width, _dRect.width, invert, factor);
			else if(_updateRightIndex == 2)
				offsetMax.x = -AbstractUpdater.Calcurate(_controlRight, _sRect.width, _dRect.width, invert, factor);

			if(_updateTop) offsetMax.y = -(_sRect.y * invert + _dRect.y * factor);

			if(_updateTopIndex == 1)
				offsetMax.y = -AbstractUpdater.Calcurate(_sRect.y, _dRect.y, invert, factor);
			else if(_updateTopIndex == 2)
				offsetMax.y = -AbstractUpdater.Calcurate(_controlTop, _sRect.y, _dRect.y, invert, factor);

			transform.offsetMin = offsetMin;
			transform.offsetMax = offsetMax;
		}
		
		public void Dispose()
		{
			this._sRect = default(Rect);
			this._dRect = default(Rect);
			this._controlLeft = null;
			this._controlRight = null;
			this._controlTop = null;
			this._controlBottom = null;
			this._updateLeft = false;
			this._updateRight = false;
			this._updateTop = false;
			this._updateBottom = false;
			this.initialized = false;
		}
	}
}