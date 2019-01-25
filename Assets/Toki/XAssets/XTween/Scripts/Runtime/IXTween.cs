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
    void StopOnDestroy();
	IXTween Play();
	IXTween Play( float position );
	WaitForTweenPlay WaitForPlay();
	WaitForTweenPlay WaitForPlay( float position );
	IXTween Seek( float position );
	void Stop();
	void Reset();
	IXTween Clone();
	IXTween SetFrameSkip(uint skip);
	IXTween SetLock();
	IXTween SetReverse();
	IXTween SetRepeat(int count);
	IXTween SetScale(float scale);
	IXTween SetDelay(float preDelay, float postDelay = 0f);
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
