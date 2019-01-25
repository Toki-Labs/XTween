using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
	
namespace Toki.Tween
{
	public class DisplayContinousUpdater : DisplayUpdater
	{
		public ITimer ticker;
		public int frameSkip;
		private float _deltaTime;

		public override void ResolveValues()
		{
			base.ResolveValues();
		}

		public override void Updating( float factor )
		{
			_factor = factor;
			this._deltaTime = ticker.GetDeltaTime( frameSkip );
			if (this._target == null)
			{
				this._tweener.StopOnDestroy();
				this.Dispose();
			}
			else
			{
				
			}
		}
	}
}