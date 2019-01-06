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
		protected float[] _controlRed;
		protected float[] _controlGreen;
		protected float[] _controlBlue;
		protected float[] _controlAlpha;
		protected bool _initialized = false;
		protected bool _updateRed;
		protected bool _updateGreen;
		protected bool _updateBlue;
		protected bool _updateAlpha;
		protected int _updateRedIndex = 0;
		protected int _updateGreenIndex = 0;
		protected int _updateBlueIndex = 0;
		protected int _updateAlphaIndex = 0;


		public bool Initialized { get{ return _initialized; } }

		public void Initialize( Color sColor, Color dColor,
								float[] controlRed, float[] controlGreen, float[] controlBlue, float[] controlAlpha,
								bool updateRed, bool updateGreen, bool updateBlue, bool updateAlpha )
		{
			this._sColor = sColor;
			this._dColor = dColor;
			this._controlRed = controlRed;
			this._controlGreen = controlGreen;
			this._controlBlue = controlBlue;
			this._controlAlpha = controlAlpha;
			this._updateRed = updateRed;
			this._updateGreen = updateGreen;
			this._updateBlue = updateBlue;
			this._updateAlpha = updateAlpha;
			this._updateRedIndex = _updateRed ? _controlRed == null ? 1 : 2 : 0;
			this._updateGreenIndex = _updateGreen ? _controlGreen == null ? 1 : 2 : 0;
			this._updateBlueIndex = _updateBlue ? _controlBlue == null ? 1 : 2 : 0;
			this._updateAlphaIndex = _updateAlpha ? _controlAlpha == null ? 1 : 2 : 0;
			this._initialized = true;
		}

		public Color Update( float invert, float factor, Color color )
		{
			if(_updateRedIndex == 1)
				color.r = AbstractUpdater.Calcurate(_sColor.r, _dColor.r, invert, factor);
			else if(_updateRedIndex == 2)
				color.r = AbstractUpdater.Calcurate(_controlRed, _sColor.r, _dColor.r, invert, factor);

			if(_updateGreenIndex == 1)
				color.g = AbstractUpdater.Calcurate(_sColor.g, _dColor.g, invert, factor);
			else if(_updateGreenIndex == 2)
				color.g = AbstractUpdater.Calcurate(_controlGreen, _sColor.g, _dColor.g, invert, factor);

			if(_updateBlueIndex == 1)
				color.b = AbstractUpdater.Calcurate(_sColor.b, _dColor.b, invert, factor);
			else if(_updateBlueIndex == 2)
				color.b = AbstractUpdater.Calcurate(_controlBlue, _sColor.b, _dColor.b, invert, factor);

			if(_updateAlphaIndex == 1)
				color.a = AbstractUpdater.Calcurate(_sColor.a, _dColor.a, invert, factor);
			else if(_updateAlphaIndex == 2)
				color.a = AbstractUpdater.Calcurate(_controlAlpha, _sColor.a, _dColor.a, invert, factor);
			return color;
		}
		
		public void Dispose()
		{
			this._sColor = default(Color);
			this._dColor = default(Color);
			this._controlRed = null;
			this._controlGreen = null;
			this._controlBlue = null;
			this._controlAlpha = null;
			this._updateRed = false;
			this._updateGreen = false;
			this._updateBlue = false;
			this._updateAlpha = false;
			this._initialized = false;
		}
	}
}