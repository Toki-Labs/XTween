using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Toki.Tween
{
	public class UpdaterColor : IDisposable
	{
		protected Color _sColor;
		protected Color _dColor;
		protected bool _initialized = false;
		protected bool _updateRed;
		protected bool _updateGreen;
		protected bool _updateBlue;
		protected bool _updateAlpha;

		public bool Initialized { get{ return _initialized; } }

		public void Initialize( Color sColor, Color dColor,
								bool updateRed, bool updateGreen, bool updateBlue, bool updateAlpha )
		{
			this._sColor = sColor;
			this._dColor = dColor;
			this._updateRed = updateRed;
			this._updateGreen = updateGreen;
			this._updateBlue = updateBlue;
			this._updateAlpha = updateAlpha;
			this._initialized = true;
		}

		public Color Update( float invert, float factor, Color color )
		{
			if(_updateRed) color.r = _sColor.r * invert + _dColor.r * factor;
			if(_updateGreen) color.g = _sColor.g * invert + _dColor.g * factor;
			if(_updateBlue) color.b = _sColor.b * invert + _dColor.b * factor;
			if(_updateAlpha) color.a = _sColor.a * invert + _dColor.a * factor;
			return color;
		}
		
		public void Dispose()
		{
			this._sColor = default(Color);
			this._dColor = default(Color);
			this._initialized = false;
		}
	}
}