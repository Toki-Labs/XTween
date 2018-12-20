using System;

public interface IIObjectTween : IObjectTween, IIAni
{
	float time
	{
		get;
		set;
	}
		
	IEasing easing
	{
		get;
		set;
	}
		
	IUpdater updater
	{
		get;
		set;
	}
}
