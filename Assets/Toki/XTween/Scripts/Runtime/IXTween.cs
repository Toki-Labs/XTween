using UnityEngine;
using System;
using System.Collections.Generic;
using Toki.Tween;

public interface IXTween
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
	IXTween Play();
	WaitForTweenPlay WaitForPlay();
	WaitForTweenPlay WaitForGotoAndPlay( float position );
	IXTween GotoAndPlay( float position );
	void GotoAndStop( float position );
	void Stop();
	void Reset();
	IXTween Clone();
	IXTween Lock();
	IXTween AddOnComplete(Action listener);
	IXTween AddOnComplete(IExecutable executor);
	IXTween AddOnStop(Action listener);
	IXTween AddOnStop(IExecutable executor);
	IXTween AddOnPlay(Action listener);
	IXTween AddOnPlay(IExecutable executor);
	IXTween AddOnUpdate(Action listener);
	IXTween AddOnUpdate(IExecutable executor);
	void Release();
}
