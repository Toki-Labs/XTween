using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DeltaTimeTicker
{
	private int _frameSkip;
	private int _frameSkipCount;
	private float _deltaTime;
	private float _time;

	public float deltaTime { get{ return this._deltaTime; } }

	public DeltaTimeTicker( int frameSkip, float time )
	{
		this._frameSkip = frameSkip;
		this._frameSkipCount = 0;
		this._deltaTime = 0f;
		this._time = time;
	}

	public void Update( float time )
	{
		this._frameSkipCount++;
		if( this._frameSkip < this._frameSkipCount )
		{
			this._deltaTime = time - this._time;
			this._time = time;
			this._frameSkipCount = 0;
		}
	}
}

public class UpdateTickerReal : UpdateTickerBase<UpdateTickerReal>, ITimer
{
	public UpdateTickerReal() : base()
	{

	}
	
	protected override void TimeSet()
	{
		_time = Time.realtimeSinceStartup;
	}
}

