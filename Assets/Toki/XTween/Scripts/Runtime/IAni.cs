using UnityEngine;
using System;
using System.Collections.Generic;

public interface IAni
{
	float Duration
	{
		get;
	}
		
	float Position
	{
		get;
	}
		
	bool IsPlaying
	{
		get;
	}
		
	bool StopOnComplete
	{
		get;
		set;
	}

	bool IsRealTime
	{
		get;
		set;
	}

	uint FrameSkip
	{
		get;
		set;
	}
		
	IExecutable OnPlay
	{
		get;
		set;
	}
		
	IExecutable OnStop
	{
		get;
		set;
	}
		
	IExecutable OnUpdate
	{
		get;
		set;
	}
		
	IExecutable OnComplete
	{
		get;
		set;
	}

    void StopOnDestroy();
	void Play();
	void Stop();	
	void TogglePause();
	void GotoAndPlay( float position );
	void GotoAndStop( float position );
	void Dispose();
	IAni Clone();
}
