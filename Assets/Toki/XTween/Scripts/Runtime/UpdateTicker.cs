using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

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
			_time = Time.time;
		else
			_time = Time.realtimeSinceStartup;
#else
		_time = Time.time;
#endif
	}
}