using UnityEngine;
using System;
using System.Collections.Generic;

public interface IAni
{
	float duration
	{
		get;
	}
		
	float position
	{
		get;
	}
		
	bool isPlaying
	{
		get;
	}
		
	bool stopOnComplete
	{
		get;
		set;
	}

	bool isRealTime
	{
		get;
		set;
	}

	uint frameSkip
	{
		get;
		set;
	}
		
	IExecutable onPlay
	{
		get;
		set;
	}
		
	IExecutable onStop
	{
		get;
		set;
	}
		
	IExecutable onUpdate
	{
		get;
		set;
	}
		
	IExecutable onComplete
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
