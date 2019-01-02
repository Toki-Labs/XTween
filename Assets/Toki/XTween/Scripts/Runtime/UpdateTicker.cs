using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Toki.Tween
{
	public class UpdateTicker : UpdateTickerBase<UpdateTicker>, ITimer
	{	
#if UNITY_EDITOR
		public UpdateTicker() : base()
		{
		}
#endif

		protected override void TimeSet()
		{
#if UNITY_EDITOR
			if( Application.isPlaying )
                _time = UnityEngine.Time.time;
			else
                _time = UnityEngine.Time.realtimeSinceStartup;
#else
			_time = Time.time;
#endif
		}
	}
}